namespace RemoteAudioServer;

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
            this.DeviceInfo = new System.Windows.Forms.Label();
            this.DeviceDescription = new System.Windows.Forms.TextBox();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeviceInfo
            // 
            this.DeviceInfo.AutoSize = true;
            this.DeviceInfo.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeviceInfo.Location = new System.Drawing.Point(12, 44);
            this.DeviceInfo.Name = "DeviceInfo";
            this.DeviceInfo.Size = new System.Drawing.Size(104, 32);
            this.DeviceInfo.TabIndex = 0;
            this.DeviceInfo.Text = "PC 정보:";
            this.DeviceInfo.Click += new System.EventHandler(this.label1_Click);
            // 
            // DeviceDescription
            // 
            this.DeviceDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeviceDescription.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeviceDescription.Location = new System.Drawing.Point(782, 408);
            this.DeviceDescription.Name = "DeviceDescription";
            this.DeviceDescription.PlaceholderText = "설명";
            this.DeviceDescription.Size = new System.Drawing.Size(214, 29);
            this.DeviceDescription.TabIndex = 1;
            // 
            // SettingsButton
            // 
            this.SettingsButton.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SettingsButton.Location = new System.Drawing.Point(12, 12);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(75, 29);
            this.SettingsButton.TabIndex = 2;
            this.SettingsButton.Text = "설정";
            this.SettingsButton.UseVisualStyleBackColor = true;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.StatusStrip.Location = new System.Drawing.Point(0, 451);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1008, 22);
            this.StatusStrip.TabIndex = 3;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(31, 17);
            this.toolStripStatusLabel1.Text = "준비";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1008, 473);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.DeviceDescription);
            this.Controls.Add(this.DeviceInfo);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "MainWindow";
            this.Text = "Remote Audio";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Label DeviceInfo;
    private TextBox DeviceDescription;
    private Button SettingsButton;
    private StatusStrip StatusStrip;
    private ToolStripStatusLabel toolStripStatusLabel1;
}