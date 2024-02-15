using static Objects.Helpers;
using static Objects.EWallSide;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Objects;
using _2D_Monogame_Individual_Project.Objects;

namespace _2D_Monogame_Individual_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _wallTexture;
        private Random rnd;

        private InputManager inputManager;
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

            ball = new Ball(this);

            rnd = new Random();

            Components.Add(ball);
            for (int i = 0; i < 10; i++)
            {
                walls[i] = new Wall(this);
                Components.Add(walls[i]);
            }

            inputManager = new InputManager(this);
            Services.AddService(typeof(InputManager), inputManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            _frame.upperLeft = new Vector2(5, 5);
            _frame.upperRight = new Vector2(GraphicsDevice.Viewport.Width - 5, 5);
            _frame.lowerLeft = new Vector2(5, GraphicsDevice.Viewport.Height - 5);
            _frame.lowerRight = new Vector2(GraphicsDevice.Viewport.Width - 5, GraphicsDevice.Viewport.Height - 5);
            _frame.center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            ball.Setup(_frame, rnd);
            
            minLength = (int) ball.sprite.size().Length() + 1;

            int wallNum = 0;

            int x = (int)(_frame.center.X - 300 - wallThickness);
            int y = (int)(_frame.center.Y - 150);
            walls[0].sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            minLength * 14,
                            wallThickness
                       ),
                    rotation = MathHelper.ToRadians(90),
                    scale = 1.0f,
                };
            walls[0].sides = new EWallSide[] { LEFT };
            walls[0].id = wallNum++;

            Point pt = walls[0].endPoint().ToPoint();
            x = (int)(_frame.center.X - 70 - wallThickness);
            y = (int)(_frame.center.Y - 90 + minLength);
            walls[1].sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            pt.Y - y,
                            wallThickness
                       ),
                    rotation = MathHelper.ToRadians(90),
                    scale = 1.0f,
                };
            walls[1].sides = new EWallSide[] { RIGHT };
            walls[1].id = wallNum++;

            (x, y) = pt;
            x -= wallThickness;
            walls[2].sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            (int)(walls[1].endPoint().X - x),
                            wallThickness

                       ),
                    rotation = MathHelper.ToRadians(0),
                    scale = 1.0f,
            };
            walls[2].sides = new EWallSide[] { BOTTOM };
            walls[2].id = wallNum++;

            x = walls[0].sprite.rect.X;
            y = walls[0].sprite.rect.Y;
            walls[3].sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            walls[2].sprite.rect.Width,
                            wallThickness
                       ),
                    rotation = MathHelper.ToRadians(0),
                    scale = 1.0f,
                };
            walls[3].sides = new EWallSide[] { TOP };
            walls[3].id = wallNum++;

            //This is where it returns a negative x
            pt = walls[3].endPoint().ToPoint();
            x = pt.X;
            y = pt.Y;
            walls[4].sprite = new SpriteData
                {
                    rect = new Rectangle(
                            x,
                            y,
                            walls[0].sprite.rect.Width - walls[1].sprite.rect.Width - 2 * minLength,
                            wallThickness
                       ),
                    rotation = MathHelper.ToRadians(90),
                    scale = 1.0f,
                };
            walls[4].sides = new EWallSide[] { RIGHT };
            walls[4].id = wallNum++;

            pt = walls[4].endPoint().ToPoint();
            x = pt.X - wallThickness;
            y = pt.Y;
            walls[5].sprite = new SpriteData
            {
                rect = new Rectangle(
                            x,
                            y,
                            10 * minLength,
                            wallThickness
                       ),
                rotation = MathHelper.ToRadians(0),
                scale = 1.0f,
            };
            walls[5].sides = new EWallSide[] { TOP };
            walls[5].id = wallNum++;

            x = walls[1].sprite.rect.Left;
            y = walls[1].sprite.rect.Top;
            walls[6].sprite = new SpriteData
            {
                rect = new Rectangle(
                            x,
                            y,
                            7 * minLength,
                            wallThickness
                       ),
                rotation = 0.0f,
                scale = 1.0f,
            };
            walls[6].sides = new EWallSide[] { BOTTOM };
            walls[6].id = wallNum++;

            pt = walls[5].endPoint().ToPoint();
            x = pt.X;
            y = pt.Y;
            walls[7].sprite = new SpriteData
            {
                rect = new Rectangle(
                            x,
                            y,
                            (int)(walls[2].endPoint().Y - walls[3].endPoint().Y),
                            wallThickness
                       ),
                rotation = MathHelper.ToRadians(60),
                scale = 1.0f,
            };
            walls[7].sides = new EWallSide[] { RIGHT, TOP };
            walls[7].rotatedY = true;
            walls[7].id = wallNum++;

            pt = walls[6].endPoint().ToPoint();
            x = pt.X;
            y = pt.Y;
            walls[8].sprite = new SpriteData
            {
                rect = new Rectangle(
                            x,
                            y,
                            (int)(walls[7].endPoint().Y - walls[5].endPoint().Y),
                            wallThickness
                       ),
                rotation = MathHelper.ToRadians(120),
                scale = 1.0f,
            };
            walls[8].sides = new EWallSide[] { RIGHT, TOP };
            walls[8].rotatedY = true;
            walls[8].id = wallNum++;

            pt = walls[8].endPoint().ToPoint();
            x = pt.X;
            y = pt.Y - wallThickness;
            walls[9].sprite = new SpriteData
            {
                rect = new Rectangle(
                            x,
                            y,
                            (int)(walls[7].endPoint().X - x),
                            wallThickness
                       ),
                rotation = MathHelper.ToRadians(0),
                scale = 1.0f,
            };
            walls[9].sides = new EWallSide[] { BOTTOM };
            walls[9].id = wallNum++;

            foreach (var wall in walls)
            {
                wall.UpdateContent();
            }

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            this.BoundsCheck();

            base.Update(gameTime);
        }

        public void BoundsCheck()
        {
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
