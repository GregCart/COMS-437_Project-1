using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Monogame_Individual_Project.Objects
{
    internal class Hole : DrawableGameComponent
    {
        public static Texture2D tex;
        SpriteData sprite;

        public Hole(Game game) : base(game)
        {
        }
    }
}
