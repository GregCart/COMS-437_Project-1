using _2D_Monogame_Individual_Project.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Monogame_Individual_Project.Content
{
    internal class Wall:DrawableGameComponent
    {
        public static Texture2D tex;

        public SpriteData sprite;
        public float angle;

        public Wall(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("COMS_437-Project_1-ColorStrip");
            }

            this.sprite.tex = tex;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch))).Draw(tex, sprite.loc, Color.White);

            base.Draw(gameTime);
        }
    }
}
