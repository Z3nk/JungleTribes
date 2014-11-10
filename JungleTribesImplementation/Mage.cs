using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class Mage : Hero
    {
        public Mage() : base()
        {
            _skills.Add(new MageAutoShot(this));
        }
        public Mage(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _skills.Add(new MageAutoShot(this));
        }
    }
}
