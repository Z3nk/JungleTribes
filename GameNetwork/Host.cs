using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameNetwork
{
    public class Host
    {
        private static object lockId = new object();
        private static int nextId = 0;

        private byte[] _buffer;
        private bool _valid, _connected;
        private int _id;
        private string _username;
        private NetworkStream _tcpStream;
        private IPEndPoint _udpEndPoint;
        private UdpClient _udpClient;



        public int id
        {
            get { return _id; }
        }
        public string username
        {
            get { return _username; }
        }

        public IPEndPoint udpEndPoint
        {
            get { return _udpEndPoint; }
        }
        public bool connected
        {
            get { return _connected; }
            set { _connected = value; }
        }

        public Host(TcpClient tcpClient, UdpClient udpClient)
        {
            lock (lockId)
            {
                _id = nextId;
                nextId++;
            }

            _buffer = new byte[4096];
            _tcpStream = tcpClient.GetStream();
            _udpClient = udpClient;
            _valid = false;
            _username = "unnamed";
            _udpEndPoint = new IPEndPoint(((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, 0);
            _connected = false;
        }

        public void initialize(MessageInitConnection message)
        {
            _udpEndPoint.Port = message.port;
            _username = message.hostname;
            _connected = true;

            _valid = true;

        }

        public MessageNet receiveTcpMessage()
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
            if (_valid)
            {
                byte[] b = MessageManager.getBytes(message);
                _udpClient.Send(b, b.Length, _udpEndPoint);
            }
            else throw new InvalidOperationException("Impossible d'envoyer un message UDP avec un host non validé.");
        }

        public override bool Equals(object obj)
        {
            Host host = (Host)obj;
            if (obj is Host)
            {
                if (host._id == _id) return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}
