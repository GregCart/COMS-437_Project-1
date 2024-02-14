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

        private static MouseState ms = new MouseState(), oms;

        public static Vector2 MDPos = new Vector2();

        public InputManager(Game game) : base(game)
        {
        }

        public static void Update()
        {
            oms = ms;
            ms = Mouse.GetState();
            LeftClicked = ms.LeftButton != ButtonState.Pressed && oms.LeftButton == ButtonState.Pressed;
            // true On left release like Windows buttons

            if (LeftClicked)
            {
                MDPos = ms.Position.ToVector2();
                Console.WriteLine(MDPos.ToString());
            }
        }

        public static bool Hover(Rectangle r)
        {
            return r.Contains(new Vector2(ms.X, ms.Y));
        }
    }
}
