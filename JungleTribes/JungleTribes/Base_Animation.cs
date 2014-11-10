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
    public abstract class Base_Animation
    {
        protected int frame;
        protected Texture2D image;
        protected int nbLigne;
        protected int nbColone;
        protected int largeur;
        protected int hauteur;
        protected int ligneActuel;
        protected int colonneActuel;
        protected double vitesse;
        protected int _x, _y;
        public int x
        {
            get { return _x; }
            set { _x = value; }
        }
        public int y
        {
            get { return _y; }
            set { _y = value; }
        }
        public abstract void getImage();
        public abstract void Draw();
    }
}
