using DirectShowLib;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Serilog;
using System;
using System.Data;
using System.IO;
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
        public static int PttClickCounter;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCameras();
            LoadSettings();
            ToggleOcrSettings();
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
        internal static ConfigModel Config { get; private set; }

        private void LoadSettings()
        {
            try
            {
                Config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("preferences.json"));
                h1.Value = Config.StartHSV.H;
                s1.Value = Config.StartHSV.S;
                v1.Value = Config.StartHSV.V;
                h2.Value = Config.EndHSV.H;
                s2.Value = Config.EndHSV.S;
                v2.Value = Config.EndHSV.V;
                telegramTokenBox.Text = Config.TelegramToken;
                n2yoApiKeyBox.Text = Config.N2YOApiKey;
                sstvPathBox.Text = Config.SSTVPath;
                ocrSpaceKeyBox.Text = Config.OcrSpaceKey;
            }
            catch
            {
                // use default values
            }
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
                Config.OcrSpaceKey = ocrSpaceKeyBox.Text;
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
            await Telegram.Start(Config.TelegramToken, cts.Token);
            runTelegramButton.Text = "Started";
            runTelegramButton.Enabled = false;
        }
    }
}
