using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    public class ChamanAnimation : BonhommeAnimation
    {
        public ChamanAnimation(Texture2D source)
            : base(source, 56, 57, new Vector2(28, 50), 0.3f)
        {
            addAnimation(0, 0, false);
            addAnimation(1, 0, false);
            addAnimation(2, 0, false);
            addAnimation(3, 0, false);
            addAnimation(4, 0, false);
            addAnimation(5, 0, false);
            addAnimation(6, 0, false);
            addAnimation(7, 0, false);
            addAnimation(0, 1, false, 1, 1, false, 0, 1, false, 0, 0, false, 2, 1, false, 3, 1, false, 2, 1, false, 0, 0, false);
            addAnimation(4, 1, false, 5, 1, false, 4, 1, false, 1, 0, false, 6, 1, false, 7, 1, false, 6, 1, false, 1, 0, false);
            addAnimation(0, 2, false, 1, 2, false, 0, 2, false, 2, 0, false, 2, 2, false, 3, 2, false, 2, 2, false, 2, 0, false);
            addAnimation(4, 2, false, 5, 2, false, 4, 2, false, 3, 0, false, 6, 2, false, 7, 2, false, 6, 2, false, 3, 0, false);
            addAnimation(0, 3, false, 1, 3, false, 0, 3, false, 4, 0, false, 2, 3, false, 3, 3, false, 2, 3, false, 4, 0, false);
            addAnimation(4, 3, false, 5, 3, false, 4, 3, false, 5, 0, false, 6, 3, false, 7, 3, false, 6, 3, false, 5, 0, false);
            addAnimation(0, 4, false, 1, 4, false, 0, 4, false, 6, 0, false, 2, 4, false, 3, 4, false, 2, 4, false, 6, 0, false);
            addAnimation(4, 4, false, 5, 4, false, 4, 4, false, 7, 0, false, 6, 4, false, 7, 4, false, 6, 4, false, 7, 0, false);
        }
    }
}
