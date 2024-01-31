using _2D_Monogame_Individual_Project.Content;
using static _2D_Monogame_Individual_Project.Content.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace _2D_Monogame_Individual_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _wallTexture;
        private Random rnd;
        private Rectangle _wallColor;

        private Frame2D _frame;
        private Wall[] walls;
        private Ball ball;

        private int wallThickness;
        private int minLength;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _frame = new Frame2D();
            walls = new Wall[10];
            wallThickness = 4;
            _wallColor = new Rectangle(2, 0, 1, 1);

            ball = new Ball(this);

            rnd = new Random();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _frame.upperLeft = new Vector2(5, 5);
            _frame.upperRight = new Vector2(GraphicsDevice.Viewport.Width - 5, 5);
            _frame.lowerLeft = new Vector2(5, GraphicsDevice.Viewport.Height - 5);
            _frame.lowerRight = new Vector2(GraphicsDevice.Viewport.Width - 5, GraphicsDevice.Viewport.Height - 5);
            _frame.center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            ball.sprite.loc = new Vector2((float)((rnd.NextDouble() * ((_frame.upperRight.X - _frame.upperLeft.X) - ball.sprite.tex.Width)) + _frame.upperLeft.X),
                        (float)((rnd.NextDouble() * ((_frame.lowerRight.Y - _frame.upperRight.Y) - ball.sprite.tex.Height)) + _frame.upperRight.Y));
            ball.vel = new Vector2(-3, 1);
            ball.sprite.scale = .1f;

            minLength = (int) ball.sprite.size().Length() + 1;

            int x = (int)(_frame.center.X - 300 - wallThickness);
            int y = (int)(_frame.center.Y - 150);
            walls[0] = new Wall()
            {
                sprite = new SpriteData {
                    rect = new Rectangle(
                            x,
                            y,
                            wallThickness,
                            minLength * 14
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            x = (int)(_frame.center.X - 70 - wallThickness);
            y = (int)(_frame.center.Y - 90 + minLength);
            walls[1] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            wallThickness,
                            walls[0].sprite.rect.Bottom - y
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            x = walls[0].sprite.rect.Left;
            y = walls[0].sprite.rect.Bottom;
            walls[2] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            walls[1].sprite.rect.X - (x - wallThickness),
                            wallThickness
                            
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[3] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[4] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[5] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[6] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[7] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[8] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };
            walls[9] = new Wall()
            {
                sprite = new SpriteData
                {
                    rect = new Rectangle(
                            (int)(_frame.center.X - 20 - wallThickness),
                            (int)(_frame.center.Y - 5 - wallThickness),
                            10,
                            wallThickness
                       ),
                    rotation = 0.0f,
                    scale = 1.0f,
                }
            };

            Components.Add(ball);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch );

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();


            if (ball.sprite.loc.Y <= _frame.upperLeft.Y)
            {
                ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitY);
            }
            if (ball.sprite.loc.Y + ball.sprite.size().Y >= _frame.lowerLeft.Y)
            {
                ball.vel = Vector2.Reflect(ball.vel, Vector2.UnitY);
            }

            float m = (_frame.lowerLeft.Y - _frame.upperLeft.Y) / (_frame.lowerLeft.X - _frame.upperLeft.X);
            float b = (_frame.upperLeft.Y - (m * _frame.upperLeft.X));
            //float xCheck = (ball.sprite.loc.Y - b) / m;
            float xCheck = _frame.lowerLeft.X;

            if (ball.sprite.loc.X <= xCheck)
            {
                Vector2 slope = _frame.upperLeft - _frame.lowerLeft;
                slope.Normalize();

                Vector3 normal3D = Vector3.Cross(new Vector3(slope, 0), new Vector3(0, 0, -1));
                Vector2 norm = new Vector2(normal3D.X, normal3D.Y);

                ball.vel = Vector2.Reflect(ball.vel, norm);
            }

            m = (_frame.lowerRight.Y - _frame.upperRight.Y) / (_frame.lowerRight.X - _frame.upperRight.X);
            b = (_frame.upperRight.Y - (m * _frame.upperRight.X));
            //xCheck = (ball.sprite.loc.Y - b) / m;
            xCheck = _frame.upperRight.X;

            if (ball.sprite.loc.X + ball.sprite.size().X >= xCheck)
            {
                Vector2 slope = _frame.upperRight - _frame.lowerRight;
                slope.Normalize();

                Vector3 normal3D = Vector3.Cross(new Vector3(slope, 0), new Vector3(0, 0, -1));
                Vector2 norm = new Vector2(normal3D.X, normal3D.Y);

                ball.vel = Vector2.Reflect(ball.vel, norm);
            }

            foreach (Wall wall in walls)
            {
                
            }

            ball.sprite.loc += ball.vel;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            base.Draw(gameTime); 
            
            _spriteBatch.End();
        }
    }
}
