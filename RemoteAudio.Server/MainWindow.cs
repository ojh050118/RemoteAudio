namespace RemoteAudio.Server;

using NAudio.CoreAudioApi;
using RemoteAudio.Server.Networking;

public partial class MainWindow : Form
{
    public UdpAudioServer Server;
    public const int PORT = 6974;

    private MMDevice defaultDevice;

    public MainWindow()
    {
        InitializeComponent();
        deviceInfo.Text = $"PC 정보: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";

        initializeComponent();
    }

    private void initializeComponent()
    {
        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(deviceInfo, "클라이언트에서 컴퓨터를 구분할때 사용합니다.");

        MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        
        Server = new UdpAudioServer(PORT);
        Server.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        var value = (int)(defaultDevice.AudioMeterInformation.MasterPeakValue * progressBar.Maximum);

        progressBar.Value = value;
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }

    private void MainWindow_Load(object sender, EventArgs e)
    {

    }

    private void ProgressBar_Click(object sender, EventArgs e)
    {

    }
}