using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;
namespace JungleTribes
{
    class EPDrawer : ElementDrawer
    {
        private Texture2D sprite;


        public EPDrawer(EP e)
            : base(e)
        {
            sprite = Global.EP;
        }



        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White,
                (float)Math.Atan2(((EP)_element).speed.Y, ((EP)_element).speed.X), new Vector2(100, 38), 1f, SpriteEffects.None, 0.7f);
        }
    }
}
