﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameNetwork;

namespace JungleTribesImplementation
{
    public class EP: ElementDynamic
    {
        private Hero _owner;
        protected float _timer;
        public float angle;
        private List<Hero> listHeroEverTouch;

        public EP(Vector2 position, Hero owner)
            : base()
        {
            
            this._owner = owner;
            this.position = position;
            _solid = false;
            _collisionRadius = 50;            
            _timer = 200;
            angle = (float)(Math.Atan2(position.Y - owner.position.Y, position.X - owner.position.X));
            Vector2 tmp;
            tmp.X = (float)(Math.Cos(angle) * 100);
            tmp.Y =( float)(Math.Sin(angle) * 100);
            speed = tmp;
            listHeroEverTouch = new List<Hero>();
        }


        public EP(int id, ByteStream byteStream)
            : base(id, byteStream)
        {
            _solid = false;
            _collisionRadius = 50;
        }


         public override void update2(float delta, Dictionary<int, Element> listElement)
        {
            
            if (Element.currentListElements is ServerListElements)
            {
                _timer -= delta;

                if (_timer <= 0)
                    alive = false;
                if (alive)
                {
                    foreach (Element e in listElement.Select(p => p.Value))
                    {
                        if (e is Hero)
                        {
                            Hero h = (Hero)e;
                            if (h.teamId != _owner.teamId)
                            {
                                if (Geometry.collideElements(e, this) && !listHeroEverTouch.Contains(h))
                                {
                                    h.affectHP(-60);
                                    h.affectMovement(-1, 1000.0f);
                                    listHeroEverTouch.Add(h);
                                }
                            }
                        }
                    }
                }
               
            }
            
        }

    }
}
