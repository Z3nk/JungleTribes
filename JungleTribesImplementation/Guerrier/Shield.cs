using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class Shield: Element
    {
        private Hero _owner;
        protected float _timer;

        public Shield(Vector2 position, Hero owner)
            : base()
        {
            this._owner = owner;
            this.position = position;
            this._owner.armor = 0.8f;
            this._owner.speedPotential /= 2;
            _timer = 3000;
        }


        public Shield(int id, ByteStream byteStream)
            : base(id, byteStream)
        {

        }


         public override void update(float delta, Dictionary<int, Element> listElement)
        {
            
            if (Element.currentListElements is ServerListElements)
            {
                _timer -= delta;
                this.position = _owner.position + new Vector2(0, -100);
                if (_timer <= 0)
                {
                    alive = false;
                    this._owner.armor = 0.2f;
                    this._owner.speedPotential *= 2;
                    
                }
                
               
            }
            
        }

    }
}
