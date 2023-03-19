using Newtonsoft.Json;
using RemoteAudioServer.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteAudioServer.Networking
{
    public class UdpServer
    {
        private UdpClient server;
        private int port;

        public UdpServer(int port)
        {
            server = new UdpClient(port);
            server.EnableBroadcast = true;

            this.port = port;
        }

        public void Brodcast()
        {
            string hostInfo = JsonConvert.SerializeObject(new HostInfo
            {
                Provider = "Remote Audio",
                DeviceName = DeviceInfo.DeviceName,
                OS = DeviceInfo.OS,
                Address = NetworkUtils.GetPrimaryIPv4Address()
            });

            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, port);

            var data = Encoding.UTF8.GetBytes(hostInfo);

            server.Send(data, data.Length, broadcast);
        }
    }
}
