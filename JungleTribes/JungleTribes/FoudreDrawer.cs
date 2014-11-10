using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class FoudreDrawer : ElementDrawer
    {
        private Texture2D sprite;

        public FoudreDrawer(MageFoudre e)
            : base(e)
        {
            sprite = Global.mage_foudre;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White,
                (float)Math.Atan2(((MageFoudre)_element).speed.Y, ((MageFoudre)_element).speed.X),
                new Vector2(68, 32), 1f, SpriteEffects.None, 0.7f);
        }
    }
}
