using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JungleTribes
{
    public class Map
    {
        Texture2D ground;
        Animation_Decor plante;
        LinkedList<Animation_Decor> decor_anim;
        
        public Map()
        {
            ground = Global.content.Load<Texture2D>("Image\\jeu\\Map\\ground3");
            Texture2D im_plante=Global.content.Load<Texture2D>("Image\\jeu\\Map\\herbe_S2");
            plante = new Animation_Decor(im_plante, 3, 3, im_plante.Width, im_plante.Height,0.2);
            decor_anim = new LinkedList<Animation_Decor>();
            DynamiserMapAnimation(100, plante);
        }

        public void Draw()
        {
            for (int i = 0; i < Global.graphics.PreferredBackBufferWidth; i += 32)
            {
                for (int y = 0; y < Global.graphics.PreferredBackBufferHeight; y += 32)
                {
                    Global.spriteBatch.Draw(ground, new Vector2(i, y), Color.White);
                    Global.spriteBatch.Draw(ground, new Vector2(i, y), ground.Bounds, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                }
            }
            
            foreach (Animation_Decor Anim in decor_anim)
            {                
                Anim.Draw();                
            }            
        }

        public void DynamiserMapAnimation(int nb, Animation_Decor animation_decor)
        {

            Random R = new Random();
            for (int i = 0; i < nb/10; i++)
            {
                for (int y = 0; y < nb / 10; y++)
                {
                    if (i % 2 == 0)
                    {
                        animation_decor.x = (int)(i * (Global.mapWidth / 10) * R.NextDouble() * 2);
                        animation_decor.y = (int)(y * (Global.mapHeight / 10) * R.NextDouble() * 2);
                    }
                    else
                    {
                        animation_decor.x = Global.mapWidth - ((int)(i * (Global.mapWidth / 10) * R.NextDouble() * 2));
                        animation_decor.y = Global.mapHeight - ((int)(y * (Global.mapHeight / 10) * R.NextDouble() * 2));
                    }
                    decor_anim.AddLast(new Animation_Decor(animation_decor));
                }
            }
        }
    }
}
