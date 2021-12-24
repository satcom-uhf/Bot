using DirectShowLib;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
        PrivateFontCollection pfc = new PrivateFontCollection();
        private const int ParrotsCount = 25;

        public static int PttClickCounter { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pfc.AddFontFile("LCR_Parrot_Talk.ttf");
            LoadPorts();
            LoadSettings();
            ParrotLevel(0);
        }

        public static void AppendText(RichTextBox box, string text, Color color, Font font)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionFont = font;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void ParrotLevel(int level)
        {
            if (level > ParrotsCount) level = ParrotsCount;
            if (level < 0) level = 0;
            sMeter.Text = "";
            for (int i = 0; i < ParrotsCount; i++)
            {
                AppendText(sMeter, "E", level > i ? GetColor(i) : Color.Gray, new Font(pfc.Families[0], 14, FontStyle.Bold));
            }
        }

        private Color GetColor(float level) => level switch
        {
            < ParrotsCount * 0.2f => Color.Green,
            <= ParrotsCount * 0.5f => Color.Lime,
            <= ParrotsCount * 0.75f => Color.YellowGreen,
            _ => Color.Red
        };


        private void LoadPorts()
        {
            comPortsBox.Items.Clear();
            comPortsBox.Items.AddRange(SerialPort.GetPortNames());
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
                Transmitter.ComPort?.Close();
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
            //DTMF.Changed += (s, e) => Invoke(new Action(() => dtmfLabel.Text = e));
            //DTMF.Detected += async (s, e) =>
            //{
            //    try
            //    {
            //        Transmitter.Vox.Start(TimeSpan.FromSeconds(10), cts.Token);
            //        await Sound.PlayOK();
            //        dtmfDetected = DateTime.Now;
            //    }
            //    finally
            //    {
            //        Transmitter.Vox.Stop();
            //    }
            //};
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
            var comPortState = !comPortsBox.Enabled;
            if (!comPortState)
            {
                OpenComPort();
                connectComPortButton.Text = "Disconnect;";
                comPortsBox.Enabled = false;
                refreshPortsButton.Enabled = false;
            }
            else
            {
                Transmitter.ComPort?.Close();
                connectComPortButton.Text = "Connect";
                refreshPortsButton.Enabled = true;
                comPortsBox.Enabled = true;
            }
        }

        private Action handleActive;
        private const string SQUELCH_OPEN = "Squelch opened";
        private const string ACTIVITY = "Activity";
        private const string SILENCE = "Silence";
        private bool SQL = false;
        private void RedrawScanState()
        {
            scanList.Items.Clear();
            foreach (var scanRow in Sniffer.ScanState)
            {
                scanList.Items.Add(new ListViewItem(scanRow)
                {
                    BackColor = Color.Black,
                    ForeColor = Color.White,
                    Font = new Font(FontFamily.GenericSansSerif, 12)
                });
            }
            if (scanList.Items.Count > 0)
            {
                scanList.Items[0].ForeColor = SQL ? Color.Lime : Color.White;
                scanList.Items[0].Font = new Font(FontFamily.GenericSansSerif, SQL ? 14 : 12);
            }
            if (!SQL)
            {
                ParrotLevel(0);
            }
        }
        internal static SBEPSniffer Sniffer = new SBEPSniffer();
        private int minLevel = 190;
        private int maxLevel = 250;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Task monitor;
        static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return from2 + (value - from1) * (to2 - from2) / (to1 - from1);
        }
        private void OpenComPort()
        {
            var portName = comPortsBox.SelectedItem?.ToString();

            handleActive = Debounce(() =>
             {
                 if (Transmitter.ChannelBusy)
                 {
                     Invoke(new Action(() =>
                     {
                         // activityLabel.Text = ACTIVITY;
                     }));
                     LastActivity = DateTime.Now;
                 }
             });
            if (!string.IsNullOrEmpty(portName))
            {
                timer.Interval = 10000;
                timer.Tick += (s, e) => RedrawScanState();
                timer.Start();
                rawLog.Text = "";

                Sniffer.SMeter += (s, e) =>
                {
                    minLevel = Math.Min(minLevel, e);
                    maxLevel = Math.Max(maxLevel, e);
                    if (SQL)
                    {
                        Invoke(new Action(() =>
                        {
                            var level = Convert.ToInt32(Remap(e, minLevel, maxLevel, 0, ParrotsCount));
                            ParrotLevel(level);
                            rawSmeter.Value = e;
                        }));
                    }

                };
                Sniffer.RawUpdate += (s, e) => Invoke(new Action(() =>
                {
                    rawLog.Text = e + "\r\n" + rawLog.Text;
                    if (rawLog.Text.Length > 5000)
                    {
                        rawLog.Text = rawLog.Text.Substring(0, 4000);
                    }
                }));
                Sniffer.DisplayChange += (s, e) => Invoke(new Action(() =>
                {
                    RedrawScanState();
                }));
                Sniffer.SquelchUpdate += (s, busy) => Invoke(new Action(() =>
                {
                    SQL = busy;
                    RedrawScanState();
                }));
                Transmitter.ComPort = new SerialPort(portName, 115200);
                //Transmitter.ComPort.DataReceived += (s, e) =>
                //{
                //    var data = Transmitter.ComPort.ReadExisting();
                //    Invoke(new Action(() => activityLabel.Text = data));
                //    return;
                //    if (data.Contains("activity"))
                //    {
                //        Invoke(new Action(() => activityLabel.Text = data));
                //        Transmitter.ChannelBusy = true;
                //        handleActive();
                //        RecordSoundIfNeed();
                //    }
                //    else if (data.Contains("silence"))
                //    {
                //        Transmitter.ChannelBusy = false;
                //        Invoke(new Action(() =>
                //        {
                //            if (activityLabel.Text == SQUELCH_OPEN)
                //            {
                //                PttClickCounter++;
                //            }
                //            activityLabel.Text = SILENCE;
                //        }));
                //    }
                //};
                Transmitter.ComPort.Open();
                monitor = Task.Run(() =>
                {
                    Sniffer.Subscribe(GetBytes(Transmitter.ComPort));
                });
            }
        }
        private static IEnumerable<byte[]> GetBytes(SerialPort port)
        {
            List<byte> batch = new();
            while (port.IsOpen)
            {
                var b = port.BaseStream.ReadByte();
                var byteVal = (byte)b;
                batch.Add(byteVal);
                if (b == -1) { yield break; }
                if (byteVal == 0x50)
                {
                    yield return batch.ToArray();
                    batch.Clear();
                }
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

        private ImageStreamingServer streamingServer;
        private void startWebCamServerButton_Click(object sender, EventArgs e)
        {
            startWebCamServerButton.Text = "Started";
            startWebCamServerButton.Enabled = false;
            streamingServer = new ImageStreamingServer(screenPanel);
            streamingServer.Start(Convert.ToInt32(httpPortNumberBox.Value));
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            Transmitter.ComPort?.Write("p3");
        }
    }
}
