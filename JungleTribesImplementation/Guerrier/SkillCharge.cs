using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    class SkillCharge: Skill
    {
        public SkillCharge(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 10000.0f;
            _blockingTime = 0.0f;
            _name = "Charge";
        }

        public override void performCode()
        {
            Vector2 PositionShield = (_owner.position + new Vector2(0,-100));
            Element.currentListElements.addElement(new Charge(PositionShield, _owner));
        }
    }
}
