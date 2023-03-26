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

        private UdpClient brodcastClient;

        public BrodcastingController(int port)
        {
            brodcastClient = new UdpClient(port);
            brodcastClient.EnableBroadcast = true;

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
    }
}
