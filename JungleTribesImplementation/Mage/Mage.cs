using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class Mage : Hero
    {
        public Mage() : base()
        {
            _speedPotential = 150;
            _maxHp = 500;
            _hp = 500;
            _skills.Add(new MageAutoShot(this));
            _skills.Add(new MageTeleport(this));
            _skills.Add(new SkillMageFire(this));
            _skills.Add(new SkillMageBlizzard(this));
            _skills.Add(new SkillMageFoudre(this));

        }
        public Mage(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _maxHp = 500;
            _hp = 500;
            _skills.Add(new MageAutoShot(this));
            _skills.Add(new MageTeleport(this));
            _skills.Add(new SkillMageFire(this));
            _skills.Add(new SkillMageBlizzard(this));
            _skills.Add(new SkillMageFoudre(this));
        }
    }
}
