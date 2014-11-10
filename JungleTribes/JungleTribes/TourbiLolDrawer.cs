using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using JungleTribesImplementation;

namespace JungleTribes
{
    class TourbiLolDrawer : ElementDrawer
    {
        private Texture2D sprite;
        private float rotation;

        public TourbiLolDrawer(TourbiLol e)
            : base(e)
        {
            sprite = Global.TourbiLol;
            rotation = 0.0f;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White, rotation, new Vector2(150, 150), 1f, SpriteEffects.None, 0.7f);
            rotation += 0.5f;
        }
    }
}
