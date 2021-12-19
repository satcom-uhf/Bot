using DirectShowLib;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace SatcomPiratesBot
{
    public partial class MainForm : Form
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        public static DateTime LastActivity;
        private DateTime dtmfDetected = DateTime.Now;
        public static int PttClickCounter { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadPorts();
            LoadSettings();
        }

        private void LoadPorts()
        {
            outComPortsBox.Items.Clear();
            outComPortsBox.Items.AddRange(SerialPort.GetPortNames());
        }

        private bool closingHack = true;
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closingHack)
            {
                cts.Cancel();
                e.Cancel = true;
                closingHack = false;
                SaveSettings();
                Close();
            }
        }

        #region Settings
        internal static ConfigModel Config { get; private set; } = new ConfigModel();

        private void LoadSettings()
        {
            try
            {
                Config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("preferences.json"));
            }
            catch
            {
                // use default values
            }
            
            telegramTokenBox.Text = Config.TelegramToken;
            n2yoApiKeyBox.Text = Config.N2YOApiKey;
            sstvPathBox.Text = Config.SSTVPath;
            sstvChannelBox.Text = Config.SSTVChannel;
            mainGroupBox.Text = Config.PrimaryGroup.ToString();
            dtmfCodeBox.Text = Config.DTMFCode;
            witAiTokenBox.Text = Config.WitAiToken;
        }
        private void SaveSettings()
        {
            try
            {
                Config.TelegramToken = telegramTokenBox.Text;
                Config.N2YOApiKey = n2yoApiKeyBox.Text;
                Config.SSTVPath = sstvPathBox.Text;
                Config.SSTVChannel = sstvChannelBox.Text;
                Config.PrimaryGroup = Convert.ToInt64(mainGroupBox.Text);
                Config.DTMFCode = dtmfCodeBox.Text;
                Config.WitAiToken = witAiTokenBox.Text;
                File.WriteAllText("preferences.json", JsonConvert.SerializeObject(Config));
            }
            catch
            {
                // No problem
            }
        }
        #endregion
        
        private void apiKeysSaveButton_Click(object sender, EventArgs e) => SaveSettings();

        private void setSstvPathButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = sstvPathBox.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                sstvPathBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private async void testTelegramButton_Click(object sender, EventArgs e)
        {
            try
            {
                var bot = new TelegramBotClient(telegramTokenBox.Text);
                var user = await bot.GetMeAsync();
                MessageBox.Show($"Success: Username:{user.Username}");
                SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void runTelegramButton_Click(object sender, EventArgs e)
        {
            await Telegram.Start(Config, cts.Token);
            DTMF.Changed += (s, e) => Invoke(new Action(() => dtmfLabel.Text = e));
            DTMF.Detected += async (s, e) =>
            {
                try
                {
                    Transmitter.Vox.Start(TimeSpan.FromSeconds(10), cts.Token);
                    await Sound.PlayOK();
                    dtmfDetected = DateTime.Now;
                }
                finally
                {
                    Transmitter.Vox.Stop();
                }
            };
            // DTMF.StartDetection(Config);
            runTelegramButton.Text = "Started";
            runTelegramButton.Enabled = false;
        }

        private void refreshPortsButton_Click(object sender, EventArgs e)
        {
            LoadPorts();
        }

        private void connectComPortButton_Click(object sender, EventArgs e)
        {
            var comPortState = !outComPortsBox.Enabled;
            if (!comPortState)
            {
                OpenComPort();
                connectComPortButton.Text = "Disconnect;";
                outComPortsBox.Enabled = false;
                refreshPortsButton.Enabled = false;
            }
            else
            {
                Transmitter.ComPort?.Close();
                connectComPortButton.Text = "Connect";
                refreshPortsButton.Enabled = true;
                outComPortsBox.Enabled = true;
            }
        }

        private Action handleActive;
        private const string SQUELCH_OPEN = "Squelch opened";
        private const string ACTIVITY = "Activity";
        private const string SILENCE = "Silence";
        private void OpenComPort()
        {
            var portName = outComPortsBox.SelectedItem?.ToString();
            handleActive = Debounce(() =>
             {
                 if (Transmitter.ChannelBusy)
                 {
                     Invoke(new Action(() =>
                     {
                         activityLabel.Text = ACTIVITY;
                     }));
                     LastActivity = DateTime.Now;
                 }
             });
            if (!string.IsNullOrEmpty(portName))
            {
                Transmitter.ComPort = new SerialPort(portName);
                Transmitter.ComPort.BaudRate = 9600;
                Transmitter.ComPort.DataReceived += (s, e) =>
                {
                    var data = Transmitter.ComPort.ReadExisting();
                    Invoke(new Action(() => activityLabel.Text = data));
                    return;
                    if (data.Contains("activity"))
                    {
                        Invoke(new Action(() => activityLabel.Text = data));
                        Transmitter.ChannelBusy = true;
                        handleActive();
                        RecordSoundIfNeed();
                    }
                    else if (data.Contains("silence"))
                    {
                        Transmitter.ChannelBusy = false;
                        Invoke(new Action(() =>
                        {
                            if (activityLabel.Text == SQUELCH_OPEN)
                            {
                                PttClickCounter++;
                            }
                            activityLabel.Text = SILENCE;
                        }));
                    }
                };
                Transmitter.ComPort.Open();
            }
        }

        private void RecordSoundIfNeed()
        {
            var diff = DateTime.Now - dtmfDetected;
            if (diff < TimeSpan.FromSeconds(10))
            {
                Task.Run(async () =>
                {
                    try
                    {
                        Log.Information("Recording started");
                        var (available, caption) = await Sound.RecordVoice();
                        if (available)
                        {
                            Log.Information("Sending recorded mp3");
                            await Telegram.SendVoiceMessageToChannel(Config, caption);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Recording error");
                    }
                });
            }
        }

        private Action Debounce(Action func, int milliseconds = 1200)
        {
            var last = 0;
            return () =>
            {
                var current = Interlocked.Increment(ref last);
                Task.Delay(milliseconds, cts.Token).ContinueWith(task =>
                {
                    if (current == last) func();
                    task.Dispose();
                });
            };
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Vox.Sensitivity = trackBar1.Value;
        }

        private ImageStreamingServer streamingServer;
        private void startWebCamServerButton_Click(object sender, EventArgs e)
        {
            startWebCamServerButton.Text = "Started";
            startWebCamServerButton.Enabled = false;
            streamingServer = new ImageStreamingServer(screenPanel);
            streamingServer.Start(Convert.ToInt32(httpPortNumberBox.Value));
        }
    }
}
