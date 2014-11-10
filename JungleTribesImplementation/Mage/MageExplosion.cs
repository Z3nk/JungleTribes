using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class MageExplosion : Element
    {
        private float _timer;
        private List<Hero> _done;
        private Hero _owner;
        public MageExplosion(Vector2 position, Hero owner) : base()
        {
            this.position = position;
            _solid = false;
            _collisionRadius = 100;
            _timer = 500f;
            _done = new List<Hero>();
            _owner = owner;
        }

        public MageExplosion(int id, ByteStream byteStream) : base(id, byteStream)
        {
            _collisionRadius = 100f;
            _solid = false;
        }

        public override void update(float delta, Dictionary<int, Element> listElement)
        {
            
            if (Element.currentListElements is ServerListElements)
            {
                _timer -= delta;
                if (_timer <= 0f)
                    alive = false;
                foreach (Element e in listElement.Select(p => p.Value))
                {
                    if (e is Hero)
                    {
                        Hero h = (Hero)e;
                        if (h.teamId != _owner.teamId)
                        {
                            if (Geometry.collideElements(e, this) && !_done.Contains(h))
                            {
                                h.affectHP(-100);
                                _done.Add(h);
                            }
                        }
                    }
                }
            }
        }
    }
}

