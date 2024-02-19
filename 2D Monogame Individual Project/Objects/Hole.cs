using _2D_Monogame_Individual_Project.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Objects
{
    internal class Hole : DrawableGameComponent
    {
        public static Texture2D tex;

        public SpriteData sprite;

        private SpriteFont font;
        private bool isEntered;


        public Hole(Game game) : base(game)
        {
            this.isEntered = false;
        }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("Textures/hole");
            }
            if (font == null)
            {
                font = Game.Content.Load<SpriteFont>("Textures/GO-Font");
            }

            sprite.tex = tex;

            this.sprite.rect = new Rectangle()
            {
                X = (int)this.sprite.loc.X,
                Y = (int)this.sprite.loc.Y,
                Width = (int)this.sprite.size().X,
                Height = (int)this.sprite.size().Y
            };

            base.LoadContent();
        }

        public void UpdateContent()
        {
            this.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.First());
            if(ball.sprite.Intersects(this.sprite))
            {
                this.isEntered = true;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            if (isEntered)
            {
                spriteBatch.DrawString(font, "You Win!", new Vector2(100, 100), Color.Black);
            }

            spriteBatch.Draw(
                sprite.tex, 
                sprite.loc, 
                null, 
                Color.White, 
                sprite.rotation,
                new Vector2(this.sprite.size().X / 2, this.sprite.size().Y / 2), 
                sprite.scale, 
                SpriteEffects.None, 
                0.0f
           );
            
            base.Draw(gameTime);
        }
    }
}
