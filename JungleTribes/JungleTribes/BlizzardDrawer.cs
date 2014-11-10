using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class BlizzardDrawer : ElementDrawer
    {
        private Texture2D sprite;
        private float rotation;

        public BlizzardDrawer(MageBlizzard e)
            : base(e)
        {
            sprite = Global.mage_blizzard;
            rotation = 0;
        }

        public override void draw()
        {
            
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White, rotation, new Vector2(150, 150), 1f, SpriteEffects.None, 0.1f);
            rotation += 0.5f;
        }
    }
}
