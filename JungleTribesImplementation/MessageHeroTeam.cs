using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class MessageHeroTeam : MessageNet
    {
        private int _nbElem;
        public int nbElem
        {
            get { return _nbElem; }
        }
        private ByteStream _byteStream;
        public ByteStream byteStream
        {
            get { return _byteStream; }
        }

        public override byte[] bytes
        {
            get { return ByteStream.concat(BitConverter.GetBytes(_nbElem), _bytes); }
        }

        public MessageHeroTeam(byte[] data)
        {
            _byteStream = new ByteStream(data);
            _nbElem = _byteStream.readInt();
        }

        public MessageHeroTeam()
        {
            _bytes = new byte[0];
            _nbElem = 0;
        }


        public void addElement(Hero h)
        {
            _bytes = ByteStream.concatBytes(_bytes, BitConverter.GetBytes(h.id), BitConverter.GetBytes(h.teamId));
            _nbElem++;
        }

        public int readNext()
        {
            return _byteStream.readInt();
        }
    }
}
