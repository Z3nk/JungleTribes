using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JungleTribesImplementation;

namespace JungleTribesImplementation
{
    class SkillEP : Skill
    {
        public SkillEP(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 12000.0f;
            _blockingTime = 0.0f;
            _name = "Epee percante";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new EP(_owner.position + Geometry.getVectorWithLimit(_owner.position, _position, 100), _owner));
        }
    }
}
