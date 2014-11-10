using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JungleTribesImplementation
{
    public static class Geometry
    {
        public static float collideCircleMovingToCircle(float radius1, Vector2 position1, Vector2 speed1, float radius2, Vector2 position2)
        {
            Vector2 center = position1 + (speed1 / 2f);
            float testRadius = abs(speed1.X + speed1.Y) / 2f + radius1;
            //if ((center - position2).LengthSquared() < (testRadius + radius2) * (testRadius + radius2))
            {
                Vector2 vb = position2 - position1;
                float a = speed1.X * speed1.X + speed1.Y * speed1.Y;
                float b = -2f * vb.X * speed1.X - 2f * vb.Y * speed1.Y;
                float c = vb.X * vb.X + vb.Y * vb.Y - (radius1 + radius2) * (radius1 + radius2);
                float delta = b * b - 4f * a * c;

                if (delta >= 0)
                {
                    float k = (-b - (float)Math.Sqrt(delta)) / (2f * a);
                    if (k >= 0f && k < 1f)
                    {
                        return k;
                    }
                }
            }
            return -1f;
        }

        public static Vector2 getBounceVector(Vector2 position1, Vector2 speed1, Vector2 position2)
        {
            Vector2 vb = position2 - position1;
            Vector2 n = new Vector2(0, 0);
            Vector2 ps = position1 + speed1;
            if (ps.X == position2.X || ps.Y == position2.Y)
            {
                return Vector2.Zero;
            }
            if (abs(vb.X) > abs(vb.Y))
            {
                n.Y = 10f;
                n.X = -(vb.Y * n.Y) / vb.X;
            }
            else
            {
                n.X = 10f;
                n.Y = -(vb.X * n.X) / vb.Y;
            }
            float nLenght = n.Length();
            float resultLenght = (speed1.X * n.X + speed1.Y * n.Y) / nLenght;
            return n * (resultLenght / nLenght);
        }

        public static float abs(float x)
        {
            return x < 0 ? -x : x;
        }

        public static bool collideElements(Element e1, Element e2)
        {
            if ((e1.position - e2.position).LengthSquared() <= e1.collisionRadius * e1.collisionRadius + e2.collisionRadius * e2.collisionRadius)
                return true;
            else return false;
        }

        public static Vector2 getVectorWithLimit(Vector2 position1, Vector2 position2, float maxDist)
        {
            Vector2 result = position2 - position1;
            float lenght = result.Length();
            if (lenght > maxDist)
            {
                result *= maxDist;
                result /= lenght;
            }
            return result;
        }
    }
}