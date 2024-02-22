using static System.MathF;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Objects
{
    public static class Helpers
    {
        public static Vector2 Manhattan(this Vector2 from, Vector2 to)
        {
            return new Vector2(to.X - from.X, to.Y - from.Y);
        }

        #region My Attempt
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
                if ((r1.Max() <= r2.Max() && r1.Min() >= r2.Min()) || (r1.Max() >= r2.Max() && r1.Min() <= r2.Min()))
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

        public static Rectangle? CollidesWith(this Rectangle r1, Rectangle r2)
        {
            Point p00 = r1.Location;
            Point p01 = r1.Location + new Point(r1.Width, r1.Height);
            Point p10 = r2.Location;
            Point p11 = r2.Location + new Point(r2.Width, r2.Height);

            if (p00.X > p11.X || p00.Y > p11.Y || p10.X > p01.X || p10.Y > p01.Y)
            {
                return null;
            }

            Point min = new(Math.Max(p00.X, p10.X), Math.Max(p00.Y, p10.Y));
            Point max = new(Math.Min(p01.X, p11.X), Math.Min(p01.Y, p11.Y));

            return new Rectangle(min, max - min);
        }

        #endregion

        #region from geeksforgeeks
        // Given three collinear points p, q, r, the function checks if 
        // point q lies on line segment 'pr' 
        static Boolean onSegment(Vector2 p, Vector2 q, Vector2 r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are collinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        static int orientation(Vector2 p, Vector2 q, Vector2 r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
            // for details of below formula. 
            float val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            /*int s1X = (q.X - p.X);
            if (s1X > 0)
            {
                int s1y = (q.Y - p.Y);
            }
            */


            if (val < 1e-6f) return 0; // collinear 

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The main function that returns true if line segment 'p1q1' 
        // and 'p2q2' intersect. 
        public static bool doIntersect(Vector2 p1, Vector2 q1, Vector2 p2, Vector2 q2)
        {
            // Find the four orientations needed for general and 
            // special cases 
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases 
            // p1, q1 and p2 are collinear and p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are collinear and q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are collinear and p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are collinear and q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases 
        }
        #endregion

        # region from Addison
        public static Vector2 Rotate(this Vector2 point, float angle)
        {
            float x = point.X * (float)Math.Cos(angle) - point.Y * (float)Math.Sin(angle);
            float y = point.X * (float)Math.Sin(angle) + point.Y * (float)Math.Cos(angle);

            return new Vector2(x, y);
        }

        #endregion
    }
}
