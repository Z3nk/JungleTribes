using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    class SkillTourbiLol : Skill
    {
        public SkillTourbiLol(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 16000.0f;
            _blockingTime = 0.0f;
            _name = "TourbiLol";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new TourbiLol(_owner.position, _owner));
        }
    }
}
