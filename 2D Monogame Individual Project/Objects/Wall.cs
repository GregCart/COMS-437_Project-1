using static Objects.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Objects
{
    internal class Wall : DrawableGameComponent
    {
        public static Texture2D tex;
        public static Rectangle wallColor;

        private static SoundEffect wallsSound;
        private static SoundEffect wall10Sound;

        public int id;
        public SpriteData sprite;
        public bool rotatedY = false; 
        public EWallSide[] sides;
        public float angle;
        public int color;

        private Vector2 scale;


        private int cooldown = 0;

        public Wall(Game game) : base(game)
        {

        }

        public Wall(Game game, EWallSide[] sides) : base(game)
        {
            this.sides = sides;
        }

        public Wall(Game game, int x, int y, int w, int h, int r, int s) : base(game)
        {
            sprite = new SpriteData
            {
                rect = new Rectangle(x, y, w, h),
                rotation = r,
                scale = s,
            };
        }

        public Vector2 endPoint()
        {
            var ang = this.sprite.rotation;
            var dist = this.sprite.rect.Width;

            var x = (int)(Math.Cos(ang) * dist) + this.sprite.rect.X;
            var y = (int)(Math.Sin(ang) * dist) + this.sprite.rect.Y;

            return new Vector2(x, y);
        }

        public override void Initialize()
        {
            this.color = 2;
        }

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("Textures/COMS_437-Project_1-ColorStrip");
            }
            if (wallColor.IsEmpty)
            {
                wallColor = new Rectangle(2, 0, 1, 1);
            }
            if (wallsSound == null)
            {
                wallsSound = Game.Content.Load<SoundEffect>("Sounds/Clips/TF2 Heavy Hurt Sound Effect-edits");
            }
            if (wall10Sound == null)
            {
                wall10Sound = Game.Content.Load<SoundEffect>("Sounds/Clips/Metal pipe falling sound effect but it’s more violent");
            }
            if (sprite.rect.Width <= 0)
            {
                sprite.rect = new Rectangle(1, 1, 1, 1);
            }
            this.scale = new Vector2(this.sprite.rect.Width, this.sprite.rect.Height);

            SpriteBatch sb = ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)));

            sprite.tex = new Texture2D(sb.GraphicsDevice, 1, 1);

            // Fill texture with given color.
            var data = new Color[1];
            var texc = new Color[32];

            tex.GetData(texc);

            data[0] = texc[color];

            sprite.tex.SetData(data);

            base.LoadContent();
        }

        public void UpdateContent()
        {
            this.LoadContent();
        }

        /*public bool OutOfBounds()
        {
            foreach (EWallSide w in this.sides)
            {
                switch (w)
                {
                    case EWallSide.LEFT:
                        ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitX);
                        break;
                    case EWallSide.RIGHT:
                        ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitX);
                        break;
                    case EWallSide.TOP:
                        ball.vel = Vector2.Reflect(ball.vel, -Vector2.UnitY);
                        break;
                    case EWallSide.BOTTOM:
                        ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitY);
                        break;
                    default:
                        break;
                }
            }
        }*/

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.ElementAt(4));
            Vector2 ballDir = ball.vel;
            Vector2 ballRad = ball.sprite.size() / 2;
            Vector2 center = ball.sprite.center();
            ballDir.Normalize();
            Vector2 ballDirPt = center + (ballRad * ballDir);
            //switch to using clamp for detection
            Vector2 pos = new Vector2(this.sprite.rect.X, this.sprite.rect.Y);
            Vector2 end = this.endPoint();

            if (this.sides == null)
            {
                base.Update(gameTime);

                return;
            }

            if (doIntersect(ballDirPt, ball.nextPos, pos, end) || ball.sprite.Intersects(this.sprite))
            {
                if (this.cooldown <= 0)
                {
                    ball.vel = Vector2.Reflect(ball.vel, -Vector2.UnitX.Rotate(this.sprite.rotation - (MathF.PI / 2)));
                    ball.vel *= new Vector2(1.1f, 1.1f);
                    this.cooldown = 100;

                    switch (id)
                    {
                        case 10:
                            wall10Sound.Play();
                            break;
                        default:
                            wallsSound.Play();
                            break;
                    }
                    MediaPlayer.IsRepeating = false;
                    ball.vel *= new Vector2(1.0001f, 1.0001f);
                    this.cooldown = 15;
                }
            }
            this.cooldown--;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch))).Draw(
                sprite.tex, 
                sprite.loc, 
                null, 
                Color.White, 
                sprite.rotation, 
                Vector2.Zero, 
                this.scale,
                SpriteEffects.None, 
                0.0f
            );

            base.Draw(gameTime);
        }
    }
}
