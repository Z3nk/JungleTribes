using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;



namespace JungleTribesImplementation
{
    public class Guerrier : Hero
    {
        public Guerrier() : base()
        {
            _speedPotential = 145;
            _maxHp = 600;
            _hp = 600;
            _armor = 0.2f;
            _skills.Add(new SkillGuerrierAutoShot(this));
            _skills.Add(new SkillGuerrierShield(this));
            _skills.Add(new SkillCharge(this));
            _skills.Add(new SkillEP(this));
            _skills.Add(new SkillTourbiLol(this));

        }
        public Guerrier(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _maxHp = 600;
            _hp = 600;
            _armor = 0.2f;
            _skills.Add(new SkillGuerrierAutoShot(this));
            _skills.Add(new SkillGuerrierShield(this));
            _skills.Add(new SkillCharge(this));
            _skills.Add(new SkillEP(this));
            _skills.Add(new SkillTourbiLol(this));
        }
    }
}
