using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class MageBullet : Bullet
    {
        public MageBullet(Vector2 position, Vector2 dest, Hero owner)
            : base(position, dest, owner)
        {
            _timer = 1000f;
            _damage = 20f;
            speed *= 600f;
        }
        public MageBullet(int id, ByteStream byteStream) : base(id, byteStream)
        {
        }
    }
}
