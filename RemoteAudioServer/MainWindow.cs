namespace RemoteAudioServer;

using RemoteAudioServer.Audio;
using RemoteAudioServer.Networking;

public partial class MainWindow : Form
{
    public const int PORT = 6974;

    public MainWindow()
    {
        InitializeComponent();
        DeviceInfo.Text = $"PC ����: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";

        initializeComponent();

        AudioCapturer capturer = new AudioCapturer();
        capturer.Start();
        
    }

    private void initializeComponent()
    {
        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(DeviceInfo, "Ŭ���̾�Ʈ���� ��ǻ�͸� �����Ҷ� ����մϴ�.");

        UdpAudioServer server = new UdpAudioServer(PORT);
        server.Start();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }
}