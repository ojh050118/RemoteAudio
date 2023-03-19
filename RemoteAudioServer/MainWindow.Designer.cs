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
            this.SuspendLayout();
            // 
            // DeviceInfo
            // 
            this.DeviceInfo.AutoSize = true;
            this.DeviceInfo.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeviceInfo.Location = new System.Drawing.Point(12, 9);
            this.DeviceInfo.Name = "DeviceInfo";
            this.DeviceInfo.Size = new System.Drawing.Size(140, 45);
            this.DeviceInfo.TabIndex = 0;
            this.DeviceInfo.Text = "PC 정보:";
            this.DeviceInfo.Click += new System.EventHandler(this.label1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.DeviceInfo);
            this.Name = "MainWindow";
            this.Text = "Remote Audio";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Label DeviceInfo;
}