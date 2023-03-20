namespace RemoteAudioServer;

using RemoteAudioServer.Audio;
using RemoteAudioServer.Networking;

public partial class MainWindow : Form
{
    public const int PORT = 6974;

    public MainWindow()
    {
        InitializeComponent();
        DeviceInfo.Text = $"PC 정보: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";

        initializeComponent();
    }

    private void initializeComponent()
    {
        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(DeviceInfo, "클라이언트에서 컴퓨터를 구분할때 사용합니다.");

        UdpAudioServer server = new UdpAudioServer(PORT);
        server.Start();
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }
}