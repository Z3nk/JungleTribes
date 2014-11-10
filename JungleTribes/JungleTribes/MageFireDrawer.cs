using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class MageFireDrawer : ElementDrawer
    {
        private Texture2D sprite;

        public MageFireDrawer(MageFire e)
            : base(e)
        {
            sprite = Global.mage_fire;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White, 0f, new Vector2(5, 13), 1f, SpriteEffects.None, 0.7f);
        }
    }
}
