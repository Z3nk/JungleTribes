using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class MessageSkillLaunched : MessageNet
    {
        private byte _id;
        public byte id
        {
            get { return _id; }
        }

        public MessageSkillLaunched(byte[] data)
        {
            _id = data[0];
        }

        public MessageSkillLaunched(byte id)
        {
            _id = id;
            _bytes = new byte[1];
            _bytes[0] = (byte)id;
        }
    }
}
