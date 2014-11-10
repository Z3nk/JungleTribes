using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameNetwork;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public class Teleport : Element
    {
        private float timer;

        public Teleport(Vector2 position)
        {
            this.position = position;
            timer = 1000;
            _solid = false;
        }

        public Teleport(int id, ByteStream byteStream) : base(id, byteStream)
        {
            _solid = false;
        }

        public override sealed void update(float delta, Dictionary<int, Element> listElement)
        {
            if (Element.currentListElements is ServerListElements)
            {
                timer -= delta;
                if (timer <= 0) alive = false;
            }
        }
    }
}
