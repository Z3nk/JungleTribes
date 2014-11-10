using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameNetwork
{
    public class MessagePing : MessageNet
    {
        private long _time;
        public long time
        {
            get { return _time; }
        }

        public MessagePing(byte[] data)
        {
            _bytes = data;
            ByteStream bs = new ByteStream(data);
            _time = bs.readLong();
        }

        public MessagePing(long time)
        {
            _time = time;
            ByteStream bs = new ByteStream(time);
            _bytes = bs.bytes;
        }
    }
}
