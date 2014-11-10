using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GameNetwork
{
    public class MessageChat : MessageNet
    {
        private string _text;

        public string text
        {
            get { return _text; }
        }

        public MessageChat(string text)
        {
            ByteStream bs = new ByteStream();
            bs.write(text);
            _bytes = bs.bytes;
            _text = text;
        }

        public MessageChat(byte[] bytes)
        {
            ByteStream bs = new ByteStream(bytes);
            _text = bs.readString();
            _bytes = bytes;
        }
    }
}
