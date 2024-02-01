using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    internal class Ball : DrawableGameComponent
    {
        public static Texture2D tex;
        public SpriteData sprite;
        public Vector2 vel;


        public Ball(Game game) : base(game) { }

        public Ball Setup(Frame2D frame, Random rnd)
        {
            sprite.loc = new Vector2(
                (float)(rnd.NextDouble() * (frame.upperRight.X - frame.upperLeft.X - sprite.tex.Width) + frame.upperLeft.X),
                (float)(rnd.NextDouble() * (frame.lowerRight.Y - frame.upperRight.Y - sprite.tex.Height) + frame.upperRight.Y));
            vel = new Vector2(-3, 1);
            sprite.scale = .1f;

            return this;
        }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("Textures/ball");
            }

            sprite.tex = tex;

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
