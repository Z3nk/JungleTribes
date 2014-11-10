using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class SkillChamanSoin : Skill
    {
        public SkillChamanSoin(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 8000f;
            _blockingTime = 0f;
            _name = "Soin";
        }

        public override void performCode()
        {
            Hero target;

            target = _owner.findNearestHero(_position, true, false, null);
            if (target == null || (target.position - _owner.position).LengthSquared() > 400 * 400)
                cancel();
            else
                Element.currentListElements.addElement(new ChamanSoin(_owner.position, target));
        }
    }
}
