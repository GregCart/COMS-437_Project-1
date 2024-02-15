using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// from https://stackoverflow.com/questions/75449753/c-sharp-monogame-handling-mouse
namespace _2D_Monogame_Individual_Project.Objects
{
    public class InputManager : GameComponent
    {
        public static bool LeftClicked = false;
        public static bool LeftWasClicked = false;
        public static bool count = true;

        private static MouseState ms = new MouseState(), oms;

        public static Vector2 MDPos = Vector2.Zero;
        public static float Time = 0.0f;
        public static float DownTime = 0.0f;

        public InputManager(Game game) : base(game)
        {
        }

        public static void Update(GameTime gameTime)
        {
            oms = ms;
            ms = Mouse.GetState();
            LeftClicked = ms.LeftButton == ButtonState.Pressed;
            // true On left release like Windows buttons

            if (LeftClicked)
            {
                MDPos = ms.Position.ToVector2();
                LeftWasClicked = true;
                if (count)
                {
                    Time = (float)gameTime.TotalGameTime.TotalSeconds;
                    count = false;
                }
                DownTime = (float)(gameTime.TotalGameTime.TotalSeconds - Time);
                Console.WriteLine(MDPos.ToString());
            } else if (LeftWasClicked)
            {
                Time = (float)(gameTime.TotalGameTime.TotalSeconds - Time);
                LeftWasClicked = false;
            }
        }

        public static bool Hover(Rectangle r)
        {
            return r.Contains(new Vector2(ms.X, ms.Y));
        }
    }
}
