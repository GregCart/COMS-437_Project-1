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

        public SpriteData sprite;
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

            sprite.tex = tex;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Ball ball = ((Ball)Game.Components.First());
            Vector2 pos = new Vector2(this.sprite.rect.X, this.sprite.rect.Y);
            var ang = this.sprite.rotation;
            ang = (float)(2 * Math.PI - ang);
            ang = (float)(Math.PI / 2 - ang);
            var dist = this.sprite.rect.Height;
            var x = (int)(Math.Cos(ang) * dist) + this.sprite.rect.X;
            var y = (int)(Math.Sin(ang) * dist) + this.sprite.rect.Y;
            Vector2 end = new Vector2(x, y);

            foreach (EWallSide w in this.sides)
            {
                if (!Intersects(ball.sprite.loc.ToPoint(), ball.nextPos.ToPoint(), pos.ToPoint(), end.ToPoint()))
                {
                    continue;
                } else {
                    switch (w)
                    {
                        case EWallSide.LEFT:
                            if (ball.nextPos.X <= this.sprite.rect.X)
                            {
                                ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitY);
                                ball.nextPos.X = this.sprite.rect.X;
                            }
                            break;
                        case EWallSide.RIGHT:
                            if (ball.nextPos.X >= this.sprite.rect.X)
                            {
                                ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitY);
                                ball.nextPos.X = this.sprite.rect.X;
                            }
                            break;
                        case EWallSide.TOP:
                            if (ball.nextPos.Y >= this.sprite.rect.Y)
                            {
                                ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitX);
                                ball.nextPos.Y = this.sprite.rect.Y;
                            }
                            break;
                        case EWallSide.BOTTOM:
                            if (ball.nextPos.Y <= this.sprite.rect.Y)
                            {
                                ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitX);
                                ball.nextPos.Y = this.sprite.rect.Y;
                            }
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
