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
    }
}
