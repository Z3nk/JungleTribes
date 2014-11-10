using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JungleTribes
{
    public class BonhommeTestAnimation : BonhommeAnimation
    {
        public BonhommeTestAnimation(Texture2D source) : base(source, 28, 34, new Vector2(14, 30), 0.3f)
        {
            addAnimation(0, 0, false);
            addAnimation(1, 0, false);
            addAnimation(2, 0, false);
            addAnimation(3, 0, false);
            addAnimation(4, 0, false);
            addAnimation(3, 0, true);
            addAnimation(2, 0, true);
            addAnimation(1, 0, true);
            addAnimation(0, 1, false, 1, 1, false, 0, 1, false, 2, 1, false, 0, 1, true, 1, 1, true, 0, 1, true, 2, 1, false);
            addAnimation(0, 2, false, 1, 2, false, 0, 2, false, 2, 2, false, 3, 2, false, 4, 2, false, 3, 2, false, 2, 2, false);
            addAnimation(0, 3, false, 1, 3, false, 0, 3, false, 2, 3, false, 3, 3, false, 4, 3, false, 3, 3, false, 2, 3, false);
            addAnimation(0, 4, false, 1, 4, false, 0, 4, false, 2, 4, false, 3, 4, false, 4, 4, false, 3, 4, false, 2, 4, false);
            addAnimation(0, 5, false, 1, 5, false, 0, 5, false, 2, 5, false, 0, 5, true, 1, 5, true, 0, 5, true, 2, 5, false);
            addAnimation(0, 4, true, 1, 4, true, 0, 4, true, 2, 4, true, 3, 4, true, 4, 4, true, 3, 4, true, 2, 4, true);
            addAnimation(0, 3, true, 1, 3, true, 0, 3, true, 2, 3, true, 3, 3, true, 4, 3, true, 3, 3, true, 2, 3, true);
            addAnimation(0, 2, true, 1, 2, true, 0, 2, true, 2, 2, true, 3, 2, true, 4, 2, true, 3, 2, true, 2, 2, true);
        }
    }
}
