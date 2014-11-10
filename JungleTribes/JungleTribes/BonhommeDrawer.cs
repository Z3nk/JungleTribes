using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JungleTribes
{
    class BonhommeDrawer : ElementDrawer
    {
        private InstanceAnimation _animation;
        private Texture2D hpbar;

        public BonhommeDrawer(Hero e)
            : base(e)
        {
            _animation = new InstanceAnimation(Global.bonhommeTestAnimation);
            hpbar = Global.hp_bar;
        }

        public override void draw()
        {
            Hero h = (Hero)_element;
            BonhommeAnimationId animId = (BonhommeAnimationId)h.direction;
            if (h.speed != Vector2.Zero)
                animId += 8;

            _animation.draw(animId, h.position, 0.3f+0.3f*h.position.Y/Global.mapHeight);
            Global.spriteBatch.Draw(hpbar, h.position - new Vector2(0, +30), new Rectangle(0, 0, (int)(h.hp / h.maxHp * 50f), 10), Color.Red, 0f, new Vector2(25, 5), 1f, SpriteEffects.None, 0.9f);
        }
    }
}
