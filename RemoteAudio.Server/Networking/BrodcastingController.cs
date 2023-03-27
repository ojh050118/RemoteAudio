using Newtonsoft.Json;
using RemoteAudio.Server.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteAudio.Server.Networking
{
    public class BrodcastingController
    {
        private int port;

        public HostInfo HostInfo;

        private UdpClient broadcastClient;
        private readonly CancellationTokenSource cancellationTokenSource;

        public BrodcastingController(int port)
        {
            broadcastClient = new UdpClient(port);
            broadcastClient.EnableBroadcast = true;

            cancellationTokenSource = new CancellationTokenSource();

            HostInfo = new HostInfo
            {
                Provider = "Remote Audio Host",
                DeviceName = DeviceInfo.DeviceName,
                OS = DeviceInfo.OS,
                Address = NetworkUtils.GetPrimaryIPv4Address()
            };

            this.port = port;
        }

        public void Brodcast()
        {
            string info = JsonConvert.SerializeObject(HostInfo);
            var data = Encoding.UTF8.GetBytes(info);

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Broadcast, port);

            broadcastClient.Send(data, data.Length, endpoint);
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

                if (hostInfo.Provider != "Remote Audio Server")
                    Brodcast();

                broadcastClient.BeginReceive(ReceiveCallback, null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
