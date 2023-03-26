using Newtonsoft.Json;
using RemoteAudio.Client.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteAudio.Client.Networking
{
    public class BrodcastingController
    {
        private int port;

        private HostInfo hostInfo;
        public List<HostInfo> HostList;
        public event Action<List<HostInfo>> HostListChanged;

        private UdpClient brodcastClient;

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken;

        public BrodcastingController(int port)
        {
            brodcastClient = new UdpClient(port);
            brodcastClient.EnableBroadcast = true;

            HostList = new List<HostInfo>();

            hostInfo = new HostInfo
            {
                Provider = "Remote Audio Client",
                DeviceName = DeviceInfo.DeviceName,
                OS = DeviceInfo.OS,
                Address = NetworkUtils.GetPrimaryIPv4Address()
            };

            this.port = port;
        }

        public void Brodcast()
        {
            string info = JsonConvert.SerializeObject(hostInfo);

            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, port);

            var data = Encoding.UTF8.GetBytes(info);

            brodcastClient.Send(data, data.Length, broadcast);
        }

        public bool ReceiveBrodcasting(bool brodcast = false)
        {
            try
            {
                IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, port);

                var data = brodcastClient.Receive(ref broadcast);

                if (brodcast)
                    Brodcast();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ReceiveBrodcastingLoop()
        {
            cancellationTokenSource.Cancel();
            cancellationToken = cancellationTokenSource.Token;

            HostList.Clear();
            HostListChanged?.Invoke(HostList);

            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Broadcast, port);

                    try
                    {
                        byte[] data = brodcastClient.Receive(ref remoteEndPoint);
                        string message = Encoding.UTF8.GetString(data);
                        var info = JsonConvert.DeserializeObject<HostInfo>(message);

                        if (!HostList.Exists(h => h.Equals(info)))
                        {
                            HostList.Add(info);
                            HostListChanged?.Invoke(HostList);
                        }
                    }
                    catch (SocketException ex)
                    {
                    }
                }
            }, cancellationToken);
        }
    }
}
