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
        public Vector2 nextPos;


        public Ball(Game game) : base(game) { }

        public Ball Setup(Frame2D frame, Random rnd)
        {
            sprite.loc = new Vector2(frame.center.X, frame.center.Y);
            vel = new Vector2(0, 0);
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
            this.nextPos = this.sprite.loc + vel;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch))).Draw(sprite.tex, sprite.loc, null, Color.White, sprite.rotation, Vector2.Zero, sprite.scale, SpriteEffects.None, 0.0f);

            base.Draw(gameTime);
        }
    }
}
