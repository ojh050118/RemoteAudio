namespace RemoteAudio.Core.Networking
{
    public class RemoteAudioBroadcastingController : BroadcastingController<HostInfo>
    {
        public ServiceMode TargetServiceMode { get; }
        public HostInfo HostInfo { get; }

        public RemoteAudioBroadcastingController(int port, HostInfo hostInfo, ServiceMode targetServiceMode)
            : base(port, hostInfo)
        {
            HostInfo = hostInfo;
            TargetServiceMode = targetServiceMode;
        }

        protected override bool ReceiveCallback(HostInfo data)
        {
            if (data.ServiceMode != TargetServiceMode)
                return false;

            if (!DataList.Any(h => h.Equals(data)))
                DataList.Add(data);

            return true;
        }
    }
}
