using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class SkillChamanBenediction : Skill
    {
        public SkillChamanBenediction(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 15000f;
            _blockingTime = 0f;
            _name = "Benediction";
        }

        public override void performCode()
        {
            Hero target;

            target = _owner.findNearestHero(_position, true, false, null);
            if (target == null || (target.position - _owner.position).LengthSquared() > 400 * 400)
                cancel();
            else
                Element.currentListElements.addElement(new ChamanBenediction(_owner.position, target));
        }
    }
}
