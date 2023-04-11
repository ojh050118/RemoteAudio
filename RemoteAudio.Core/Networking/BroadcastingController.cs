using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteAudio.Core.Networking
{
    public abstract class BroadcastingController<T> : UdpClient where T : class
    {
        public int Port { get; }

        public T Data;
        public List<T> DataList { get; }
        public event Action<T> DataReceived;

        private CancellationTokenSource cancellationTokenSource;

        public BroadcastingController(int port, T data)
            : base(port)
        {
            EnableBroadcast = true;

            cancellationTokenSource = new CancellationTokenSource();
            DataList = new List<T>();

            Data = data;
            Port = port;
        }

        public virtual void Brodcast()
        {
            string info = JsonConvert.SerializeObject(Data);
            var data = Encoding.UTF8.GetBytes(info);

            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, Port);

            Send(data, data.Length, broadcast);
        }

        public virtual void ReceiveBroadcast()
        {
            BeginReceive(receiveCallback, null);
        }

        public virtual void StopReceivingBroadcast()
        {
            cancellationTokenSource.Cancel();
        }

        protected abstract bool ReceiveCallback(T data);

        private void receiveCallback(IAsyncResult ar)
        {
            try
            {
                IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, Port);
                byte[] receivedBytes = EndReceive(ar, ref remoteIpEndPoint);
                string receivedMessage = Encoding.UTF8.GetString(receivedBytes);

                T data = JsonConvert.DeserializeObject<T>(receivedMessage);

                if (ReceiveCallback(data))
                    DataReceived.Invoke(data);

                BeginReceive(receiveCallback, null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
