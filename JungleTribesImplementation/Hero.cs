using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public enum Direction : int { bas, bas_droite, droite, haut_droite, haut, haut_gauche, gauche, bas_gauche }

    public class Hero : ElementDynamic
    {
        private class ValueTimer
        {
            public float value;
            public float timer;
            public ValueTimer(float value, float duration)
            {
                this.value = value;
                timer = duration;
            }
        }

        private string _name;
        private Direction _direction;
        private bool _wantMove;
        private readonly static float pi2 = (float)(Math.Sqrt(2.0) / 2.0);
        private User _owner;
        private bool _wantUp, _wantRight, _wantDown, _wantLeft;
        private bool[] _wantSkill;
        private bool _freezed;
        private float _freezeTimer;
        private List<ValueTimer> _movementAffections;


        protected float _speedPotential;
        protected float _armor;
        protected float _maxHp;
        protected float _hp;
        protected List<Skill> _skills;
        protected int _teamId;

        public float speedPotential
        {
            get { return _speedPotential; }
            set { _speedPotential = value; }
        }
        public List<Skill> skills
        {
            get { return _skills; }
        }

        public int teamId
        {
            get { return _teamId; }
            set { _teamId = value; }
        }
        
        protected Vector2 _target;


        public float armor
        {
            get { return _armor; }
            set { _armor = value; }
        }
        public User owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public Direction direction
        {
            get { return _direction; }
        }

        public float hp
        {
            get { return _hp; }
        }

        public float maxHp
        {
            get { return _maxHp; }
        }

        public Hero()
            : base()
        {
            _collisionRadius = 13;
            _speedPotential = 120;
            _freezed = false;
            _skills = new List<Skill>();
            _wantSkill = new bool[6];
            for (int i = 0; i < 6; i++)
                _wantSkill[i] = false;
            _wantUp = false;
            _wantRight = false;
            _wantLeft = false;
            _wantDown = false;
            _movementAffections = new List<ValueTimer>();
            _armor = 0f;
        }

        public Hero(int id, ByteStream byteStream) : base(id, byteStream)
        {
            _collisionRadius = 13;
            _skills = new List<Skill>();
            _wantSkill = new bool[6];
            for (int i = 0; i < 6; i++)
                _wantSkill[i] = false;
            _wantUp = false;
            _wantRight = false;
            _wantLeft = false;
            _wantDown = false;
        }

        public void unfreeze()
        {
            _freezed = false;
            _freezeTimer = 0f;
        }

        public void freeze(float duration)
        {
            _freezed = true;
            if(_freezeTimer < duration)
            _freezeTimer = duration;
        }

        public void affectHP(float value)
        {
            if (value < 0)
                _hp += value * (1f - _armor);
            else
                _hp += value;
            if (_hp < 0)
                _hp = 0;
            else if (_hp > _maxHp)
                _hp = _maxHp;
            _hasChanged = true;
        }

        public void affectMovement(float value, float duration)
        {
            _movementAffections.Add(new ValueTimer(value, duration));
        }

        public void skillLaunched(Skill s)
        {
            ((ServerListElements)Element.currentListElements).sendSkillLaunched(this, (byte)_skills.IndexOf(s));
        }

        public void commandByMessage(MessageUserCommand m)
        {
            _wantLeft = m.left;
            _wantRight = m.right;
            _wantUp = m.up;
            _wantDown = m.down;

            for (int i = 0; i < 6; i++)
            {
                _wantSkill[i] = m.skill[i];
            }

            _target = m.target;
        }


        public override byte[] getUpdateBytes()
        {
            ByteStream bs = new ByteStream(position, speed, (int)_direction, alive, _hp);
            return bs.bytes;
        }

        public override void updateElementFromBytes(ByteStream byteStream)
        {
            position = byteStream.readVector2();
            speed = byteStream.readVector2();
            _direction = (Direction)byteStream.readInt();
            alive = byteStream.readBool();
            _hp = byteStream.readFloat();
        }

        public override sealed void update2(float delta, Dictionary<int, Element> listElement)
        {
            if (Element.currentListElements is ServerListElements)
            {
                if (_hp <= 0)
                    position = new Vector2(-100000, -100000);
                    
                if (_freezed)
                {
                    _freezeTimer -= delta;
                    if (_freezeTimer <= 0)
                        _freezed = false;
                }


                _wantMove = true;
                if (_wantDown && _wantRight) _direction = Direction.bas_droite;
                else if (_wantUp && _wantRight) _direction = Direction.haut_droite;
                else if (_wantUp && _wantLeft) _direction = Direction.haut_gauche;
                else if (_wantDown && _wantLeft) _direction = Direction.bas_gauche;
                else if (_wantDown) _direction = Direction.bas;
                else if (_wantRight) _direction = Direction.droite;
                else if (_wantUp) _direction = Direction.haut;
                else if (_wantLeft) _direction = Direction.gauche;
                else _wantMove = false;

                if (_wantMove && !_freezed)
                {
                    switch (_direction)
                    {
                        case Direction.bas:
                            speedX = 0f;
                            speedY = _speedPotential;
                            break;
                        case Direction.bas_droite:
                            speedX = _speedPotential * pi2;
                            speedY = _speedPotential * pi2;
                            break;
                        case Direction.droite:
                            speedX = _speedPotential;
                            speedY = 0f;
                            break;
                        case Direction.haut_droite:
                            speedX = _speedPotential * pi2;
                            speedY = -_speedPotential * pi2;
                            break;
                        case Direction.haut:
                            speedX = 0f;
                            speedY = -_speedPotential;
                            break;
                        case Direction.haut_gauche:
                            speedX = -_speedPotential * pi2;
                            speedY = -_speedPotential * pi2;
                            break;
                        case Direction.gauche:
                            speedX = -_speedPotential;
                            speedY = 0f;
                            break;
                        case Direction.bas_gauche:
                            speedX = -_speedPotential * pi2;
                            speedY = _speedPotential * pi2;
                            break;
                    }
                }
                else
                {
                    speedX = 0;
                    speedY = 0;
                }

                foreach (Skill k in _skills)
                {
                    k.update(delta);
                }

                if (!_freezed)
                {
                    for (int i = 0; i < _skills.Count; i++)
                    {
                        if (_wantSkill[i])
                            _skills[i].launch(_target);
                    }
                }

                float speedModifier = 1;
                for (int i = 0; i < _movementAffections.Count; i++)
                {
                    _movementAffections[i].timer -= delta;
                    if (_movementAffections[i].timer <= 0)
                    {
                        _movementAffections.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        speedModifier += _movementAffections[i].value;
                    }
                }

                if (speedModifier < 0)
                    speedModifier = 0;
                speed *= speedModifier;
            }
        }


        public Hero findNearestHero(Vector2 position, bool allies, bool ennemy, Hero exception)
        {
            float nearestDist = float.PositiveInfinity;
            Hero result = null;

            foreach (Element e in Element.currentListElements.list.Select(p => p.Value))
            {
                if (e is Hero)
                {
                    Hero h = (Hero)e;

                    if (((h.teamId == teamId && allies) || (h.teamId != teamId && ennemy)) && h != exception)
                    {
                        float dist = (position - h.position).LengthSquared();
                        if (dist < nearestDist)
                        {
                            result = h;
                            nearestDist = dist;
                        }
                    }
                }
            }

            return result;
        }
    }
}
