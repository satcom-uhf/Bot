using DirectShowLib;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SatcomPiratesBot
{
    public partial class MainForm : Form
    {
        VideoCapture videoCapture = new VideoCapture();
        CancellationTokenSource cts = new CancellationTokenSource();
        Task capturing;
        static Mat CurrentFrame = new Mat();
        static Mat Mask = new Mat();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCameras();
            LoadOcrSettings();
            ToggleOcrSettings();
        }

        private void LoadOcrSettings()
        {
            try
            {
                var config = JsonConvert.DeserializeObject<ConfigModel>(File.ReadAllText("preferences.json"));
                h1.Value = config.StartHSV.H;
                s1.Value = config.StartHSV.S;
                v1.Value = config.StartHSV.V;
                h2.Value = config.EndHSV.H;
                s2.Value = config.EndHSV.S;
                v2.Value = config.EndHSV.V;
            }
            catch
            {
                // use default values
            }
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
                SaveOcrSettings();
                Close();
            }
        }

        private void SaveOcrSettings()
        {
            try
            {
                var config = new ConfigModel();
                config.StartHSV.H = Convert.ToInt32(h1.Value);
                config.StartHSV.S = Convert.ToInt32(s1.Value);
                config.StartHSV.V = Convert.ToInt32(v1.Value);
                config.EndHSV.H = Convert.ToInt32(h2.Value);
                config.EndHSV.S = Convert.ToInt32(s2.Value);
                config.EndHSV.V = Convert.ToInt32(v2.Value);
                File.WriteAllText("preferences.json", JsonConvert.SerializeObject(config));                
            }
            catch
            {
                // No problem
            }
        }

        private void ocrSettings_Click(object sender, EventArgs e) => ToggleOcrSettings();

        private bool ocrDebug = true;
        private void ToggleOcrSettings()
        {
            ocrDebug = !ocrDebug;
            mask1.Enabled = ocrDebug;
            mask2.Enabled = ocrDebug;
        }
    }
}
