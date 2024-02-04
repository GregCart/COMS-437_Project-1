using static System.MathF;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    public static class Helpers
    {
        public static Vector2 Manhattan(Vector2 from, Vector2 to)
        {
            return new Vector2(to.X - from.X, to.Y - from.Y);
        }

        public static bool Intersects(Point from1, Point to1, Point from2, Point to2)
        {
            if (Max(from1.X, to1.X) < Min(from2.X, to2.X)) {
                return false;
            }
            //line 1 is vertical
            if (from1.X == to1.X)
            {
                //if 2 crosses horizontally
                if ((from2.X < to1.X && to2.X > to1.X) || (from2.X > to1.X && to2.X < to1.X))
                {
                    var r1 = Enumerable.Range(from1.Y, to1.Y).ToArray();
                    var r2 = Enumerable.Range(from2.Y, to2.Y).ToArray();

                    //if 2 matches vertically
                    if (r1.Max() <= r2.Max() && r1.Min() >= r2.Min())
                    {
                        return true;
                    } else { 
                        return false; 
                    }
                } else {
                    return false;
                }
            }
            //line 1 is horizontal
            if (from1.Y == to1.Y)
            {
                //if 2 crosses vertically
                if ((from2.Y < to1.Y && to2.Y > to1.Y) || (from2.Y > to1.Y && to2.Y < to1.Y))
                {
                    var r1 = Enumerable.Range(from1.X, to1.X).ToArray();
                    var r2 = Enumerable.Range(from2.X, to2.X).ToArray();

                    //if 2 matches horizontally
                    if (r1.Max() <= r2.Max() && r1.Min() >= r2.Min())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }


            var A1 = (from1.Y - to1.Y) / (from1.X - to1.X);
            var A2 = (from2.Y - to2.Y) / (from2.X - to2.X);

            if (A1 == A2)
            {
                return false;
            }

            var b1 = from1.Y - A1 * from1.X;
            var b2 = from2.Y - A2 * from2.X;
            var Xa = (b2 - b1) / (A1 - A2);
            
            if ((Xa < Max(Min(from1.X, to1.X), Min(from2.X, to2.X))) || (Xa > Min(Max(from1.X, to1.X), Max(from2.X, to2.X))))
            {
                return false;
            } else {
                return true;
            }
        }
    }
}
