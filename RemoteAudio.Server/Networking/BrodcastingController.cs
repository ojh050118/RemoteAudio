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

        private HostInfo hostInfo;
        private Thread loop;

        private UdpClient brodcastClient;

        public BrodcastingController(int port)
        {
            brodcastClient = new UdpClient(port);
            brodcastClient.EnableBroadcast = true;

            hostInfo = new HostInfo
            {
                Provider = "Remote Audio",
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

        public bool ReceiveBrodcasting()
        {
            try
            {
                IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, port);

                var data = brodcastClient.Receive(ref broadcast);

                Brodcast();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void StartReceiveBrodcastingLoop()
        {
            Thread thread = loop = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        ReceiveBrodcasting();
                    }
                }
                catch
                {
                }
            });
        }

        public void StopReceiveBrodcastingLoop()
        {
            if (loop != null)
                loop.Interrupt();
        }
    }
}
