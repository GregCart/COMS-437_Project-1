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
        public int id;
        public Vector2 speedDir;
        public SpriteData sprite;


        public SpeedZone(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.ElementAt(4));
            if (ball.sprite.rect.CollidesWith(sprite.rect).HasValue && ball.sprite.rect.CollidesWith(sprite.rect).Value.Width >= 1)
            {
                if (ball.inMotion || ball.wasInMotion)
                {
                    ball.vel += this.speedDir * this.speedMod;
                }
            }

            base.Update(gameTime);
        }

        public override void Initialize()
        {
            this.sprite.rect = new Rectangle()
            {
                X = (int)this.sprite.loc.X,
                Y = (int)this.sprite.loc.Y,
                Width = 10,
                Height = 10
            };

            base.Initialize();
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
            if (zoneColor.IsEmpty)
            {
                zoneColor = new Rectangle(3, 0, 1, 1);
            }

            sprite.tex = tex;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            spriteBatch.Draw(
                sprite.tex,
                sprite.rect,
                zoneColor,
                Color.White,
                sprite.rotation,
                Vector2.Zero,
                SpriteEffects.None,
                0.0f
           );

            base.Draw(gameTime);
        }
    }
}
