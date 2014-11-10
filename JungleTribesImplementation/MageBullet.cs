using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class MageBullet : ElementDynamic
    {
        public MageBullet(Vector2 position, Vector2 dest)
            : base()
        {
            _position = position;
            float angle = (float)Math.Atan2(dest.Y - position.Y, dest.X - position.X);
            /*_speed.X = (float)Math.Cos(angle) * 600f;
            _speed.Y = (float)Math.Sin(angle) * 600f;*/
            _solid = false;
        }
        public MageBullet(int id, ByteStream byteStream) : base(id, byteStream)
        {
            _solid = false;
        }

        public override void update2(float delta, Dictionary<int, Element> listElement)
        {
            Console.WriteLine(_position);
        }
    }
}
