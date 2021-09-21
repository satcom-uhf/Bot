using DirectShowLib;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCameras();
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
                    }
                    catch { }
                }
                Invoke(new Action(SetImage));
            }
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
                Close();
            }
        }
    }
}
