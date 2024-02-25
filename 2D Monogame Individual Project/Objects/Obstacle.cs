using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Objects;
using System.Linq;

namespace _2D_Monogame_Individual_Project.Objects
{
    internal class Obstacle : DrawableGameComponent
    {
        private static SoundEffect obstacleSound;

        public SpriteData sprite;
        public int size;
        public int cooldown;


        public Obstacle(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            this.size = 20;
            this.cooldown = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (obstacleSound == null)
            {
                obstacleSound = Game.Content.Load<SoundEffect>("Sounds/Clips/Microsoft Windows XP Error - Sound Effect (HD)");
            }
            SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            if (spriteBatch != null)
            {
                var texture = new Texture2D(spriteBatch.GraphicsDevice, size, size);

                // Fill texture with given color.
                var data = new Color[size * size];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = Color.Bisque;
                }
                texture.SetData(data);

                sprite.tex = texture;
            }

            base.LoadContent();
        }

        public void UpdateContent()
        {
            this.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.ElementAt(4));
            Vector2? intersect = ball.sprite.Intersects(this.sprite);

            if (intersect.HasValue && this.cooldown <= 0)
            {
                ball.vel = Vector2.Reflect(ball.vel, this.sprite.SpriteReflect(intersect.Value));
                obstacleSound.Play();
                cooldown = 30;
            }
            cooldown--;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            spriteBatch.Draw(
                sprite.tex,
                sprite.loc,
                null,
                Color.White,
                sprite.rotation,
                //new Vector2(this.sprite.size().X / 2, this.sprite.size().Y / 2), 
                Vector2.Zero,
                sprite.scale,
                SpriteEffects.None,
                0.0f
           );

            base.Draw(gameTime);
        }
    }
}
