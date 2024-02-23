using _2D_Monogame_Individual_Project.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Objects
{
    internal class Ball : DrawableGameComponent
    {
        public static Texture2D tex;
        public SpriteData sprite;
        public Vector2 acceleration;
        public Vector2 nextPos;
        public Vector2 vel;
        public bool inMotion { get; private set; }
        public bool wasInMotion { get; private set; }
        public bool visible;

        public Ball(Game game) : base(game) { }

        public Ball Setup(Frame2D frame, Random rnd)
        {
            sprite.loc = new Vector2(frame.center.X - 200, frame.center.Y + 100);
            vel = new Vector2(0, 0);
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
            this.visible = true;
            this.sprite.rect = new(this.sprite.loc.ToPoint(), (this.sprite.loc + this.sprite.size()).ToPoint());

            base.LoadContent();
        }

        public void UpdateContent()
        {
            this.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (this.Visible)
            {
                if (inMotion)
                {
                    wasInMotion = true;
                }
                else
                {
                    wasInMotion = false;
                }

                if (this.vel.Length() > 5e-3f)
                {
                    inMotion = true;
                }
                else
                {
                    inMotion = false;
                }

                if (this.inMotion || this.wasInMotion || this.vel.Length() > 5e-6f)
                {
                    this.sprite.loc = this.nextPos;

                    this.nextPos = this.sprite.loc + this.vel;

                    Vector2 accel = this.vel;

                    accel.Normalize();

                    this.vel -= accel * MathHelper.Min(this.vel.Length(), (float)(.2 * gameTime.ElapsedGameTime.TotalSeconds));
                    this.sprite.rect = new(this.sprite.loc.ToPoint(), (this.sprite.loc + this.sprite.size()).ToPoint());
                }
            }

            base.Update(gameTime);
        }

        public void Kick()
        {
            if (!this.inMotion && !this.wasInMotion)
            {
                Vector2 dir = (InputManager.MDPos - this.sprite.center());
                dir.Normalize();
                this.vel = (dir * MathHelper.Max(1, (InputManager.DownTime * 25) % 100)) / 50;
                this.inMotion = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (this.visible)
            {

                SpriteBatch spriteBatch = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

                spriteBatch.Draw(sprite.tex, sprite.loc, null, Color.White, sprite.rotation, Vector2.Zero, sprite.scale, SpriteEffects.None, 0.0f);

                var thickness = 2;
                Color color = Color.Red;
                #region from https://stackoverflow.com/questions/72913759/how-can-i-draw-lines-in-monogame
                // Create a texture as wide as the distance between two points and as high as
                // the desired thickness of the line.
                var distance = (int)MathF.Max(Vector2.Distance(sprite.loc + (sprite.size() / 2), nextPos), 1);
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

                /*spriteBatch.Draw(
                    texture,
                    sprite.loc + (sprite.size() / 2),
                    null,
                    Color.White,
                    rotation,
                    origin,
                    1.0f,
                    SpriteEffects.None,
                    1.0f);*/
                #endregion

                if (!this.inMotion && !this.wasInMotion)
                {
                    InputManager im = ((InputManager)Game.Services.GetService(typeof(InputManager)));

                    if (InputManager.MDPos != Vector2.Zero && (InputManager.LeftClicked))
                    {
                        //distance = (int)Vector2.Distance(sprite.loc + (sprite.size() / 2), InputManager.MDPos);
                        distance = (int)(MathHelper.Max(1, (InputManager.DownTime * 25) % 100));
                        texture = new Texture2D(spriteBatch.GraphicsDevice, distance, thickness);
                        data = new Color[distance * thickness];
                        for (int i = 0; i < data.Length; i++)
                        {
                            data[i] = color;
                        }
                        texture.SetData(data);
                        rotation = (float)Math.Atan2(InputManager.MDPos.Y - sprite.loc.Y, InputManager.MDPos.X - sprite.loc.X);
                        spriteBatch.Draw(
                            texture,
                            sprite.loc + (sprite.size() / 2),
                            null,
                            Color.DimGray,
                            rotation,
                            origin,
                            1.0f,
                            SpriteEffects.None,
                            1.0f);

                    }
                    else if (InputManager.LeftWasClicked)
                    {
                        InputManager.Reset();
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
