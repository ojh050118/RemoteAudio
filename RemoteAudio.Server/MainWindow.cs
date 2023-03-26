namespace RemoteAudio.Server;

using NAudio.CoreAudioApi;
using RemoteAudio.Server.Networking;

public partial class MainWindow : Form
{
    public UdpAudioServer Server;
    public const int PORT = 6974;

    private BrodcastingController brodcasting;
    private bool isRunning;

    private MMDevice defaultDevice;

    public MainWindow()
    {
        InitializeComponent();

        deviceInfo.Text = $"PC 정보: {Utils.DeviceInfo.DeviceName}, {Utils.DeviceInfo.OS}";

        MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        Server = new UdpAudioServer(PORT);
        brodcasting = new BrodcastingController(PORT - 1);
        
        deviceDescription.TextChanged += (s, e) => brodcasting.HostInfo.Description = deviceDescription.Text;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        var value = (int)(defaultDevice.AudioMeterInformation.MasterPeakValue * progressBar.Maximum);

        progressBar.Value = value;
    }

    private void startButton_Click(object sender, EventArgs e)
    {
        Server.Start();

        isRunning = true;
        startButton.Enabled = false;
        stopButton.Enabled = true;
        deviceDescription.Enabled = false;
        statusText.Text = "멀티캐스팅 중";

        brodcasting.ReceiveBroadcast();
    }

    private void stopButton_Click(object sender, EventArgs e)
    {
        Server.Stop();

        isRunning = false;
        startButton.Enabled = true;
        stopButton.Enabled = false;
        deviceDescription.Enabled = true;
        statusText.Text = "준비";

        brodcasting.StopReceivingBroadcast();
    }
}