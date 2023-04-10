namespace RemoteAudio.Client.Windows
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.hostListView = new System.Windows.Forms.ListView();
            this.settingsButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.deviceInfo = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.searchButton = new System.Windows.Forms.Button();
            this.directConnectButton = new System.Windows.Forms.Button();
            this.hostIPAddress = new System.Windows.Forms.TextBox();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // hostListView
            // 
            this.hostListView.Location = new System.Drawing.Point(679, 12);
            this.hostListView.MultiSelect = false;
            this.hostListView.Name = "hostListView";
            this.hostListView.Size = new System.Drawing.Size(317, 366);
            this.hostListView.TabIndex = 0;
            this.hostListView.UseCompatibleStateImageBehavior = false;
            // 
            // settingsButton
            // 
            this.settingsButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingsButton.Location = new System.Drawing.Point(12, 12);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(75, 29);
            this.settingsButton.TabIndex = 1;
            this.settingsButton.Text = "설정";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusText});
            this.statusStrip.Location = new System.Drawing.Point(0, 451);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusText
            // 
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(71, 17);
            this.statusText.Text = "연결 준비됨";
            // 
            // deviceInfo
            // 
            this.deviceInfo.AutoSize = true;
            this.deviceInfo.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deviceInfo.Location = new System.Drawing.Point(12, 44);
            this.deviceInfo.Name = "deviceInfo";
            this.deviceInfo.Size = new System.Drawing.Size(104, 32);
            this.deviceInfo.TabIndex = 3;
            this.deviceInfo.Text = "PC 정보:";
            // 
            // connectButton
            // 
            this.connectButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.connectButton.Location = new System.Drawing.Point(841, 419);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(155, 29);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "연결";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.disconnectButton.Location = new System.Drawing.Point(679, 419);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(155, 29);
            this.disconnectButton.TabIndex = 5;
            this.disconnectButton.Text = "연결 해제";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "테스트 알림입니다.";
            this.notifyIcon.BalloonTipTitle = "Remote Audio Client";
            this.notifyIcon.ContextMenuStrip = this.ctx;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "알림";
            this.notifyIcon.Visible = true;
            // 
            // ctx
            // 
            this.ctx.Name = "ctx";
            this.ctx.Size = new System.Drawing.Size(61, 4);
            // 
            // searchButton
            // 
            this.searchButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchButton.Location = new System.Drawing.Point(679, 384);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(317, 29);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "호스트 검색";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // directConnectButton
            // 
            this.directConnectButton.Enabled = false;
            this.directConnectButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.directConnectButton.Location = new System.Drawing.Point(209, 419);
            this.directConnectButton.Name = "directConnectButton";
            this.directConnectButton.Size = new System.Drawing.Size(155, 29);
            this.directConnectButton.TabIndex = 7;
            this.directConnectButton.Text = "직접 연결";
            this.directConnectButton.UseVisualStyleBackColor = true;
            this.directConnectButton.Click += new System.EventHandler(this.directConnectButton_Click);
            // 
            // hostIPAddress
            // 
            this.hostIPAddress.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.hostIPAddress.Location = new System.Drawing.Point(12, 419);
            this.hostIPAddress.MaxLength = 100;
            this.hostIPAddress.Name = "hostIPAddress";
            this.hostIPAddress.PlaceholderText = "IP 주소";
            this.hostIPAddress.Size = new System.Drawing.Size(191, 29);
            this.hostIPAddress.TabIndex = 8;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.hostIPAddress);
            this.Controls.Add(this.directConnectButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.deviceInfo);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.hostListView);
            this.Name = "MainWindow";
            this.Text = "Remote Audio Client";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView hostListView;
        private Button settingsButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusText;
        private Label deviceInfo;
        private Button connectButton;
        private Button disconnectButton;
        private NotifyIcon notifyIcon;
        private Button searchButton;
        private ContextMenuStrip ctx;
        private Button directConnectButton;
        private TextBox hostIPAddress;
    }
}