using Newtonsoft.Json;
using RemoteAudio.Core.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteAudio.Core.Networking
{
    public class BrodcastingController
    {
        private int port;

        private HostInfo hostInfo;
        public List<HostInfo> HostList;
        public event Action<HostInfo> HostInfoReceived;

        private UdpClient broadcastClient;
        private CancellationTokenSource cancellationTokenSource;

        private ServiceMode serviceMode;

        public BrodcastingController(int port, ServiceMode serviceMode)
        {
            broadcastClient = new UdpClient(port);
            broadcastClient.EnableBroadcast = true;

            cancellationTokenSource = new CancellationTokenSource();

            HostList = new List<HostInfo>();

            hostInfo = new HostInfo
            {
                ServiceMode = serviceMode,
                DeviceName = PlatformUtils.GetDeviceInfo().DeviceName,
                OS = PlatformUtils.GetDeviceInfo().OS,
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

        public void ReceiveBroadcast(ServiceMode receiveServiceMode)
        {
            serviceMode = receiveServiceMode;
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

                if (serviceMode == hostInfo.ServiceMode)
                    HostInfoReceived?.Invoke(hostInfo);

                broadcastClient.BeginReceive(ReceiveCallback, null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
