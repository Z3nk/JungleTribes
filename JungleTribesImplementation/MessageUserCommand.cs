using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class MessageUserCommand : MessageNet
    {
        private bool _left, _up, _right, _down;
        private bool[] _skill;
        private Vector2 _target;

        public bool left
        {
            get { return _left; }
        }
        public bool up
        {
            get { return _up; }
        }
        public bool right
        {
            get { return _right; }
        }
        public bool down
        {
            get { return _down; }
        }
        public bool[] skill
        {
            get { return _skill; }
        }
        public Vector2 target
        {
            get { return _target; }
        }

        public MessageUserCommand(bool left, bool up, bool right, bool down, bool skill1, bool skill2, bool skill3, bool skill4, bool skill5, bool skill6, Vector2 target)
        {
            int command = (left ? 1 : 0) | ((up ? 1 : 0) * 2) | ((right ? 1 : 0) * 4) | ((down ? 1 : 0) * 8) | ((skill1 ? 1 : 0) * 16) | ((skill2 ? 1 : 0) * 32) | ((skill3 ? 1 : 0) * 64)
                 | ((skill4 ? 1 : 0) * 128) | ((skill5 ? 1 : 0) * 256) | ((skill6 ? 1 : 0) * 512);

            ByteStream bs = new ByteStream(command, target);

            _bytes = bs.bytes;
        }

        public MessageUserCommand(byte[] bytes)
        {
            ByteStream bs = new ByteStream(bytes);
            int command = bs.readInt();
            _target = bs.readVector2();

            if ((command & 1) == 1) _left = true; else _left = false;
            if ((command & 2) == 2) _up = true; else _up = false;
            if ((command & 4) == 4) _right = true; else _right = false;
            if ((command & 8) == 8) _down = true; else _down = false;

            _skill = new bool[6];
            int mult = 16;
            for(int i = 0; i < 6; i++)
            {
                if ((command & mult) == mult) _skill[i] = true; else _skill[i] = false;
                mult *= 2;
            }
        }
    }
}
