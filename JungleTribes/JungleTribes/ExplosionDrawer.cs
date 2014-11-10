using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class ExplosionDrawer : ElementDrawer
    {
        private Texture2D sprite;

        public ExplosionDrawer(MageExplosion e)
            : base(e)
        {
            sprite = Global.mage_explosion;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White, 0f, new Vector2(100, 100), 1f, SpriteEffects.None, 0.1f);
        }
    }
}
