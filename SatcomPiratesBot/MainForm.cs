using DirectShowLib;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
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
        VideoCapture videoCapture = new VideoCapture();
        CancellationTokenSource cts = new CancellationTokenSource();
        Task capturing;
        public static Mat CurrentFrame = new Mat();
        public static Mat Mask = new Mat();
        public static DateTime LastActivity;
        public static int PttClickCounter { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCameras();
            LoadPorts();
            LoadSettings();
            ToggleOcrSettings();
            CurrentFrame = BitmapConverter.ToMat(new Bitmap(cameraBox.Image));
        }

        private void LoadPorts()
        {
            comPortsBox.Items.Clear();
            comPortsBox.Items.AddRange(SerialPort.GetPortNames());
        }

        private void LoadCameras()
        {
            camerasDropDown.Items.Clear();
            camerasDropDown.Items.AddRange(
                DsDevice
                .GetDevicesOfCat(FilterCategory.VideoInputDevice)
                .Select(x => x.Name)
                .ToArray()
            );
            camerasDropDown.SelectedIndex = 0;
        }

        private void startWebCam_Click(object sender, EventArgs e)
        {
            refreshCameras.Enabled = !refreshCameras.Enabled;
            camerasDropDown.Enabled = refreshCameras.Enabled;
            startWebCam.Text = refreshCameras.Enabled ? "Connect" : "Disconnect";
            if (refreshCameras.Enabled)
            {
                videoCapture.Release();
            }
            else
            {
                videoCapture.Open(camerasDropDown.SelectedIndex);
                capturing = Task.Run(() => CameraCapturing(cts.Token), cts.Token);
            }

        }

        private void CameraCapturing(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested && videoCapture.IsOpened())
            {
                videoCapture.Read(CurrentFrame);
                void SetImage()
                {
                    try
                    {
                        cameraBox.Image?.Dispose();
                        cameraBox.Image = BitmapConverter.ToBitmap(CurrentFrame);
                        if (ocrDebug)
                        {
                            SetMask();
                        }
                    }
                    catch
                    {
                        // Ignored
                    }
                }
                Invoke(new Action(SetImage));
            }
        }

        private void SetMask()
        {
            Mat hsv = new Mat();
            Cv2.CvtColor(CurrentFrame, hsv, ColorConversionCodes.BGR2HSV);
            static Vec3i build(NumericUpDown h, NumericUpDown s, NumericUpDown v)
            {
                static int I(NumericUpDown n) => Convert.ToInt32(n.Value);
                return new Vec3i(I(h), I(s), I(v));
            }
            var start = build(h1, s1, v1);
            var end = build(h2, s2, v2);
            Cv2.InRange(hsv, start, end, Mask);
            maskBox.Image?.Dispose();
            maskBox.Image = BitmapConverter.ToBitmap(Mask);
        }


        private void refreshCameras_Click(object sender, EventArgs e) => LoadCameras();

        private bool closingHack = true;
        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closingHack)
            {
                videoCapture.Release();
                cts.Cancel();
                e.Cancel = true;
                if (capturing != null)
                {
                    await capturing;
                }
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
            h1.Value = Config.StartHSV.H;
            s1.Value = Config.StartHSV.S;
            v1.Value = Config.StartHSV.V;
            h2.Value = Config.EndHSV.H;
            s2.Value = Config.EndHSV.S;
            v2.Value = Config.EndHSV.V;
            telegramTokenBox.Text = Config.TelegramToken;
            n2yoApiKeyBox.Text = Config.N2YOApiKey;
            sstvPathBox.Text = Config.SSTVPath;
            sstvChannelBox.Text = Config.SSTVChannel;
            mainGroupBox.Text = Config.MainDiscussuionGroup;
            dtmfCodeBox.Text = Config.DTMFCode;
        }
        private void SaveSettings()
        {
            try
            {
                Config.StartHSV.H = Convert.ToInt32(h1.Value);
                Config.StartHSV.S = Convert.ToInt32(s1.Value);
                Config.StartHSV.V = Convert.ToInt32(v1.Value);
                Config.EndHSV.H = Convert.ToInt32(h2.Value);
                Config.EndHSV.S = Convert.ToInt32(s2.Value);
                Config.EndHSV.V = Convert.ToInt32(v2.Value);
                Config.TelegramToken = telegramTokenBox.Text;
                Config.N2YOApiKey = n2yoApiKeyBox.Text;
                Config.SSTVPath = sstvPathBox.Text;
                Config.SSTVChannel = sstvChannelBox.Text;
                Config.MainDiscussuionGroup = mainGroupBox.Text;
                Config.DTMFCode = dtmfCodeBox.Text;
                File.WriteAllText("preferences.json", JsonConvert.SerializeObject(Config));
            }
            catch
            {
                // No problem
            }
        }
        #endregion
        private void ocrSettings_Click(object sender, EventArgs e) => ToggleOcrSettings();

        private bool ocrDebug = true;


        private void ToggleOcrSettings()
        {
            ocrDebug = !ocrDebug;
            mask1.Enabled = ocrDebug;
            mask2.Enabled = ocrDebug;
        }

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
                }
                finally
                {
                    Transmitter.Vox.Stop();
                }
            };
            DTMF.StartDetection(Config);
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
        private void OpenComPort()
        {
            var portName = comPortsBox.SelectedItem?.ToString();
            handleActive = Debounce(() =>
             {
                 if (Transmitter.ChannelBusy)
                 {
                     try
                     {
                         Invoke(new Action(() =>
                         {
                             activityLabel.Text = ACTIVITY;
                             SetMask();
                         }));
                         LastActivity = DateTime.Now;

                     }
                     catch (Exception ex)
                     {
                         Log.Error(ex, "");
                     }
                 }
             });
            if (!string.IsNullOrEmpty(portName))
            {
                Transmitter.ComPort = new SerialPort(portName);
                Transmitter.ComPort.BaudRate = 9600;
                Transmitter.ComPort.DataReceived += (s, e) =>
                {
                    var data = Transmitter.ComPort.ReadExisting();
                    if (data.Contains("activity"))
                    {
                        Invoke(new Action(() => activityLabel.Text = SQUELCH_OPEN));
                        Transmitter.ChannelBusy = true;
                        handleActive();
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
    }
}
