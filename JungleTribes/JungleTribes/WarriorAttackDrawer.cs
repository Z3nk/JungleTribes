using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using JungleTribesImplementation;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class WarriorAttackDrawer : ElementDrawer
    {
        private Texture2D sprite;

        public WarriorAttackDrawer(WarriorAttack e)
            : base(e)
        {
            sprite = Global.warrior_Attack;
        }

        public override void draw()
        {
            Global.spriteBatch.Draw(sprite, _element.position, sprite.Bounds, Color.White,
                (float)Math.Atan2(((WarriorAttack)_element).speed.Y, ((WarriorAttack)_element).speed.X), new Vector2(38, 100), 1f, SpriteEffects.None, 0.7f);
        }
    }
}
