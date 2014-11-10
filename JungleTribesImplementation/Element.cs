using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;
using System.Reflection;

namespace JungleTribesImplementation
{
    public abstract class Element
    {
        public static ListElements currentListElements;

        protected bool _hasChanged;
        public bool hasChanged
        {
            get { return _hasChanged; }
            set{ _hasChanged = value; }
        }
        private static int nextId = 0;
        protected int _id;
        private Vector2 _position;
        public Vector2 position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    _hasChanged = true;
                }
            }
        }
        private bool _alive;
        public bool alive
        {
            get { return _alive; }
            set
            {
                if (value != _alive)
                {
                    _alive = value;
                    _hasChanged = true;

                }
            }
        }
        protected float _collisionRadius;
        public float collisionRadius
        {
            get { return _collisionRadius; }
        }

        public int id
        {
            get { return _id; }
        }

        protected bool _solid;
        public bool solid
        {
            get { return _solid; }
        }

        public Element()
        {
            _id = nextId;
            nextId++;
            _hasChanged = true;
            _alive = true;
            _position = Vector2.Zero;
            _solid = true;
        }

        public Element(int id, ByteStream byteStream)
        {
            _id = id;
            updateElementFromBytes(byteStream);
            _solid = true;
        }

        public static Element createElementFromBytes(int id, int elementTypeId, ByteStream byteStream)
        {
            Type elementType = ElementManager.typeList[elementTypeId];
            return (Element)Activator.CreateInstance(elementType, id, byteStream);
        }

        public virtual byte[] getUpdateBytes()
        {
            ByteStream bs = new ByteStream(_position, _alive);
            return bs.bytes;
        }

        public virtual void updateElementFromBytes(ByteStream byteStream)
        {
            position = byteStream.readVector2();
            _alive = byteStream.readBool();
        }

        public abstract void update(float delta, Dictionary<int,Element> listElement);
    }
}
