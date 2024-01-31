using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Monogame_Individual_Project.Content
{
    internal class Ball: DrawableGameComponent 
    {
        public static Texture2D tex;
        public SpriteData sprite;
        public Vector2 vel;


        public Ball(Game game) : base(game) { }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("ball");
            }

            this.sprite.tex = tex;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //handle Ball Position updates

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch))).Draw(tex, sprite.loc, Color.White);

            base.Draw(gameTime);
        }
    }
}
