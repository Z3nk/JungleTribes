using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JungleTribes
{
    class InstanceAnimation
    {
        private float _idCursor;
        private BonhommeAnimation _animation;

        public InstanceAnimation(BonhommeAnimation animation)
        {
            _idCursor = 0;
            _animation = animation;
        }

        public void draw(BonhommeAnimationId animationId, Vector2 position, float depth)
        {
            if (_idCursor >= _animation.getAnimationSize((int)animationId)) _idCursor = 0;
            _animation.draw(animationId, (int)_idCursor, position, depth);
            _idCursor += _animation.speed;
        }
    }
}
