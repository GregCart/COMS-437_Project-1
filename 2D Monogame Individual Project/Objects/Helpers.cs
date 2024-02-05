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

            //line 2 is vertical
            if (from2.X == to2.X)
            {
                return IntersectsVerticalLine(from2, to2, from1, to1);
            }
            //line 2 is horizontal
            if (from2.Y == to2.Y)
            {
                return IntersectsHorizontalLine(from2, to2, from1, to1);
            }
            //line 1 is vertical
            if (from1.X == to1.X)
            {
                return IntersectsVerticalLine(from1, to1, from2, to2);
            }
            //line 1 is horizontal
            if (from1.Y == to1.Y)
            {
                return IntersectsHorizontalLine(from1, to1, from2, to2);
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

        public  static bool IntersectsHorizontalLine(Point hLineFrom, Point hLineTo, Point fromLine, Point toLine)
        {
            //if line crosses vertically
            if ((fromLine.Y < hLineTo.Y && toLine.Y > hLineTo.Y) || (fromLine.Y > hLineTo.Y && toLine.Y < hLineTo.Y))
            {
                var r1 = Enumerable.Range(hLineFrom.X, hLineTo.X).ToArray();
                var r2 = Enumerable.Range(fromLine.X, toLine.X).ToArray();

                //if line matches horizontally
                if ((r1.Max() <= r2.Max() && r1.Min() >= r2.Min()) || (r2.Max() <= r1.Max() && r2.Min() >= r1.Min()))
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

        public static bool IntersectsVerticalLine(Point vLineFrom, Point vLineTo, Point fromLine, Point toLine)
        {
            //if line crosses horizontally
            if ((fromLine.X < vLineTo.X && toLine.X > vLineTo.X) || (fromLine.X > vLineTo.X && toLine.X < vLineTo.X))
            {
                var r1 = Enumerable.Range(vLineFrom.Y, vLineTo.Y).ToArray();
                var r2 = Enumerable.Range(fromLine.Y, toLine.Y).ToArray();

                //if line matches vertically
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
    }
}
