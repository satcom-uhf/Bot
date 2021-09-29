
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
            this.radioPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comPortsBox = new System.Windows.Forms.ComboBox();
            this.refreshPortsButton = new System.Windows.Forms.Button();
            this.connectComPortButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.camerasDropDown = new System.Windows.Forms.ComboBox();
            this.refreshCameras = new System.Windows.Forms.Button();
            this.startWebCam = new System.Windows.Forms.Button();
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
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.audioLevel = new System.Windows.Forms.ProgressBar();
            this.mainGroupBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.sstvChannelBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.testTelegramButton = new System.Windows.Forms.Button();
            this.setSstvPathButton = new System.Windows.Forms.Button();
            this.sstvPathBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.apiKeysSaveButton = new System.Windows.Forms.Button();
            this.n2yoApiKeyBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.telegramTokenBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logPage = new System.Windows.Forms.TabPage();
            this.simpleLogTextBox1 = new Serilog.Sinks.WinForms.SimpleLogTextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.runTelegramButton = new System.Windows.Forms.Button();
            this.activityLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cameraBox)).BeginInit();
            this.tabs.SuspendLayout();
            this.radioPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.mask2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.v2)).BeginInit();
            this.mask1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.s1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.h1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.v1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maskBox)).BeginInit();
            this.settingsPage.SuspendLayout();
            this.logPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraBox
            // 
            this.cameraBox.Image = ((System.Drawing.Image)(resources.GetObject("cameraBox.Image")));
            this.cameraBox.Location = new System.Drawing.Point(8, 120);
            this.cameraBox.Name = "cameraBox";
            this.cameraBox.Size = new System.Drawing.Size(320, 240);
            this.cameraBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cameraBox.TabIndex = 0;
            this.cameraBox.TabStop = false;
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.radioPage);
            this.tabs.Controls.Add(this.settingsPage);
            this.tabs.Controls.Add(this.logPage);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Multiline = true;
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(624, 399);
            this.tabs.TabIndex = 1;
            // 
            // radioPage
            // 
            this.radioPage.Controls.Add(this.groupBox2);
            this.radioPage.Controls.Add(this.groupBox1);
            this.radioPage.Controls.Add(this.ocrSettings);
            this.radioPage.Controls.Add(this.mask2);
            this.radioPage.Controls.Add(this.mask1);
            this.radioPage.Controls.Add(this.maskBox);
            this.radioPage.Controls.Add(this.cameraBox);
            this.radioPage.Location = new System.Drawing.Point(4, 24);
            this.radioPage.Name = "radioPage";
            this.radioPage.Padding = new System.Windows.Forms.Padding(3);
            this.radioPage.Size = new System.Drawing.Size(616, 371);
            this.radioPage.TabIndex = 0;
            this.radioPage.Text = "Radio integration";
            this.radioPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comPortsBox);
            this.groupBox2.Controls.Add(this.refreshPortsButton);
            this.groupBox2.Controls.Add(this.connectComPortButton);
            this.groupBox2.Location = new System.Drawing.Point(8, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 41);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "COM port";
            // 
            // comPortsBox
            // 
            this.comPortsBox.FormattingEnabled = true;
            this.comPortsBox.Location = new System.Drawing.Point(6, 15);
            this.comPortsBox.Name = "comPortsBox";
            this.comPortsBox.Size = new System.Drawing.Size(166, 23);
            this.comPortsBox.TabIndex = 1;
            // 
            // refreshPortsButton
            // 
            this.refreshPortsButton.Location = new System.Drawing.Point(178, 15);
            this.refreshPortsButton.Name = "refreshPortsButton";
            this.refreshPortsButton.Size = new System.Drawing.Size(56, 23);
            this.refreshPortsButton.TabIndex = 3;
            this.refreshPortsButton.Text = "Refresh";
            this.refreshPortsButton.UseVisualStyleBackColor = true;
            this.refreshPortsButton.Click += new System.EventHandler(this.refreshPortsButton_Click);
            // 
            // connectComPortButton
            // 
            this.connectComPortButton.Location = new System.Drawing.Point(239, 15);
            this.connectComPortButton.Name = "connectComPortButton";
            this.connectComPortButton.Size = new System.Drawing.Size(75, 23);
            this.connectComPortButton.TabIndex = 4;
            this.connectComPortButton.Text = "Connect";
            this.connectComPortButton.UseVisualStyleBackColor = true;
            this.connectComPortButton.Click += new System.EventHandler(this.connectComPortButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.camerasDropDown);
            this.groupBox1.Controls.Add(this.refreshCameras);
            this.groupBox1.Controls.Add(this.startWebCam);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 41);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // camerasDropDown
            // 
            this.camerasDropDown.FormattingEnabled = true;
            this.camerasDropDown.Location = new System.Drawing.Point(6, 15);
            this.camerasDropDown.Name = "camerasDropDown";
            this.camerasDropDown.Size = new System.Drawing.Size(166, 23);
            this.camerasDropDown.TabIndex = 1;
            // 
            // refreshCameras
            // 
            this.refreshCameras.Location = new System.Drawing.Point(178, 15);
            this.refreshCameras.Name = "refreshCameras";
            this.refreshCameras.Size = new System.Drawing.Size(56, 23);
            this.refreshCameras.TabIndex = 3;
            this.refreshCameras.Text = "Refresh";
            this.refreshCameras.UseVisualStyleBackColor = true;
            this.refreshCameras.Click += new System.EventHandler(this.refreshCameras_Click);
            // 
            // startWebCam
            // 
            this.startWebCam.Location = new System.Drawing.Point(239, 15);
            this.startWebCam.Name = "startWebCam";
            this.startWebCam.Size = new System.Drawing.Size(75, 23);
            this.startWebCam.TabIndex = 4;
            this.startWebCam.Text = "Connect";
            this.startWebCam.UseVisualStyleBackColor = true;
            this.startWebCam.Click += new System.EventHandler(this.startWebCam_Click);
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
            this.maskBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.maskBox.TabIndex = 5;
            this.maskBox.TabStop = false;
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.mainGroupBox);
            this.settingsPage.Controls.Add(this.label12);
            this.settingsPage.Controls.Add(this.sstvChannelBox);
            this.settingsPage.Controls.Add(this.label11);
            this.settingsPage.Controls.Add(this.testTelegramButton);
            this.settingsPage.Controls.Add(this.setSstvPathButton);
            this.settingsPage.Controls.Add(this.sstvPathBox);
            this.settingsPage.Controls.Add(this.label9);
            this.settingsPage.Controls.Add(this.apiKeysSaveButton);
            this.settingsPage.Controls.Add(this.n2yoApiKeyBox);
            this.settingsPage.Controls.Add(this.label8);
            this.settingsPage.Controls.Add(this.telegramTokenBox);
            this.settingsPage.Controls.Add(this.label1);
            this.settingsPage.Location = new System.Drawing.Point(4, 24);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsPage.Size = new System.Drawing.Size(616, 371);
            this.settingsPage.TabIndex = 1;
            this.settingsPage.Text = "Settings";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // audioLevel
            // 
            this.audioLevel.Location = new System.Drawing.Point(189, 406);
            this.audioLevel.Name = "audioLevel";
            this.audioLevel.Size = new System.Drawing.Size(229, 23);
            this.audioLevel.TabIndex = 15;
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Location = new System.Drawing.Point(143, 158);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(322, 23);
            this.mainGroupBox.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 15);
            this.label12.TabIndex = 13;
            this.label12.Text = "Main discussion group";
            // 
            // sstvChannelBox
            // 
            this.sstvChannelBox.Location = new System.Drawing.Point(143, 130);
            this.sstvChannelBox.Name = "sstvChannelBox";
            this.sstvChannelBox.Size = new System.Drawing.Size(322, 23);
            this.sstvChannelBox.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 133);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = "SSTV && Voice channel";
            // 
            // testTelegramButton
            // 
            this.testTelegramButton.Location = new System.Drawing.Point(471, 22);
            this.testTelegramButton.Name = "testTelegramButton";
            this.testTelegramButton.Size = new System.Drawing.Size(75, 23);
            this.testTelegramButton.TabIndex = 8;
            this.testTelegramButton.Text = "Test";
            this.testTelegramButton.UseVisualStyleBackColor = true;
            this.testTelegramButton.Click += new System.EventHandler(this.testTelegramButton_Click);
            // 
            // setSstvPathButton
            // 
            this.setSstvPathButton.Location = new System.Drawing.Point(471, 74);
            this.setSstvPathButton.Name = "setSstvPathButton";
            this.setSstvPathButton.Size = new System.Drawing.Size(75, 23);
            this.setSstvPathButton.TabIndex = 7;
            this.setSstvPathButton.Text = "...";
            this.setSstvPathButton.UseVisualStyleBackColor = true;
            this.setSstvPathButton.Click += new System.EventHandler(this.setSstvPathButton_Click);
            // 
            // sstvPathBox
            // 
            this.sstvPathBox.Enabled = false;
            this.sstvPathBox.Location = new System.Drawing.Point(143, 74);
            this.sstvPathBox.Name = "sstvPathBox";
            this.sstvPathBox.Size = new System.Drawing.Size(322, 23);
            this.sstvPathBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "SSTV Path";
            // 
            // apiKeysSaveButton
            // 
            this.apiKeysSaveButton.Location = new System.Drawing.Point(533, 342);
            this.apiKeysSaveButton.Name = "apiKeysSaveButton";
            this.apiKeysSaveButton.Size = new System.Drawing.Size(75, 23);
            this.apiKeysSaveButton.TabIndex = 4;
            this.apiKeysSaveButton.Text = "Apply";
            this.apiKeysSaveButton.UseVisualStyleBackColor = true;
            this.apiKeysSaveButton.Click += new System.EventHandler(this.apiKeysSaveButton_Click);
            // 
            // n2yoApiKeyBox
            // 
            this.n2yoApiKeyBox.Location = new System.Drawing.Point(143, 48);
            this.n2yoApiKeyBox.Name = "n2yoApiKeyBox";
            this.n2yoApiKeyBox.Size = new System.Drawing.Size(322, 23);
            this.n2yoApiKeyBox.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "N2YO API Key";
            // 
            // telegramTokenBox
            // 
            this.telegramTokenBox.Location = new System.Drawing.Point(143, 22);
            this.telegramTokenBox.Name = "telegramTokenBox";
            this.telegramTokenBox.Size = new System.Drawing.Size(322, 23);
            this.telegramTokenBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Telegram Bot Token";
            // 
            // logPage
            // 
            this.logPage.BackColor = System.Drawing.Color.White;
            this.logPage.Controls.Add(this.simpleLogTextBox1);
            this.logPage.Location = new System.Drawing.Point(4, 24);
            this.logPage.Name = "logPage";
            this.logPage.Size = new System.Drawing.Size(616, 371);
            this.logPage.TabIndex = 2;
            this.logPage.Text = "Log";
            // 
            // simpleLogTextBox1
            // 
            this.simpleLogTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleLogTextBox1.ForContext = "";
            this.simpleLogTextBox1.Location = new System.Drawing.Point(0, 0);
            this.simpleLogTextBox1.LogBorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.simpleLogTextBox1.LogPadding = new System.Windows.Forms.Padding(3);
            this.simpleLogTextBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.simpleLogTextBox1.Name = "simpleLogTextBox1";
            this.simpleLogTextBox1.ReadOnly = false;
            this.simpleLogTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.simpleLogTextBox1.Size = new System.Drawing.Size(616, 371);
            this.simpleLogTextBox1.TabIndex = 0;
            // 
            // runTelegramButton
            // 
            this.runTelegramButton.Location = new System.Drawing.Point(448, 406);
            this.runTelegramButton.Name = "runTelegramButton";
            this.runTelegramButton.Size = new System.Drawing.Size(75, 23);
            this.runTelegramButton.TabIndex = 2;
            this.runTelegramButton.Text = "Start bot";
            this.runTelegramButton.UseVisualStyleBackColor = true;
            this.runTelegramButton.Click += new System.EventHandler(this.runTelegramButton_Click);
            // 
            // activityLabel
            // 
            this.activityLabel.AutoSize = true;
            this.activityLabel.Location = new System.Drawing.Point(18, 413);
            this.activityLabel.Name = "activityLabel";
            this.activityLabel.Size = new System.Drawing.Size(44, 15);
            this.activityLabel.TabIndex = 3;
            this.activityLabel.Text = "Silence";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.audioLevel);
            this.Controls.Add(this.activityLabel);
            this.Controls.Add(this.runTelegramButton);
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
            this.radioPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
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
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            this.logPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraBox;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage radioPage;
        private System.Windows.Forms.TabPage settingsPage;
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
        private System.Windows.Forms.TextBox telegramTokenBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox n2yoApiKeyBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button apiKeysSaveButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox sstvPathBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button setSstvPathButton;
        private System.Windows.Forms.Button testTelegramButton;
        private System.Windows.Forms.TabPage logPage;
        private System.Windows.Forms.Button runTelegramButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comPortsBox;
        private System.Windows.Forms.Button refreshPortsButton;
        private System.Windows.Forms.Button connectComPortButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label activityLabel;
        private Serilog.Sinks.WinForms.SimpleLogTextBox simpleLogTextBox1;
        private System.Windows.Forms.TextBox sstvChannelBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox mainGroupBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ProgressBar audioLevel;
    }
}

