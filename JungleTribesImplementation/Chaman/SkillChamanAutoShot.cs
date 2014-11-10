using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JungleTribesImplementation
{
    public class SkillChamanAutoShot : Skill
    {
        public SkillChamanAutoShot(Hero owner)
            : base(owner)
        {
            _castTime = 0f;
            _cooldown = 900f;
            _blockingTime = 100f;
            _name = "Attaque auto";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new ChamanBullet(_owner.position, _position, _owner));
        }
    }
}
