using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JungleTribesImplementation;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class ChamanBlocageDrawer : ElementDrawer
    {
        private Texture2D sprite;
        private float direction;

        public ChamanBlocageDrawer(ChamanBlocage e)
            : base(e)
        {
            sprite = Global.chaman_blocage;
            direction = (float)Math.Atan2(((ChamanBlocage)_element).speed.Y, ((ChamanBlocage)_element).speed.X);
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White,
                direction,
                new Vector2(60, 25), 1f, SpriteEffects.None, 0.7f);
        }
    }
}
