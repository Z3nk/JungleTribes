using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public abstract class Bullet : ElementDynamic
    {
        protected float _timer;
        protected float _damage;
        private Hero _owner;
        public Bullet(Vector2 position, Vector2 dest, Hero owner)
            : base()
        {
            this.position = position;
            float angle = (float)Math.Atan2(dest.Y - position.Y, dest.X - position.X);
            speed = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            _solid = false;
            _collisionRadius = 5;
            _owner = owner;
        }
        public Bullet(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _solid = false;
            _collisionRadius = 5;
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
                                this.alive = false;
                                if (e is Hero)
                                {
                                    Hero h = (Hero)e;
                                    if (h.teamId != _owner.teamId)
                                        ((Hero)e).affectHP(-_damage);
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
