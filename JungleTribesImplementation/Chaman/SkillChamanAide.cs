using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    class SkillChamanAide : Skill
    {
        public SkillChamanAide(Hero owner)
            : base(owner)
        {
            _castTime = 1000;
            _blockingTime = 0;
            _cooldown = 18000;
            _name = "Aide des esprits";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new ChamanAide(_owner.position + Geometry.getVectorWithLimit(_owner.position, _position, 300), _owner));
        }
    }
}
