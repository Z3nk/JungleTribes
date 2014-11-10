using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class ChamanAide : Element
    {
        private float _timer, _life;
        private Hero _owner;
        public ChamanAide(Vector2 position, Hero owner) : base()
        {
            this.position = position;
            _owner = owner;
            _solid = false;
            _collisionRadius = 200;
            _timer = 200;
            _life =7000;
        }
        public ChamanAide(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _solid = false;
        }


        public override void update(float delta, Dictionary<int, Element> listElement)
        {
            if (Element.currentListElements is ServerListElements)
            {
                _life -= delta;
                if (_life <= 0)
                    alive = false;
                _timer -= delta;
                if (_timer <= 0)
                {
                    _timer += 200;
                    foreach (Element e in listElement.Select(p => p.Value))
                    {
                        if (e is Hero)
                        {
                            Hero h = (Hero)e;
                            if (h.teamId != _owner.teamId)
                            {
                                if (Geometry.collideElements(e, this))
                                {
                                    h.affectHP(-2);
                                }
                            }

                            if (h.teamId == _owner.teamId)
                            {
                                if (Geometry.collideElements(e, this))
                                {
                                    h.affectHP(1.2f);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
