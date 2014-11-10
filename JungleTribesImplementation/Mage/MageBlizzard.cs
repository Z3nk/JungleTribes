using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class MageBlizzard : Element
    {
        private float _timer, _life;
        private Hero _owner;
        public MageBlizzard(Vector2 position, Hero owner) : base()
        {
            this.position = position;
            _owner = owner;
            _solid = false;
            _collisionRadius = 150;
            _timer = 200;
            _life = 5000;
        }
        public MageBlizzard(int id, ByteStream byteStream) : base(id, byteStream)
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
                                    h.affectMovement(-0.3f, 200);
                                    h.affectHP(-5);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
