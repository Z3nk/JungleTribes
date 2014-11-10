using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JungleTribes
{
    public class GuerrierAnimation: BonhommeAnimation
    {
       /* public enum BonhommeAnimationId : int
        {
            stop_bas, stop_bas_droite, stop_droite, stop_haut_droite, stop_haut, stop_haut_gauche, stop_gauche, stop_bas_gauche,
            bas, bas_droite, droite, haut_droite, haut, haut_gauche, gauche, bas_gauche
        }*/
        public GuerrierAnimation(Texture2D source)
            : base(source, 99, 99, new Vector2(52, 50), 0.3f)   //base(source, 66, 66, new Vector2(35, 33), 0.3f) pour Guerrier2
        {
            addAnimation(0, 0, false);
            addAnimation(2, 0, false);
            addAnimation(3, 0, false);
            addAnimation(5, 0, false);
            addAnimation(7, 0, false);
            addAnimation(6, 0, false);
            addAnimation(4, 0, false);
            addAnimation(1, 0, false);
            addAnimation(0, 2, false, 2, 2, false, 0, 2, false, 4, 2, false, 1, 2, false, 3, 2, false, 1, 2, false, 4, 2, false);      // bas
            addAnimation(1, 3, false, 2, 3, false, 1, 3, false, 0, 3, false, 3, 3, false, 4, 3, false, 3, 3, false, 0, 3, false);       //bas droite
            addAnimation(1, 5, false, 2, 5, false, 1, 5, false, 0, 5, false, 3, 5, false, 4, 5, false, 3, 5, false, 0, 5, false);       // droite
            addAnimation(1, 7, false, 2, 7, false, 1, 7, false, 0, 7, false, 3, 7, false, 4, 7, false, 3, 7, false, 0, 7, false);       // haut droite
            addAnimation(1, 9, false, 2, 9, false, 1, 9, false, 0, 9, false, 3, 9, false, 4, 9, false, 3, 9, false, 0, 9, false);       // haut
            addAnimation(1, 8, false, 2, 8, false, 1, 8, false, 0, 8, false, 3, 8, false, 4, 8, false, 3, 8, false, 0, 8, false);       // haut gauche
            addAnimation(1, 6, false, 2, 6, false, 1, 6, false, 0, 6, false, 3, 6, false, 4, 6, false, 3, 6, false, 0, 6, false);       // gauche
            addAnimation(1, 4, false, 2, 4, false, 1, 4, false, 0, 4, false, 3, 4, false, 4, 4, false, 3, 4, false, 0, 4, false);       // bas gauche
        }
    }
}
