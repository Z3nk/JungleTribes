using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;


namespace JungleTribes
{
    class MageDrawer : ElementDrawer
    {
        private InstanceAnimation _animation;
        private Texture2D hpbar;

        public MageDrawer(Hero e)
            : base(e)
        {
            _animation = new InstanceAnimation(Global.mageAnimation);
            hpbar = Global.hp_bar;
        }

        public override void draw()
        {
            Hero h = (Hero)_element;
            BonhommeAnimationId animId = (BonhommeAnimationId)h.direction;
            if (h.speed != Vector2.Zero)
                animId += 8;

            Color c;
            try
            {
                if (Global.heroIdTeam[h.id] == Global.heroIdTeam[Global.heroId])
                    c = Color.Green;
                else
                    c = Color.Red;
            }
            catch (KeyNotFoundException)
            {
                c = Color.Red;
            }

            _animation.draw(animId, h.position, 0.3f+0.3f*h.position.Y/Global.mapHeight);
            Global.spriteBatch.Draw(Global.lifebar, h.position - new Vector2(0, +60), Global.lifebar.Bounds, Color.White, 0f, new Vector2(27, 7), 1f, SpriteEffects.None, 0.89f);
            Global.spriteBatch.Draw(hpbar, h.position - new Vector2(0, +60), new Rectangle(0, 0, (int)(h.hp / h.maxHp * 50f), 10), c, 0f, new Vector2(25, 5), 1f, SpriteEffects.None, 0.9f);
        }
    }
}
