using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public static class InputManager
    {
        private static KeyboardState keyboardState, previousKeyboardState = Keyboard.GetState();
        private static MouseState mouseState, previousMouseState = Mouse.GetState();

        public static KeyboardState CurrentKeyboard => keyboardState;
        public static KeyboardState PreviousKeyboard => previousKeyboardState;

        public static MouseState CurrentMouse => mouseState;
        public static MouseState PreviousMouse => previousMouseState;
        public static void Update()
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
        public static Vector2 GetMovement()
        {
            //Probably should have done float
            Vector2 inputDirection = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)) inputDirection.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)) inputDirection.X += 1;
            if (inputDirection.Length() != 0) return inputDirection;
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) inputDirection.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)) inputDirection.Y += 1;

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.ThumbSticks.Left.X != 0)
            {
                inputDirection.X += gamePadState.ThumbSticks.Left.X;
            }

            return inputDirection;
        }


        //Button shit
        public static bool LeftClick() => mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
        public static bool RightClick() => mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
        public static bool IsLeftShiftDown() => keyboardState.IsKeyDown(Keys.LeftShift);
        public static Rectangle MouseOver()
        {
            return new Rectangle(mouseState.X, mouseState.Y, 1, 1);
        }

        public static bool DebugButton() => keyboardState.IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U);
    }
}
