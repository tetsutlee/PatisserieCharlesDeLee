using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    static class Input
    {
        //private static Timer timer;

        private static KeyboardState currentKey;
        private static KeyboardState previousKey;

        private static MouseState currentMouse;
        private static MouseState previousMouse;

        public static void Initialize()
        {
            //timer = new Timer();
        }

        public static void Update()
        {
            //timer.Update();
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            //if (GetKeyTrigger(Keys.Space))
            //{
            //    timer.SetTime();
            //}
        }

        public static bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        public static bool GetKeyTrigger(Keys key)
        {
            return currentKey.IsKeyDown(key) && !previousKey.IsKeyDown(key);
        }

        //public static bool GetKeyMash(int i)
        //{
        //    return timer.GetTime() <= i;
        //}
        public static bool IsMouseLeftButtonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
        }
        public static bool IsMouseLeftButtonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed;
        }
        public static bool IsMouseLeftButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }
        public static Vector2 GetMousePosition
        {
            get
            {
                return new Vector2(currentMouse.X, currentMouse.Y);
            }
        }
    }
}
