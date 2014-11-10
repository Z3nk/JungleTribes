using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class MageFoudre : ElementDynamic
    {
        private Hero _owner, _currentTarget, _prevTarget;
        private int _bounce;
        private float _damage;
        public MageFoudre(Hero owner, Hero target)
            : base()
        {
            position = owner.position;
            _solid = false;
            _owner = owner;
            _damage = 80;
            _bounce = 0;
            goTo(target);
            _currentTarget = target;
            _prevTarget = null;
        }
        public MageFoudre(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _solid = false;
        }

        public override void update2(float delta, Dictionary<int, Element> listElement)
        {
            if (Element.currentListElements is ServerListElements)
            {
                if (_currentTarget == null) alive = false;
                else
                {
                    if (_bounce <= 5 && (position - _currentTarget.position).LengthSquared() <= (400f * delta / 1000f) * (400f * delta / 1000f))
                    {
                        _currentTarget.affectHP(-_damage);
                        _prevTarget = _currentTarget;
                        _damage *= 0.9f;
                        _bounce++;
                        _currentTarget = _owner.findNearestHero(position, false, true, _prevTarget);
                    }

                    else if (_bounce > 5 || (position - _currentTarget.position).LengthSquared() > (400f * 400f))
                        alive = false;

                    if(_currentTarget!=null)
                    goTo(_currentTarget);
                }
            }
        }

        public void goTo(Hero h)
        {
            speed = h.position - position;
            speed = speed / speed.Length();
            speed *= 400;
        }
    }
}
