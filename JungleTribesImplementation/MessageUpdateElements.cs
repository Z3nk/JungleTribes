using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class MessageUpdateElements : MessageNet
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

        public MessageUpdateElements(byte[] data)
        {
            _byteStream = new ByteStream(data);
            _nbElem = _byteStream.readInt();
        }

        public MessageUpdateElements()
        {
            _bytes=new byte[0];
            _nbElem = 0;
        }


        public void addUpdateElementInfo(Element e)
        {
            Byte[] typeByte = { (byte)(ElementManager.idList[e.GetType()]) };
            _bytes = ByteStream.concatBytes(_bytes, BitConverter.GetBytes(e.id), typeByte, e.getUpdateBytes());
            _nbElem++;
        }
    }
}
