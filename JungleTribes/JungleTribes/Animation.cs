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
    public class Animation : Base_Animation
    {
        public Animation(Texture2D image, int nbLigne, int nbCologne, int largeur, int hauteur, double vitesse, int x, int y)
        {
            frame = 0;
            this.image = image;
            this.nbLigne = nbLigne;
            this.nbColone = nbCologne;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.vitesse = vitesse;
            this.x = x;
            this.y = y;
        }

        public override void getImage()
        {
            frame++;
            if (frame > vitesse)
            {
                colonneActuel++;
                if (colonneActuel >= nbColone)
                {
                    colonneActuel = 0;
                    ligneActuel++;
                }
                if (ligneActuel >= nbLigne)
                    ligneActuel = 0;
                frame = 0;
            }
        }

        public override void Draw()
        {
            getImage();
            Global.spriteBatch.Draw(image, new Rectangle(x, y, (int)((largeur / nbColone)), (int)((hauteur / nbLigne))), new Rectangle(colonneActuel * (hauteur / nbLigne), ligneActuel * (largeur / nbColone), (largeur / nbColone), (hauteur / nbLigne)), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }
    }
}
