using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class ChamanSoinDrawer : ElementDrawer
    {
        private Texture2D sprite;

        public ChamanSoinDrawer(Element e)
            : base(e)
        {
            sprite = Global.chaman_soin;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White, 0f, new Vector2(16, 14), 1f, SpriteEffects.None, 0.7f);
        }
    }
}
