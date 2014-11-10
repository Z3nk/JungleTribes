using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class SkillMageFire : Skill
    {
        public SkillMageFire(Hero owner)
            : base(owner)
        {
            _castTime = 500f;
            _cooldown = 12000f;
            _blockingTime = 0f;
            _name = "Boule de feu";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new MageFire(_owner.position, _position, _owner));
        }
    }
}
