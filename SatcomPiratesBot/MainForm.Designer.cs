﻿
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
            this.ocrSettings = new System.Windows.Forms.Button();
            this.mask2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.s2 = new System.Windows.Forms.NumericUpDown();
            this.h2 = new System.Windows.Forms.NumericUpDown();
            this.v2 = new System.Windows.Forms.NumericUpDown();
            this.mask1 = new System.Windows.Forms.GroupBox();
            this.s1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.h1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.v1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.maskBox = new System.Windows.Forms.PictureBox();
            this.startWebCam = new System.Windows.Forms.Button();
            this.refreshCameras = new System.Windows.Forms.Button();
            this.camerasDropDown = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.cameraBox)).BeginInit();
            this.tabs.SuspendLayout();
            this.webcamPage.SuspendLayout();
            this.mask2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.v2)).BeginInit();
            this.mask1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.v1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskBox)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraBox
            // 
            this.cameraBox.Image = ((System.Drawing.Image)(resources.GetObject("cameraBox.Image")));
            this.cameraBox.Location = new System.Drawing.Point(8, 84);
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
            this.webcamPage.Controls.Add(this.ocrSettings);
            this.webcamPage.Controls.Add(this.mask2);
            this.webcamPage.Controls.Add(this.mask1);
            this.webcamPage.Controls.Add(this.maskBox);
            this.webcamPage.Controls.Add(this.startWebCam);
            this.webcamPage.Controls.Add(this.refreshCameras);
            this.webcamPage.Controls.Add(this.camerasDropDown);
            this.webcamPage.Controls.Add(this.cameraBox);
            this.webcamPage.Location = new System.Drawing.Point(4, 24);
            this.webcamPage.Name = "webcamPage";
            this.webcamPage.Padding = new System.Windows.Forms.Padding(3);
            this.webcamPage.Size = new System.Drawing.Size(616, 371);
            this.webcamPage.TabIndex = 0;
            this.webcamPage.Text = "Camera and machine vision";
            this.webcamPage.UseVisualStyleBackColor = true;
            // 
            // ocrSettings
            // 
            this.ocrSettings.Location = new System.Drawing.Point(344, 5);
            this.ocrSettings.Name = "ocrSettings";
            this.ocrSettings.Size = new System.Drawing.Size(259, 23);
            this.ocrSettings.TabIndex = 21;
            this.ocrSettings.Text = "Machine vision settings";
            this.ocrSettings.UseVisualStyleBackColor = true;
            this.ocrSettings.Click += new System.EventHandler(this.ocrSettings_Click);
            // 
            // mask2
            // 
            this.mask2.Controls.Add(this.label5);
            this.mask2.Controls.Add(this.label6);
            this.mask2.Controls.Add(this.label7);
            this.mask2.Controls.Add(this.s2);
            this.mask2.Controls.Add(this.h2);
            this.mask2.Controls.Add(this.v2);
            this.mask2.Location = new System.Drawing.Point(344, 100);
            this.mask2.Name = "mask2";
            this.mask2.Size = new System.Drawing.Size(259, 61);
            this.mask2.TabIndex = 20;
            this.mask2.TabStop = false;
            this.mask2.Text = "Mask 2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "V";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "S";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "H";
            // 
            // s2
            // 
            this.s2.Location = new System.Drawing.Point(100, 20);
            this.s2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.s2.Name = "s2";
            this.s2.Size = new System.Drawing.Size(64, 23);
            this.s2.TabIndex = 12;
            this.s2.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // h2
            // 
            this.h2.Location = new System.Drawing.Point(20, 20);
            this.h2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.h2.Name = "h2";
            this.h2.Size = new System.Drawing.Size(64, 23);
            this.h2.TabIndex = 14;
            this.h2.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // v2
            // 
            this.v2.Location = new System.Drawing.Point(188, 20);
            this.v2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.v2.Name = "v2";
            this.v2.Size = new System.Drawing.Size(64, 23);
            this.v2.TabIndex = 10;
            this.v2.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // mask1
            // 
            this.mask1.Controls.Add(this.s1);
            this.mask1.Controls.Add(this.label4);
            this.mask1.Controls.Add(this.h1);
            this.mask1.Controls.Add(this.label3);
            this.mask1.Controls.Add(this.v1);
            this.mask1.Controls.Add(this.label2);
            this.mask1.Location = new System.Drawing.Point(344, 38);
            this.mask1.Name = "mask1";
            this.mask1.Size = new System.Drawing.Size(259, 55);
            this.mask1.TabIndex = 19;
            this.mask1.TabStop = false;
            this.mask1.Text = "Mask 1";
            // 
            // s1
            // 
            this.s1.Location = new System.Drawing.Point(101, 18);
            this.s1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.s1.Name = "s1";
            this.s1.Size = new System.Drawing.Size(64, 23);
            this.s1.TabIndex = 13;
            this.s1.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "V";
            // 
            // h1
            // 
            this.h1.Location = new System.Drawing.Point(20, 18);
            this.h1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.h1.Name = "h1";
            this.h1.Size = new System.Drawing.Size(64, 23);
            this.h1.TabIndex = 15;
            this.h1.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "S";
            // 
            // v1
            // 
            this.v1.Location = new System.Drawing.Point(189, 18);
            this.v1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.v1.Name = "v1";
            this.v1.Size = new System.Drawing.Size(64, 23);
            this.v1.TabIndex = 11;
            this.v1.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "H";
            // 
            // maskBox
            // 
            this.maskBox.Location = new System.Drawing.Point(344, 167);
            this.maskBox.Name = "maskBox";
            this.maskBox.Size = new System.Drawing.Size(259, 157);
            this.maskBox.TabIndex = 5;
            this.maskBox.TabStop = false;
            // 
            // startWebCam
            // 
            this.startWebCam.Location = new System.Drawing.Point(253, 5);
            this.startWebCam.Name = "startWebCam";
            this.startWebCam.Size = new System.Drawing.Size(75, 23);
            this.startWebCam.TabIndex = 4;
            this.startWebCam.Text = "Connect";
            this.startWebCam.UseVisualStyleBackColor = true;
            this.startWebCam.Click += new System.EventHandler(this.startWebCam_Click);
            // 
            // refreshCameras
            // 
            this.refreshCameras.Location = new System.Drawing.Point(180, 6);
            this.refreshCameras.Name = "refreshCameras";
            this.refreshCameras.Size = new System.Drawing.Size(57, 23);
            this.refreshCameras.TabIndex = 3;
            this.refreshCameras.Text = "Refresh";
            this.refreshCameras.UseVisualStyleBackColor = true;
            this.refreshCameras.Click += new System.EventHandler(this.refreshCameras_Click);
            // 
            // camerasDropDown
            // 
            this.camerasDropDown.FormattingEnabled = true;
            this.camerasDropDown.Location = new System.Drawing.Point(8, 6);
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
            this.mask2.ResumeLayout(false);
            this.mask2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.h2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.v2)).EndInit();
            this.mask1.ResumeLayout(false);
            this.mask1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.h1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.v1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraBox;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage webcamPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox camerasDropDown;
        private System.Windows.Forms.Button refreshCameras;
        private System.Windows.Forms.Button startWebCam;
        private System.Windows.Forms.PictureBox maskBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown v2;
        private System.Windows.Forms.NumericUpDown v1;
        private System.Windows.Forms.NumericUpDown s2;
        private System.Windows.Forms.NumericUpDown s1;
        private System.Windows.Forms.NumericUpDown h2;
        private System.Windows.Forms.NumericUpDown h1;
        private System.Windows.Forms.GroupBox mask2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox mask1;
        private System.Windows.Forms.Button ocrSettings;
    }
}

