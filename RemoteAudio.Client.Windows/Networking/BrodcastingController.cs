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
        public event Action<HostInfo> HostInfoReceived;

        private UdpClient broadcastClient;

        CancellationTokenSource cancellationTokenSource;

        public BrodcastingController(int port)
        {
            broadcastClient = new UdpClient(port);
            broadcastClient.EnableBroadcast = true;

            cancellationTokenSource = new CancellationTokenSource();

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
            var data = Encoding.UTF8.GetBytes(info);

            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, port);

            broadcastClient.Send(data, data.Length, broadcast);
        }

        public void ReceiveBroadcast()
        {
            broadcastClient.BeginReceive(ReceiveCallback, null);
        }

        public void StopReceivingBroadcast()
        {
            cancellationTokenSource.Cancel();
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
                byte[] receivedBytes = broadcastClient.EndReceive(ar, ref remoteIpEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                HostInfo hostInfo = JsonConvert.DeserializeObject<HostInfo>(receivedMessage);

                if (hostInfo.Provider != "Remote Audio Client")
                    HostInfoReceived?.Invoke(hostInfo);

                broadcastClient.BeginReceive(ReceiveCallback, null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
