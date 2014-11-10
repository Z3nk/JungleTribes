using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class ChamanBullet : Bullet
    {
        public ChamanBullet(Vector2 position, Vector2 dest, Hero owner)
            : base(position, dest, owner)
        {
            _timer = 1000f;
            _damage = 20f;
            speed *= 600f;
        }
        public ChamanBullet(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
        }
    }
}
