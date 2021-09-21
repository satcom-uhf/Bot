
namespace SatcomPiratesBot
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cameraBox = new System.Windows.Forms.PictureBox();
            this.tabs = new System.Windows.Forms.TabControl();
            this.webcamPage = new System.Windows.Forms.TabPage();
            this.startWebCam = new System.Windows.Forms.Button();
            this.refreshCameras = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.camerasDropDown = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.cameraBox)).BeginInit();
            this.tabs.SuspendLayout();
            this.webcamPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraBox
            // 
            this.cameraBox.Image = ((System.Drawing.Image)(resources.GetObject("cameraBox.Image")));
            this.cameraBox.Location = new System.Drawing.Point(8, 51);
            this.cameraBox.Name = "cameraBox";
            this.cameraBox.Size = new System.Drawing.Size(320, 240);
            this.cameraBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cameraBox.TabIndex = 0;
            this.cameraBox.TabStop = false;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.webcamPage);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(624, 399);
            this.tabs.TabIndex = 1;
            // 
            // webcamPage
            // 
            this.webcamPage.Controls.Add(this.startWebCam);
            this.webcamPage.Controls.Add(this.refreshCameras);
            this.webcamPage.Controls.Add(this.label1);
            this.webcamPage.Controls.Add(this.camerasDropDown);
            this.webcamPage.Controls.Add(this.cameraBox);
            this.webcamPage.Location = new System.Drawing.Point(4, 24);
            this.webcamPage.Name = "webcamPage";
            this.webcamPage.Padding = new System.Windows.Forms.Padding(3);
            this.webcamPage.Size = new System.Drawing.Size(616, 371);
            this.webcamPage.TabIndex = 0;
            this.webcamPage.Text = "Camera";
            this.webcamPage.UseVisualStyleBackColor = true;
            // 
            // startWebCam
            // 
            this.startWebCam.Location = new System.Drawing.Point(253, 22);
            this.startWebCam.Name = "startWebCam";
            this.startWebCam.Size = new System.Drawing.Size(75, 23);
            this.startWebCam.TabIndex = 4;
            this.startWebCam.Text = "Connect";
            this.startWebCam.UseVisualStyleBackColor = true;
            this.startWebCam.Click += new System.EventHandler(this.startWebCam_Click);
            // 
            // refreshCameras
            // 
            this.refreshCameras.Location = new System.Drawing.Point(180, 22);
            this.refreshCameras.Name = "refreshCameras";
            this.refreshCameras.Size = new System.Drawing.Size(57, 23);
            this.refreshCameras.TabIndex = 3;
            this.refreshCameras.Text = "Refresh";
            this.refreshCameras.UseVisualStyleBackColor = true;
            this.refreshCameras.Click += new System.EventHandler(this.refreshCameras_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select camera";
            // 
            // camerasDropDown
            // 
            this.camerasDropDown.FormattingEnabled = true;
            this.camerasDropDown.Location = new System.Drawing.Point(8, 22);
            this.camerasDropDown.Name = "camerasDropDown";
            this.camerasDropDown.Size = new System.Drawing.Size(166, 23);
            this.camerasDropDown.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satcom pirates bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cameraBox)).EndInit();
            this.tabs.ResumeLayout(false);
            this.webcamPage.ResumeLayout(false);
            this.webcamPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraBox;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage webcamPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox camerasDropDown;
        private System.Windows.Forms.Button refreshCameras;
        private System.Windows.Forms.Button startWebCam;
    }
}

