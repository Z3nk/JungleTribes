using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class SkillMageFoudre : Skill
    {
        public SkillMageFoudre(Hero owner)
            : base(owner)
        {
            _castTime = 300;
            _blockingTime = 0;
            _cooldown = 10000;
            _name = "Foudre";
        }

        public override void performCode()
        {
            Hero target;

            target = _owner.findNearestHero(_position, false, true, null);
            if (target == null || (target.position - _owner.position).LengthSquared() > 300 * 300)
                cancel();
            else
                Element.currentListElements.addElement(new MageFoudre(_owner, target));
        }
    }
}
