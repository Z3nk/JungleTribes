using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JungleTribes
{
    class TileImageInfo
    {
        private SpriteEffects _flip;
        private Rectangle _position;

        public SpriteEffects flip
        {
            get { return _flip; }
        }

        public Rectangle position
        {
            get { return _position; }
        }

        public TileImageInfo(int x, int y, int width, int height, bool flip)
        {
            _position = new Rectangle(x * width, y * height, width, height);
            _flip = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
    }
}
