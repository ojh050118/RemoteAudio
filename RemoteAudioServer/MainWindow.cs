namespace RemoteAudioServer;

using RemoteAudioServer.Audio;

public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();
        DeviceInfo.Text = $"PC 정보: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";

        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(DeviceInfo, "클라이언트에서 컴퓨터를 구분할때 사용합니다.");

        using (AudioCapturer capturer = new AudioCapturer())
            capturer.Start();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }
}