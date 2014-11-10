using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    class SkillMageBlizzard : Skill
    {
        public SkillMageBlizzard(Hero owner)
            : base(owner)
        {
            _castTime = 1000;
            _blockingTime = 0;
            _cooldown = 14000;
            _name = "Blizzard";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new MageBlizzard(_owner.position + Geometry.getVectorWithLimit(_owner.position, _position, 300), _owner));
        }
    }
}
