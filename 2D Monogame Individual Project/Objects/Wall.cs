using static Objects.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    internal class Wall : DrawableGameComponent
    {
        public static Texture2D tex;
        public static Rectangle wallColor;

        public int id;
        public SpriteData sprite;
        public bool rotatedY = false; 
        public EWallSide[] sides;
        public float angle;

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

        protected override void LoadContent()
        {
            if (tex == null)
            {
                tex = Game.Content.Load<Texture2D>("Textures/COMS_437-Project_1-ColorStrip");
            }
            if (wallColor == null)
            {
                wallColor = new Rectangle(2, 0, 1, 1);
            }

            this.sprite.tex = tex;

            base.LoadContent();
        }

        public void UpdateContent()
        {
            this.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.First());
            Vector2 ballDir = ball.vel;
            Vector2 ballRad = ball.sprite.size() / 2;
            Vector2 center = ball.sprite.center();
            ballDir.Normalize();
            ballDir = (ballDir * 2) - new Vector2(1);
            if (ballDir > 0)
            {
                ballDir.Ceiling();
            }
            else if (ballDir < 0)
            {
                ballDir.Floor();
            }
            Vector2 ballDirPt = center + (ballRad * ballDir);
            //switch to using clamp for detection
            Vector2 pos = new Vector2(this.sprite.rect.X, this.sprite.rect.Y);
            Vector2 end = this.endPoint();

            if (this.sides == null)
            {
                base.Update(gameTime);

                return;
            }

            if (doIntersect(center, ballDirPt, pos, end))
            {
                //Vector2 normal = Vector2.Transform(Vector2.UnitX, Matrix.CreateRotationX(this.sprite.rotation));

                //ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitX * MathF.Sin(this.sprite.rotation));

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
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ((SpriteBatch)Game.Services.GetService(typeof(SpriteBatch))).Draw(tex, sprite.rect, wallColor, Color.White, sprite.rotation, Vector2.Zero, SpriteEffects.None, 0.0f);

            base.Draw(gameTime);
        }
    }
}
