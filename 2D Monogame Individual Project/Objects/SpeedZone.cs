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
        public static Rectangle zoneColor;

        public float speedMod;
        public Vector2 speedDir;
        public SpriteData sprite;


        public SpeedZone(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.ElementAt(4));
            if (ball.sprite.Intersects(this.sprite))
            {
                if (ball.vel.Length() > 1e-3f)
                {
                    ball.vel += this.speedDir * speedMod;
                }
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
                tex = Game.Content.Load<Texture2D>("Textures/COMS_437-Project_1-ColorStrip");
            }
            if (zoneColor == null)
            {
                zoneColor = new Rectangle(3, 0, 1, 1);
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

            spriteBatch.Draw(
                sprite.tex,
                sprite.loc,
                zoneColor,
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
