using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class MageFire : ElementDynamic
    {
        protected float _timer;
        private Hero _owner;
        public MageFire(Vector2 position, Vector2 dest, Hero owner)
            : base()
        {
            this.position = position;
            float angle = (float)Math.Atan2(dest.Y - position.Y, dest.X - position.X);
            speed = new Vector2((float)Math.Cos(angle)*500f, (float)Math.Sin(angle)*500f);
            _solid = false;
            _collisionRadius = 12;
            _timer = 1500f;
            _owner = owner;
        }
        public MageFire(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _solid = false;
            _collisionRadius = 12;
        }

        public override void update2(float delta, Dictionary<int, Element> listElement)
        {
            if (Element.currentListElements is ServerListElements)
            {
                _timer -= delta;

                if (_timer <= 0)
                    alive = false;

                if (alive)
                {
                    foreach (Element e in listElement.Select(p => p.Value))
                    {
                        if (e.solid)
                        {
                            if (Geometry.collideCircleMovingToCircle(_collisionRadius, position, speed * delta / 1000f, e.collisionRadius, e.position) != -1)
                            {
                                alive = false;
                                Element.currentListElements.addElement(new MageExplosion(position, _owner));
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
