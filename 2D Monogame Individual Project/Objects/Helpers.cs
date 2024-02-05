﻿using static System.MathF;
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
        #endregion

        #region from geeksforgeeks
        // Given three collinear points p, q, r, the function checks if 
        // point q lies on line segment 'pr' 
        static Boolean onSegment(Point p, Point q, Point r)
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
        static int orientation(Point p, Point q, Point r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
            // for details of below formula. 
            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.Y) * (r.Y - q.Y);

            if (val == 0) return 0; // collinear 

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The main function that returns true if line segment 'p1q1' 
        // and 'p2q2' intersect. 
        public static bool doIntersect(Point p1, Point q1, Point p2, Point q2)
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
    }
}
