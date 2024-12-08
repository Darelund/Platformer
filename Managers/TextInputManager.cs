using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class TextInputManager
    {
        private string _inputText = "";
        private bool _isInputComplete = false;
        private SpriteFont _font;
        private int _maxTextLength;
        private string _promptText;
        private Vector2 _promptPosition;
        private Vector2 _inputPosition;

        public bool IsInputComplete => _isInputComplete;
        public string InputText => _inputText;

        public TextInputManager(string promptText, Vector2 promptPosition, Vector2 inputPosition, int maxTextLength = 12)
        {
            _font = ResourceManager.GetSpriteFont("GameText");
            _promptText = promptText;
            _promptPosition = promptPosition;
            _inputPosition = inputPosition;
            _maxTextLength = maxTextLength;
        }

        public void Update()
        {
            if (_isInputComplete) return;

            var keyboardState = InputManager.CurrentKeyboard;
            if (InputManager.CurrentKeyboard != InputManager.PreviousKeyboard)
            {
                foreach (var key in keyboardState.GetPressedKeys())
                {
                    if (_inputText.Length < _maxTextLength)
                    {
                        // Handle alphabetic characters
                        if (key >= Keys.A && key <= Keys.Z)
                        {
                            _inputText += key.ToString();
                        }
                        // Handle numbers
                        else if (key >= Keys.D0 && key <= Keys.D9)
                        {
                            _inputText += key.ToString().Substring(1);
                        }
                    }
                    // Handle backspace
                    if (key == Keys.Back && _inputText.Length > 0)
                    {
                        _inputText = _inputText.Substring(0, _inputText.Length - 1);
                    }
                    // Handle Enter key to confirm input
                    if (key == Keys.Enter && _inputText.Length > 0)
                    {
                        _isInputComplete = true;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _promptText, _promptPosition, Color.White);
            spriteBatch.DrawString(_font, _inputText, _inputPosition, Color.White);
        }

        // Reset for new input if needed
        public void Reset()
        {
            _inputText = "";
            _isInputComplete = false;
        }
    }
}
