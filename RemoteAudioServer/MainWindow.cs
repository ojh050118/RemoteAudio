namespace RemoteAudioServer;

using RemoteAudioServer.Audio;

public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();
        DeviceInfo.Text = $"PC ����: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";

        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(DeviceInfo, "Ŭ���̾�Ʈ���� ��ǻ�͸� �����Ҷ� ����մϴ�.");

        using (AudioCapturer capturer = new AudioCapturer())
            capturer.Start();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }
}