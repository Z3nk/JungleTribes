using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JungleTribes
{
    public enum BonhommeAnimationId : int
    {
        stop_bas, stop_bas_droite, stop_droite, stop_haut_droite, stop_haut, stop_haut_gauche, stop_gauche, stop_bas_gauche,
        bas, bas_droite, droite, haut_droite, haut, haut_gauche, gauche, bas_gauche
    }

    public abstract class BonhommeAnimation
    {
        private int _width, _height;
        private List<List<TileImageInfo>> _animations;
        private Texture2D _image;
        private Vector2 _origin;
        private float _speed;
        public float speed
        {
            get { return _speed; }
        }

        public BonhommeAnimation(Texture2D image, int width, int height, Vector2 origin, float speed)
        {
            _image = image;
            _width = width;
            _height = height;
            _animations = new List<List<TileImageInfo>>();
            _origin = origin;
            _speed = speed;
        }

        protected void addAnimation(params object[] listparams)
        {
            List<TileImageInfo> list = new List<TileImageInfo>();
            for(int i = 0; i < listparams.Length; i+=3)
            {
                list.Add(new TileImageInfo((int)listparams[i], (int)listparams[i+1], _width, _height, (bool)listparams[i+2]));
            }
            _animations.Add(list);
        }

        public void draw(BonhommeAnimationId animationId, int imageId, Vector2 position, float depth)
        {
            TileImageInfo tii = _animations[(int)animationId][imageId];
            Global.spriteBatch.Draw(_image, position, tii.position, Color.White, 0f, _origin, 1.0f, tii.flip, depth);
        }

        public int getAnimationSize(int id)
        {
            return _animations[id].Count;
        }
    }
}
