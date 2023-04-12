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
            Ttl = 10;

            cancellationTokenSource = new CancellationTokenSource();
            DataList = new List<T>();

            Data = data;
            Port = port;
        }

        public virtual void Broadcast()
        {
            string json = JsonConvert.SerializeObject(Data);
            var byteData = Encoding.UTF8.GetBytes(json);

            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, Port);

            Send(byteData, byteData.Length, broadcast);
        }

        public virtual void Broadcast(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            var byteData = Encoding.UTF8.GetBytes(json);

            IPEndPoint broadcast = new IPEndPoint(IPAddress.Broadcast, Port);

            Send(byteData, byteData.Length, broadcast);
        }

        public virtual void ReceiveBroadcast()
        {
            BeginReceive(receiveCallback, null);
        }

        public virtual void StopReceivingBroadcast()
        {
            cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// 패킷을 수신 했을 때 호줄됩니다.
        /// </summary>
        /// <param name="data">수신한 데이터.</param>
        /// <returns>데이터를 올바르게 수신했는지 여부.</returns>
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
                    DataReceived?.Invoke(data);

                BeginReceive(receiveCallback, null);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
