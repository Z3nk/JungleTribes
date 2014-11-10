using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    class SkillGuerrierAutoShot : Skill
    {
        public SkillGuerrierAutoShot(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 365.0f;
            _blockingTime = 0.0f;
            _name = "Attaque auto";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new WarriorAttack(_owner.position + Geometry.getVectorWithLimit(_owner.position, _position, 100), _owner));
        }
    }
}
