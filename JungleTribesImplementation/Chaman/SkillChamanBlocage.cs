using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class SkillChamanBlocage : Skill
    {
        public SkillChamanBlocage(Hero owner)
            : base(owner)
        {
            _castTime = 500f;
            _cooldown = 9500f;
            _blockingTime = 0f;
            _name = "Blocage";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new ChamanBlocage(_owner.position, _position, _owner));
        }
    }
}
