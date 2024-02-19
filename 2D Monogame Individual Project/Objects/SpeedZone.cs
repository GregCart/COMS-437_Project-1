using _2D_Monogame_Individual_Project;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Objects
{
    internal class SpeedZone : DrawableGameComponent
    {
        public static Texture2D tex;

        public float speedMod;
        public Vector2 speedDir;
        public SpriteData sprite;

        private bool isEntered;


        public SpeedZone(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.First());
            if (ball.sprite.Intersects(this.sprite))
            {
                this.isEntered = true;
            }

            base.Update(gameTime);
        }

        public void UpdateContent()
        {
            this.LoadContent();
        }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("Textures/hole");
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

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            if (isEntered)
            {
                Ball ball = ((Ball)Game.Components.First());
                ball.vel += speedMod * speedDir;
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
