using RemoteAudio.Core.Networking;

namespace RemoteAudio.Core.Networking.Server
{
    public class ServerBroadcastingController : RemoteAudioBroadcastingController
    {
        public ServerBroadcastingController(int port, HostInfo hostInfo)
            : base(port, hostInfo, ServiceMode.Client)
        {
        }

        protected override bool ReceiveCallback(HostInfo data)
        {
            var received = base.ReceiveCallback(data);

            if (received)
                Broadcast();

            return received;
        }
    }
}
