using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class MageTeleport : Skill
    {
        public MageTeleport(Hero owner)
            : base(owner)
        {
            _castTime = 0;
            _blockingTime = 0;
            _cooldown = 12000;
            _name = "Teleportation";
        }

        public override void performCode()
        {
            Element.currentListElements.addElement(new Teleport(_owner.position - new Vector2(0, 20)));
            _owner.position = _owner.position + Geometry.getVectorWithLimit(_owner.position, _position, 200);
            Element.currentListElements.addElement(new Teleport(_owner.position - new Vector2(0, 20)));
        }
    }
}
