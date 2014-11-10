using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class Chaman : Hero
    {
        public Chaman() : base()
        {
            _speedPotential = 145;
            _maxHp = 600;
            _hp = 600;
            _skills.Add(new SkillChamanAutoShot(this));
            _skills.Add(new SkillChamanSoin(this));
            _skills.Add(new SkillChamanBenediction(this));
            _skills.Add(new SkillChamanBlocage(this));
            _skills.Add(new SkillChamanAide(this));
        }
        public Chaman(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _maxHp = 600;
            _hp = 600;
            _skills.Add(new SkillChamanAutoShot(this));
            _skills.Add(new SkillChamanSoin(this));
            _skills.Add(new SkillChamanBenediction(this));
            _skills.Add(new SkillChamanBlocage(this));
            _skills.Add(new SkillChamanAide(this));
        }
    }
}
