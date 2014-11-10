using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public abstract class ElementDynamic : Element
    {
        private Vector2 _speed;
        public Vector2 speed
        {
            get { return _speed; }
            protected set
            {
                if (_speed != value)
                {
                    _speed = value;
                    _hasChanged = true;
                }
            }
        }
        protected float speedX
        {
            get { return _speed.X; }
            set
            {
                if (_speed.X != value)
                {
                    _speed.X = value;
                    _hasChanged = true;
                }
            }
        }
        protected float speedY
        {
            get { return _speed.Y; }
            set
            {
                if (_speed.Y != value)
                {
                    _speed.Y = value;
                    _hasChanged = true;
                }
            }
        }

        public ElementDynamic() : base()
        {
            speed = Vector2.Zero;
        }
        public ElementDynamic(int id, ByteStream byteStream) : base(id, byteStream){}

        public override sealed void update(float delta, Dictionary<int, Element> listElement)
        {
            update2(delta, listElement);

            Vector2 speed = _speed * delta / 1000f;
            if (_solid)
            {
                float n;
                float k = -1;
                Element collision = null;
                foreach (Element e in listElement.Select(p => p.Value))
                {
                    if (e != this && e.solid)
                    {
                        n = Geometry.collideCircleMovingToCircle(_collisionRadius, position, speed, e.collisionRadius, e.position);
                        if (n != -1 && (n < k || k == -1))
                        {
                            k = n;
                            collision = e;
                        }
                    }
                }
                if (k == -1) position += speed;
                else if (k != 0)
                {
                    _hasChanged = true;
                    position += speed * k;
                    speed =  Geometry.getBounceVector(position, speed * (1 - k), collision.position);
                    Console.WriteLine(speed);
                    k = -1;
                    foreach (Element e in listElement.Select(p => p.Value))
                    {
                        if (e != this && e.solid)
                        {
                            n = Geometry.collideCircleMovingToCircle(_collisionRadius, position, speed, e.collisionRadius, e.position);
                            if (n != -1 && (n < k || k == -1)) k = n;
                        }
                    }
                    if (k == -1) position += speed;
                    else
                    {
                        position += speed * k;
                    }
                }
            }
            else
            {
                position += speed;
            }
        }

        public override byte[] getUpdateBytes()
        {
            ByteStream bs = new ByteStream(position, speed, alive);
            return bs.bytes;
        }

        public override void updateElementFromBytes(ByteStream byteStream)
        {
            position = byteStream.readVector2();
            speed = byteStream.readVector2();
            alive = byteStream.readBool();
        }

        public abstract void update2(float delta, Dictionary<int, Element> listElement);
    }
}
