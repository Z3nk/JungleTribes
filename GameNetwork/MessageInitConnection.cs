using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public class MessageInitConnection : MessageNet
    {
        private int _port;
        private string _hostname;

        public int port
        {
            get { return _port; }
        }

        public string hostname
        {
            get { return _hostname; }
        }

        public MessageInitConnection(byte[] data)
        {
            _bytes = data;

            ByteStream bs = new ByteStream(data);

            _port = bs.readInt();
            _hostname = bs.readString();
        }

        public MessageInitConnection(int port, string hostname)
        {
            _port = port;
            _hostname = hostname;

            ByteStream bs = new ByteStream();
            bs.write(port);
            bs.write(hostname);

            _bytes = bs.bytes;
        }
    }
}
