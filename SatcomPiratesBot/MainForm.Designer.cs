
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "072 255.550 active"}, -1, System.Drawing.Color.Lime, System.Drawing.Color.Empty, new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "172 234.567 00:00:00 ago"}, -1, System.Drawing.SystemColors.Info, System.Drawing.Color.Empty, new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point));
            this.tabs = new System.Windows.Forms.TabControl();
            this.radioPage = new System.Windows.Forms.TabPage();
            this.waveformPainter1 = new NAudio.Gui.WaveformPainter();
            this.scanButton = new System.Windows.Forms.Button();
            this.rawLog = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startWebCamServerButton = new System.Windows.Forms.Button();
            this.httpPortNumberBox = new System.Windows.Forms.NumericUpDown();
            this.screenPanel = new System.Windows.Forms.Panel();
            this.colorTv = new System.Windows.Forms.PictureBox();
            this.scanLabel = new System.Windows.Forms.Label();
            this.sMeter = new System.Windows.Forms.RichTextBox();
            this.scanList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comPortsBox = new System.Windows.Forms.ComboBox();
            this.refreshPortsButton = new System.Windows.Forms.Button();
            this.connectComPortButton = new System.Windows.Forms.Button();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.witAiTokenBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtmfCodeBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
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
            this.logTab = new System.Windows.Forms.TabPage();
            this.gridLog1 = new Serilog.Sinks.WinForms.GridLog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.runTelegramButton = new System.Windows.Forms.Button();
            this.rawSmeter = new System.Windows.Forms.ProgressBar();
            this.clickCounterLabel = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.radioPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.httpPortNumberBox)).BeginInit();
            this.screenPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorTv)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.logTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.radioPage);
            this.tabs.Controls.Add(this.settingsPage);
            this.tabs.Controls.Add(this.logTab);
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
            this.radioPage.Controls.Add(this.waveformPainter1);
            this.radioPage.Controls.Add(this.scanButton);
            this.radioPage.Controls.Add(this.rawLog);
            this.radioPage.Controls.Add(this.groupBox1);
            this.radioPage.Controls.Add(this.screenPanel);
            this.radioPage.Controls.Add(this.groupBox2);
            this.radioPage.Location = new System.Drawing.Point(4, 24);
            this.radioPage.Name = "radioPage";
            this.radioPage.Padding = new System.Windows.Forms.Padding(3);
            this.radioPage.Size = new System.Drawing.Size(616, 371);
            this.radioPage.TabIndex = 0;
            this.radioPage.Text = "Radio integration";
            this.radioPage.UseVisualStyleBackColor = true;
            // 
            // waveformPainter1
            // 
            this.waveformPainter1.Location = new System.Drawing.Point(384, 73);
            this.waveformPainter1.Name = "waveformPainter1";
            this.waveformPainter1.Size = new System.Drawing.Size(135, 52);
            this.waveformPainter1.TabIndex = 29;
            this.waveformPainter1.Text = "waveformPainter1";
            // 
            // scanButton
            // 
            this.scanButton.Location = new System.Drawing.Point(384, 25);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(75, 23);
            this.scanButton.TabIndex = 28;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // rawLog
            // 
            this.rawLog.Location = new System.Drawing.Point(335, 150);
            this.rawLog.Multiline = true;
            this.rawLog.Name = "rawLog";
            this.rawLog.Size = new System.Drawing.Size(273, 215);
            this.rawLog.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.startWebCamServerButton);
            this.groupBox1.Controls.Add(this.httpPortNumberBox);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 49);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP Camera Server";
            // 
            // startWebCamServerButton
            // 
            this.startWebCamServerButton.Location = new System.Drawing.Point(239, 19);
            this.startWebCamServerButton.Name = "startWebCamServerButton";
            this.startWebCamServerButton.Size = new System.Drawing.Size(75, 23);
            this.startWebCamServerButton.TabIndex = 5;
            this.startWebCamServerButton.Text = "Start";
            this.startWebCamServerButton.UseVisualStyleBackColor = true;
            this.startWebCamServerButton.Click += new System.EventHandler(this.startWebCamServerButton_Click);
            // 
            // httpPortNumberBox
            // 
            this.httpPortNumberBox.Location = new System.Drawing.Point(6, 21);
            this.httpPortNumberBox.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.httpPortNumberBox.Name = "httpPortNumberBox";
            this.httpPortNumberBox.Size = new System.Drawing.Size(166, 23);
            this.httpPortNumberBox.TabIndex = 0;
            this.httpPortNumberBox.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // screenPanel
            // 
            this.screenPanel.BackColor = System.Drawing.Color.Black;
            this.screenPanel.Controls.Add(this.clickCounterLabel);
            this.screenPanel.Controls.Add(this.colorTv);
            this.screenPanel.Controls.Add(this.scanLabel);
            this.screenPanel.Controls.Add(this.sMeter);
            this.screenPanel.Controls.Add(this.scanList);
            this.screenPanel.Location = new System.Drawing.Point(8, 150);
            this.screenPanel.Name = "screenPanel";
            this.screenPanel.Size = new System.Drawing.Size(320, 200);
            this.screenPanel.TabIndex = 25;
            // 
            // colorTv
            // 
            this.colorTv.Image = ((System.Drawing.Image)(resources.GetObject("colorTv.Image")));
            this.colorTv.Location = new System.Drawing.Point(264, 145);
            this.colorTv.Name = "colorTv";
            this.colorTv.Size = new System.Drawing.Size(47, 52);
            this.colorTv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.colorTv.TabIndex = 3;
            this.colorTv.TabStop = false;
            // 
            // scanLabel
            // 
            this.scanLabel.AutoSize = true;
            this.scanLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.scanLabel.ForeColor = System.Drawing.Color.Lime;
            this.scanLabel.Location = new System.Drawing.Point(237, 12);
            this.scanLabel.Name = "scanLabel";
            this.scanLabel.Size = new System.Drawing.Size(74, 21);
            this.scanLabel.TabIndex = 2;
            this.scanLabel.Text = "Scan: ON";
            // 
            // sMeter
            // 
            this.sMeter.BackColor = System.Drawing.Color.Black;
            this.sMeter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sMeter.Location = new System.Drawing.Point(45, 154);
            this.sMeter.Name = "sMeter";
            this.sMeter.Size = new System.Drawing.Size(213, 25);
            this.sMeter.TabIndex = 1;
            this.sMeter.Text = "";
            // 
            // scanList
            // 
            this.scanList.BackColor = System.Drawing.Color.Black;
            this.scanList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.scanList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.scanList.HideSelection = false;
            this.scanList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.scanList.Location = new System.Drawing.Point(5, 12);
            this.scanList.Name = "scanList";
            this.scanList.Size = new System.Drawing.Size(228, 136);
            this.scanList.TabIndex = 0;
            this.scanList.UseCompatibleStateImageBehavior = false;
            this.scanList.View = System.Windows.Forms.View.List;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 280;
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
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.witAiTokenBox);
            this.settingsPage.Controls.Add(this.label13);
            this.settingsPage.Controls.Add(this.dtmfCodeBox);
            this.settingsPage.Controls.Add(this.label10);
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
            // witAiTokenBox
            // 
            this.witAiTokenBox.Location = new System.Drawing.Point(143, 77);
            this.witAiTokenBox.Name = "witAiTokenBox";
            this.witAiTokenBox.Size = new System.Drawing.Size(322, 23);
            this.witAiTokenBox.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(47, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 15);
            this.label13.TabIndex = 17;
            this.label13.Text = "WIT AI Token";
            // 
            // dtmfCodeBox
            // 
            this.dtmfCodeBox.Location = new System.Drawing.Point(143, 135);
            this.dtmfCodeBox.Name = "dtmfCodeBox";
            this.dtmfCodeBox.Size = new System.Drawing.Size(322, 23);
            this.dtmfCodeBox.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(58, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 15);
            this.label10.TabIndex = 15;
            this.label10.Text = "DTMF Code";
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Location = new System.Drawing.Point(143, 192);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(322, 23);
            this.mainGroupBox.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 195);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 15);
            this.label12.TabIndex = 13;
            this.label12.Text = "Primary group";
            // 
            // sstvChannelBox
            // 
            this.sstvChannelBox.Location = new System.Drawing.Point(143, 164);
            this.sstvChannelBox.Name = "sstvChannelBox";
            this.sstvChannelBox.Size = new System.Drawing.Size(322, 23);
            this.sstvChannelBox.TabIndex = 12;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 167);
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
            this.setSstvPathButton.Location = new System.Drawing.Point(471, 108);
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
            this.sstvPathBox.Location = new System.Drawing.Point(143, 108);
            this.sstvPathBox.Name = "sstvPathBox";
            this.sstvPathBox.Size = new System.Drawing.Size(322, 23);
            this.sstvPathBox.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 111);
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
            // logTab
            // 
            this.logTab.Controls.Add(this.gridLog1);
            this.logTab.Location = new System.Drawing.Point(4, 24);
            this.logTab.Name = "logTab";
            this.logTab.Padding = new System.Windows.Forms.Padding(3);
            this.logTab.Size = new System.Drawing.Size(616, 371);
            this.logTab.TabIndex = 2;
            this.logTab.Text = "Log";
            this.logTab.UseVisualStyleBackColor = true;
            // 
            // gridLog1
            // 
            this.gridLog1.Location = new System.Drawing.Point(4, 6);
            this.gridLog1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gridLog1.Name = "gridLog1";
            this.gridLog1.Size = new System.Drawing.Size(605, 248);
            this.gridLog1.TabIndex = 0;
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
            // rawSmeter
            // 
            this.rawSmeter.Location = new System.Drawing.Point(69, 406);
            this.rawSmeter.Maximum = 1023;
            this.rawSmeter.Name = "rawSmeter";
            this.rawSmeter.Size = new System.Drawing.Size(280, 23);
            this.rawSmeter.Step = 1;
            this.rawSmeter.TabIndex = 3;
            // 
            // clickCounterLabel
            // 
            this.clickCounterLabel.AutoSize = true;
            this.clickCounterLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clickCounterLabel.ForeColor = System.Drawing.Color.Lime;
            this.clickCounterLabel.Location = new System.Drawing.Point(237, 36);
            this.clickCounterLabel.Name = "clickCounterLabel";
            this.clickCounterLabel.Size = new System.Drawing.Size(73, 21);
            this.clickCounterLabel.TabIndex = 4;
            this.clickCounterLabel.Text = "0000000";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.rawSmeter);
            this.Controls.Add(this.runTelegramButton);
            this.Controls.Add(this.tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Satcom pirates bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabs.ResumeLayout(false);
            this.radioPage.ResumeLayout(false);
            this.radioPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.httpPortNumberBox)).EndInit();
            this.screenPanel.ResumeLayout(false);
            this.screenPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorTv)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            this.logTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage radioPage;
        private System.Windows.Forms.TabPage settingsPage;
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
        private System.Windows.Forms.Button runTelegramButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comPortsBox;
        private System.Windows.Forms.Button refreshPortsButton;
        private System.Windows.Forms.Button connectComPortButton;
        private System.Windows.Forms.TextBox sstvChannelBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox dtmfCodeBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox witAiTokenBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox mainGroupBox;
        private System.Windows.Forms.Panel screenPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown httpPortNumberBox;
        private System.Windows.Forms.Button startWebCamServerButton;
        private System.Windows.Forms.ListView scanList;
        private System.Windows.Forms.TabPage logTab;
        private Serilog.Sinks.WinForms.GridLog gridLog1;
        private System.Windows.Forms.TextBox rawLog;
        private System.Windows.Forms.Button scanButton;
        private NAudio.Gui.WaveformPainter waveformPainter1;
        private System.Windows.Forms.ProgressBar rawSmeter;
        private System.Windows.Forms.RichTextBox sMeter;
        private System.Windows.Forms.Label scanLabel;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.PictureBox colorTv;
        private System.Windows.Forms.Label clickCounterLabel;
    }
}

