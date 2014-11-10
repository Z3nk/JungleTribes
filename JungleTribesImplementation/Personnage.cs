using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;
namespace JungleTribesImplementation
{
    /*class Personnage : Element
    {
        private string name;
        public Personnage(string name, Vector2 pos)
        {
            this.name = name;
            _position = pos;
        }
        public Personnage(byte[] data)
        {
            ByteStream b=new ByteStream(data);
            _id = b.readInt();
            name = b.readString();
            _position = new Vector2(b.readFloat(), b.readFloat());
            _speed = new Vector2(b.readFloat(), b.readFloat());
            _realPosition = new Vector2(_position.X,_position.Y);
            _realSpeed = new Vector2(_speed.X, _speed.Y);
        }

        public override byte[] getCreateBytes()
        {
            ByteStream bs = new ByteStream(_id, name, _realPosition.X, _realPosition.Y, _realSpeed.X, _realSpeed.Y);
            return bs.bytes;
        }
        public override void update(float delta)
        {
            
        }
    }*/
}
