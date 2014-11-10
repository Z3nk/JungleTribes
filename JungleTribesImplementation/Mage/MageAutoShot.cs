using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class MageAutoShot : Skill
    {
        public MageAutoShot(Hero owner) : base(owner)
        {
            _castTime = 0f;
            _cooldown = 733f;
            _blockingTime = 100f;
            _name = "Attaque auto";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new MageBullet(_owner.position, _position, _owner));
        }
    }
}
