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
    public class Animation_Decor : Base_Animation
    {
        private static double coefGrandeur=1;
        private double propreGrandeur;
        
        public Animation_Decor(Texture2D image, int nbLigne, int nbCologne, int largeur, int hauteur, double vitesse)
        {
            frame = 0;
            this.image = image;
            this.nbLigne = nbLigne;
            this.nbColone = nbCologne;
            this.largeur = largeur;
            this.hauteur = hauteur;
            this.vitesse = vitesse;
            coefGrandeur += 0.01;
            propreGrandeur = coefGrandeur;
            if(coefGrandeur >1.5) coefGrandeur = 1.0;
        }

        public Animation_Decor(Animation_Decor D)
        {
            frame = 0;
            this.image = D.image;
            this.nbLigne = D.nbLigne;
            this.nbColone = D.nbColone;
            this.largeur = D.largeur;
            this.hauteur = D.hauteur;
            this.vitesse = D.vitesse;
            this.x = D.x;
            this.y = D.y;
            coefGrandeur += 0.01;
            propreGrandeur = coefGrandeur;
            if (coefGrandeur > 1.5) coefGrandeur = 1.0;           
        }

        public override void getImage()
        {
            frame++;
            if (frame > ((propreGrandeur * 10) -5)*4)
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
            Global.spriteBatch.Draw(image, new Rectangle(x, y, (int)((largeur / nbColone) * propreGrandeur), (int)((hauteur / nbLigne) * propreGrandeur)), new Rectangle(colonneActuel * (largeur / nbColone), ligneActuel * (hauteur / nbLigne), (largeur / nbColone), (hauteur / nbLigne)), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
        }
    }
}
