using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
            vel = new Vector2(1, 1);
            sprite.scale = .1f;
            this.nextPos = this.sprite.loc + vel;

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
            SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            spriteBatch.Draw(sprite.tex, sprite.loc, null, Color.White, sprite.rotation, Vector2.Zero, sprite.scale, SpriteEffects.None, 0.0f);

            var thickness = 2;
            Color color = Color.Red;
            //from https://stackoverflow.com/questions/72913759/how-can-i-draw-lines-in-monogame
            // Create a texture as wide as the distance between two points and as high as
            // the desired thickness of the line.
            var distance = (int)Vector2.Distance(sprite.loc - (sprite.size() / 2), nextPos);
            var texture = new Texture2D(spriteBatch.GraphicsDevice, distance, thickness);

            // Fill texture with given color.
            var data = new Color[distance * thickness];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }
            texture.SetData(data);

            // Rotate about the beginning middle of the line.
            var rotation = (float)Math.Atan2(nextPos.Y - sprite.loc.Y, nextPos.X - sprite.loc.X);
            var origin = new Vector2(0, thickness / 2);

            spriteBatch.Draw(
                texture,
                sprite.loc,
                null,
                Color.White,
                rotation,
                origin,
                1.0f,
                SpriteEffects.None,
                1.0f);

            base.Draw(gameTime);
        }
    }
}
