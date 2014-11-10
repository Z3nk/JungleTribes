using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace GameNetwork
{
    public class Client
    {
        private IPAddress _ipAdress;
        private int _port;
        private TcpClient _tcpClient;
        private NetworkStream _tcpStream;
        private UdpClient _udpClient;
        private Thread _tcpThread;
        private IPEndPoint _udpEndPoint;
        private byte[] _buffer;
        private string _username;
        private Queue<MessageNet> _messageList;
        private float _latency;

        public float latency
        {
            get { return _latency; }
        }

        public Client(IPAddress ipAdress, int port, string username)
        {
            _ipAdress = ipAdress;
            _port = port;

            _tcpClient = new TcpClient();
            _udpClient = new UdpClient();
            _buffer = new byte[4096];
            _udpEndPoint = new IPEndPoint(ipAdress, port + 1);
            _udpClient.Connect(_udpEndPoint);
            _username = username;
            _messageList = new Queue<MessageNet>();
        }

        public void start()
        {
            _tcpThread = new Thread(new ThreadStart(threadTcp));
            _tcpThread.Start();
            _tcpThread = new Thread(new ThreadStart(threadUdp));
            _tcpThread.Start();
        }

        public MessageNet deQueueMessage()
        {
            return _messageList.Dequeue();
        }

        public bool haveMoreMessage()
        {
            return _messageList.Count != 0 ? true : false;
        }

        private MessageNet receiveTcpMessage()
        {
            byte[] bytes;
            lock (_buffer)
            {
                int size = _tcpStream.Read(_buffer, 0, _buffer.Length);
                bytes = ByteStream.subarray(_buffer, 0, size);
            }

            return MessageManager.create(bytes);
        }

        public void sendTcpMessage(MessageNet message)
        {
            byte[] b = MessageManager.getBytes(message);
            _tcpStream.Write(b, 0, b.Length);
        }

        public void sendUdpMessage(MessageNet message)
        {
            byte[] b = MessageManager.getBytes(message);
            _udpClient.Send(b, b.Length);
        }

        private void threadPing()
        {
            while (true)
            {
                sendTcpMessage(new MessagePing(DateTime.Now.Ticks));
                Thread.Sleep(3000);
            }
        }
        
        private void threadTcp()
        {
            _tcpClient.Connect(_ipAdress, _port);
            _tcpStream = _tcpClient.GetStream();
            sendTcpMessage(new MessageInitConnection(((IPEndPoint)_udpClient.Client.LocalEndPoint).Port, _username));

            _tcpThread = new Thread(new ThreadStart(threadPing));
            _tcpThread.Start();
            while (true)
            {

                MessageNet message = receiveTcpMessage();

                if (message is MessagePing)
                {
                    _latency = ((float)(DateTime.Now.Ticks - ((MessagePing)message).time)) / 10000f;
                }

                else
                {
                    lock (_messageList)
                    {
                        _messageList.Enqueue(message);
                    }
                }
            }
        }

        private void threadUdp()
        {
            IPEndPoint ipEndPoint;
            byte[] buffer;
            while (true)
            {
                ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
                buffer = _udpClient.Receive(ref ipEndPoint);
                MessageNet message = MessageManager.create(buffer);

                lock (_messageList)
                {
                    _messageList.Enqueue(message);
                }
            }
        }
    }
}
