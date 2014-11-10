using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class ChamanSoin : ElementDynamic
    {
        private Hero _target;
        public ChamanSoin(Vector2 position, Hero target)
            : base()
        {
            this.position = position;
            _target = target;
            goTo(_target);
            _solid = false;
        }
        public ChamanSoin(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _solid = false;
        }

        public override void update2(float delta, Dictionary<int, Element> listElement)
        {
            if (Element.currentListElements is ServerListElements)
            {
                if ((position - _target.position).LengthSquared() <= (1000f * delta / 1000f) * (1000f * delta / 1000f) && alive)
                {
                    alive = false;
                    _target.affectHP(125);
                }

                goTo(_target);
            }
        }

        public void goTo(Hero h)
        {
            speed = h.position - position;
            speed = speed / speed.Length();
            speed *= 1000;
        }
    }
}
