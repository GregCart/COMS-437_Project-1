using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    public record struct Frame2D
    {
        public Vector2 upperLeft;
        public Vector2 upperRight;
        public Vector2 lowerLeft;
        public Vector2 lowerRight;
        public Vector2 center;
    }

    public struct SpriteData
    {
        public Vector2 loc;
        public Rectangle rect;
        public float rotation;
        public float scale;
        public Texture2D tex;

        /// <summary>
        ///     Calculates scaled size of tex
        /// </summary>
        /// <returns>tex Height and Width scaled by scale</returns>
        public Vector2 size()
        {
            return new Vector2(this.tex.Height, this.tex.Width) * scale;
        }

        public Vector2 center()
        {
            return loc + (size() / 2);
        }
    }
}
