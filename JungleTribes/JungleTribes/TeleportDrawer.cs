using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class TeleportDrawer : ElementDrawer
    {
        private Texture2D sprite;
        private float rotation;

        public TeleportDrawer(Teleport e)
            : base(e)
        {
            sprite = Global.teleport;
            rotation = 0;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White, rotation, new Vector2(32, 32), 1f, SpriteEffects.None, 0.7f);
            rotation += 0.5f;
        }
    }
}
