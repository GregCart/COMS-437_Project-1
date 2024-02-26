using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Objects;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;

// from https://stackoverflow.com/questions/75449753/c-sharp-monogame-handling-mouse
namespace _2D_Monogame_Individual_Project.Objects
{
    public class InputManager : GameComponent
    {
        public static bool LeftClicked = false;
        public static bool LeftWasClicked = false;
        public static bool ButtonDown = false;

        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static bool count = true;

        private static MouseState ms = new MouseState(), oms;

        public static Vector2 MDPos = Vector2.Zero;
        public static float Time = 0.0f;
        public static float DownTime = 0.0f;

        private static Game1 game;

        public InputManager(Game1 game) : base(game)
        {
            InputManager.game = game;
        }

        public static void Reset()
        {
            MDPos = Vector2.Zero;
            Time = 0.0f;
            DownTime = 0.0f;
            LeftClicked = false;
            LeftWasClicked = false;
            count = true;
        }

        public static void Update(GameTime gameTime)
        {
            oms = ms;
            previousKeyState = currentKeyState;
            ms = Mouse.GetState();
            currentKeyState = Keyboard.GetState();
            LeftClicked = ms.LeftButton == ButtonState.Pressed;
            // true On left release like Windows buttons
            //Debug.WriteLine("test " + KeyPressed(Keys.Space));
            if (currentKeyState.GetPressedKeyCount() > 0)
            {
                ButtonDown = true;
            }
            if (LeftClicked)
            {
                MDPos = ms.Position.ToVector2();
                if (MDPos.IsInsideRect(new Rectangle(game._frame.upperLeft.ToPoint(), (game._frame.lowerRight - game._frame.upperLeft).ToPoint())))
                {
                    LeftWasClicked = true;
                    if (count)
                    {
                        Time = (float)gameTime.TotalGameTime.TotalSeconds;
                        count = false;
                    }
                    DownTime = (float)(gameTime.TotalGameTime.TotalSeconds - Time);
                }
            }
            else if (LeftWasClicked)
            {
                if (MDPos.IsInsideRect(new Rectangle(game._frame.upperLeft.ToPoint(), (game._frame.lowerRight - game._frame.upperLeft).ToPoint())))
                {
                    Time = (float)(gameTime.TotalGameTime.TotalSeconds - Time);
                    LeftWasClicked = false;
                    Ball ball = (Ball)game.Components.ElementAt(4);
                    ball.Kick();
                }
            }
            else if (!ButtonDown)
            {
                Reset();
            } else if (ButtonDown)
            {
                //handle button input code
                Ball ball = (Ball)game.Components.ElementAt(4);
                if (MDPos == Vector2.Zero)
                {
                    MDPos = ball.sprite.center() - Vector2.UnitY * ball.sprite.size();
                }
                foreach (Keys k in currentKeyState.GetPressedKeys())
                {
                    switch (k) 
                    {
                        case Keys.A:
                            MDPos = (MDPos - ball.sprite.center()).Rotate(MathHelper.ToRadians(-1)) + ball.sprite.center();
                            break;
                        case Keys.W:
                            if (Time == 0f)
                            {
                                Time = (float)gameTime.TotalGameTime.TotalSeconds;
                            }
                            DownTime += (float)(gameTime.ElapsedGameTime.TotalSeconds);
                            break;
                        case Keys.S:
                            if (Time == 0f)
                            {
                                Time = (float)gameTime.TotalGameTime.TotalSeconds;
                            }
                            DownTime = MathF.Max(DownTime - (float)(gameTime.ElapsedGameTime.TotalSeconds), 0f);
                            break;
                        case Keys.D:
                            MDPos = (MDPos - ball.sprite.center()).Rotate(MathHelper.ToRadians(1)) + ball.sprite.center();
                            break;
                    }
                }
                if (KeyPressed(Keys.Space))
                {
                    Time = (float)(gameTime.TotalGameTime.TotalSeconds - Time);
                    ball.Kick();
                    ButtonDown = false;
                }
            }
        }

        public static bool Hover(Rectangle r)
        {
            return r.Contains(new Vector2(ms.X, ms.Y));
        }

        public static bool IsDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool WasDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return !currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyDown(key);
        }
    }
}
