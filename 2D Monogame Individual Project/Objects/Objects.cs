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

        BoundingBox box {
            get
            {
                Vector2 s = size();
                Vector2 c = center();

                List<Vector2> corners = new()
                {
                    loc - c,
                    new Vector2(loc.X + s.X, loc.Y) - c,
                    new Vector2(loc.X, loc.Y + s.Y) - c,
                    new Vector2(loc.X + s.X, loc.Y + s.Y) - c
                };

                List<Vector2> rotatedCorners = new(corners.Count);
                foreach(Vector2 v in corners) 
                {
                    Vector2 rotatedCorner = (c - loc).Rotate(rotation) + loc;
                    rotatedCorners.Add(rotatedCorner);
                }

                return new(rotatedCorners);
            }
        }
        Color[] texData { 
            get
            {
                Color[] texData = new Color[tex.Width * tex.Height];

                tex.GetData(texData);

                return texData;
            }
                
        }

        public SpriteData ()
        {
            loc = new Vector2();
            rect = new Rectangle();
            rotation = 0;
            scale = 1;
            tex = null;
        }

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

        public Color PixelByWorldCoord(Vector2 worldCoord)
        {
            Vector2 relativeToCenter = worldCoord - loc;
            Vector2 texCoord = (relativeToCenter.Rotate(-rotation) / scale) + center();

            return PixelByTexCoord(texCoord.ToPoint());
        }

        public Color PixelByTexCoord(Point texCoord)
        {
            if (texCoord.Y < 0 || texCoord.X < 0 || texCoord.Y >= tex.Height || texCoord.X >= tex.Width) return Color.Transparent;

            return texData[texCoord.Y * tex.Width + texCoord.X];
        } 

        public bool Intersects(SpriteData other)
        {
            BoundingBox? intersection = box.Intersects(other.box);

            if (!intersection.HasValue) return false;

            float stepSize = Math.Min(scale, other.scale);
            for (float x = intersection.Value.tl.X; x <= intersection.Value.br.X; x += stepSize)
            {
                for (float y = intersection.Value.tl.Y; y <= intersection.Value.br.Y; y += stepSize)
                {
                    Vector2 coord = new(x, y);
                    if (PixelByWorldCoord(coord) != Color.Transparent && other.PixelByWorldCoord(coord) != Color.Transparent) return true; 
                }
            }

            return false;
        }
    }

    //from Addison
    public struct BoundingBox
    {
        public Vector2 tl;
        public Vector2 br;

        public BoundingBox(List<Vector2> points)
        {
            tl = points[0];
            br = points[0];

            foreach (Vector2 p in points)
            {
                tl = new(Math.Min(tl.X, p.X), Math.Min(tl.Y, p.Y));
                br = new(Math.Max(br.X, p.X), Math.Max(br.Y, p.Y));
            }
        }

        public BoundingBox? Intersects(BoundingBox other)
        {
            Vector2 p00 = this.tl;
            Vector2 p01 = this.br;
            Vector2 p10 = other.tl;
            Vector2 p11 = other.br;

            if (p00.X > p11.X || p00.Y > p11.Y || p10.X > p01.X || p10.Y > p01.Y)
            {
                return null;
            }

            Vector2 min = new(Math.Max(p00.X, p10.X), Math.Max(p00.Y, p10.Y));
            Vector2 max = new(Math.Min(p01.X, p11.X), Math.Max(p01.Y, p11.Y));

            List<Vector2> points = new()
            {
                min,
                max
            };

            return new BoundingBox(points);
        }
    }
}
