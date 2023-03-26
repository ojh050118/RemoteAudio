using RemoteAudio.Client.Networking;

namespace RemoteAudio.Client.Windows
{
    public partial class MainWindow : Form
    {
        public const int PORT = 6974;

        private BrodcastingController brodcasting;

        public MainWindow()
        {
            InitializeComponent();

            brodcasting = new BrodcastingController(PORT - 1);
            brodcasting.HostListChanged += h =>
            {
                hostListView.Items.Clear();

                foreach (var host in h)
                {
                    hostListView.Items.Add(new ListViewItem
                    {
                        Text = $"{host.DeviceName}, {host.OS}, {host.Description}"
                    });
                }
            };
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            notifyIcon.ShowBalloonTip(5);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            brodcasting.Brodcast();
            brodcasting.ReceiveBrodcastingLoop();


        }
    }
}