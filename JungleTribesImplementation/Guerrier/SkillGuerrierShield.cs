using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    class SkillGuerrierShield: Skill
    {
        public SkillGuerrierShield(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 8000.0f;
            _blockingTime = 0.0f;
            _name = "Shield";
        }

        public override void performCode()
        {
            Vector2 PositionShield = (_owner.position + new Vector2(0,-100));
            Element.currentListElements.addElement(new Shield(PositionShield, _owner));
        }
    }
}
