using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public abstract class Skill
    {
        protected float _castTime;
        protected float _blockingTime;
        protected float _cooldown;
        protected string _name;
        protected Hero _owner;
        protected bool _performing;
        protected bool _launched;
        protected float _timer;
        protected Vector2 _position;

        public float castTime
        {
            get { return _castTime; }
        }

        public float blockingTime
        {
            get { return _blockingTime; }
        }

        public float cooldown
        {
            get { return _cooldown; }
        }

        public string name
        {
            get { return _name; }
        }

        public Skill(Hero owner)
        {
            _owner = owner;
            _performing = false;

        }

        public void launch(Vector2 pos)
        {
            if (!_performing)
            {
                _position = pos;
                _performing = true;
                _owner.freeze(_castTime + blockingTime);
                _owner.skillLaunched(this);
                _launched = false;
                _timer = _castTime + _cooldown + _blockingTime;
            }
        }

        public void update(float delta)
        {
            if (_performing)
            {
                _timer -= delta;

                if (!_launched && _timer <= _cooldown + _blockingTime)
                {
                    _launched = true;
                    performCode();
                }

                if (_timer <= 0f)
                    _performing = false;

            }
        }

        public void cancel()
        {
            _performing = false;
            _timer = 0;
        }

        public abstract void performCode();

    }
}
