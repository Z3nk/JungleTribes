using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    class MageAutoShot : Skill
    {
        public MageAutoShot(Hero owner) : base(owner)
        {
            _castTime = 200f;
            _cooldown = 800f;
            _blockingTime = 0f;
            _name = "Mage auto shot";
        }

        protected override void performCode()
        {
            Element.currentListElements.addElement(new MageBullet(_owner.position, _position));
        }
    }
}
