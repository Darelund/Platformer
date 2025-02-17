﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class CharacterSelector
    {
        private const string _fontText = "GameText";
        private const string _selectText = "Select A Character Color";
        private const string _enterNameText = "Enter your name";

        private static readonly Vector2 _fontpos1 = new Vector2(GameManager.Window.ClientBounds.Width / 2, 50);
        private static readonly Vector2 _UIImagepos1 = new Vector2(330, 240);
        private static readonly Vector2 _UIImagepos2 = new Vector2(530, 240);
        private static readonly Vector2 _UIImagepos3 = new Vector2(330, 500);
        private static readonly Vector2 _UIImagepos4 = new Vector2(530, 500);

        private static readonly Vector2 _button1Pos = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 100, GameManager.Window.ClientBounds.Height / 2);
        private static readonly Vector2 _button2Pos = new Vector2(GameManager.Window.ClientBounds.Width / 2 + 100, GameManager.Window.ClientBounds.Height / 2);
        private static readonly Vector2 _button3Pos = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 100, GameManager.Window.ClientBounds.Height / 2 + 200);
        private static readonly Vector2 _button4Pos = new Vector2(GameManager.Window.ClientBounds.Width / 2 + 100, GameManager.Window.ClientBounds.Height / 2 + 200);

        private static readonly Vector2 _enterPromptTextPos = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 200, GameManager.Window.ClientBounds.Height / 2 - 100);
        private static readonly Vector2 _enterInputTextPos = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 150, GameManager.Window.ClientBounds.Height / 2);
        private static readonly Vector2 _defaultOrigin = Vector2.Zero;


        private static readonly (Color color1, Color color2, Color color3) _defaultButtonColors = (Color.White, Color.LightBlue, Color.DarkBlue);
        private const string _buttonDisplayText = "Select";
        private const float _buttonDefaultSize = 1;
        private const float _buttonDefaultLayderDepth = 0.1f;
        private const int _buttonValue1 = 1;
        private const int _buttonValue2 = 2;
        private const int _buttonValue3 = 2;
        private const int _buttonValue4 = 2;
        private static readonly Action<object> _buttonMethod = SelectCharacter;

        private static readonly Color _defaultUIColor = Color.White;
        private const float _UISize0 = 0.8f;
        private const float _UISize1 = 3f;
        private const float _UISize2 = 3f;
        private const float _UISize3 = 3f;
        private const float _UISize4 = 3f;
        private const float UIText1layedepth = 0.9f;
        private static readonly Rectangle _UIImage1Rec = new Rectangle(0, 0, 32, 32);
        private static readonly Rectangle _UIImage2Rec = new Rectangle(32, 0, 32, 32);
        private static readonly Rectangle _UIImage3Rec = new Rectangle(0, 32, 32, 32);
        private static readonly Rectangle _UIImage4Rec = new Rectangle(32, 32, 32, 32);
        private const float UIImagelayedepth = 0.9f;


        private static List<UIElement> _SelectCharacterElements;
        private static TextInputManager _textInputManager;
        static CharacterSelector()
        {
            _SelectCharacterElements = new List<UIElement>
            {
                new UIText(ResourceManager.GetSpriteFont(_fontText), _selectText, _fontpos1, _defaultUIColor, _UISize0, _defaultOrigin, UIText1layedepth),
                new UIImage(ResourceManager.GetTexture("GameColors"), _UIImagepos1, _defaultUIColor, _UISize1, _defaultOrigin, _UIImage1Rec, UIImagelayedepth),
                new UIImage(ResourceManager.GetTexture("GameColors"), _UIImagepos2, _defaultUIColor, _UISize2, _defaultOrigin, _UIImage2Rec, UIImagelayedepth),
                new UIImage(ResourceManager.GetTexture("GameColors"), _UIImagepos3, _defaultUIColor, _UISize3, _defaultOrigin, _UIImage3Rec, UIImagelayedepth),
                new UIImage(ResourceManager.GetTexture("GameColors"), _UIImagepos4, _defaultUIColor, _UISize4, _defaultOrigin, _UIImage4Rec, UIImagelayedepth),


                new Button(ResourceManager.GetSpriteFont(_fontText), _defaultButtonColors, _button1Pos, _defaultOrigin, _buttonValue1, _buttonMethod, _buttonDisplayText, _buttonDefaultSize, _buttonDefaultLayderDepth),
                new Button(ResourceManager.GetSpriteFont(_fontText), _defaultButtonColors, _button2Pos, _defaultOrigin, _buttonValue2, _buttonMethod, _buttonDisplayText, _buttonDefaultSize, _buttonDefaultLayderDepth),
                new Button(ResourceManager.GetSpriteFont(_fontText), _defaultButtonColors, _button3Pos, _defaultOrigin, _buttonValue3, _buttonMethod, _buttonDisplayText, _buttonDefaultSize, _buttonDefaultLayderDepth),
                new Button(ResourceManager.GetSpriteFont(_fontText), _defaultButtonColors, _button4Pos, _defaultOrigin, _buttonValue4, _buttonMethod, _buttonDisplayText, _buttonDefaultSize, _buttonDefaultLayderDepth)
            };
            _textInputManager = new TextInputManager(_enterNameText, _enterPromptTextPos, _enterInputTextPos);
        }
        public static void Update(GameTime gameTime)
        {
            if (!_textInputManager.IsInputComplete)
            {
                _textInputManager.Update();
            }
            else
            {
                foreach (UIElement element in _SelectCharacterElements)
                {
                    element.Update(gameTime);
                }

            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            if (!_textInputManager.IsInputComplete)
            {
                _textInputManager.Draw(spriteBatch);
            }
            else
            {
                foreach (UIElement element in _SelectCharacterElements)
                {
                    element.Draw(spriteBatch);
                }
            }
        }

        public static void SelectCharacter(object selectedOption)
        {
            int startScore = 0;
            int startLevel = 0;
            GameManager.Name = _textInputManager.InputText;
            HighScore.UpdateScore(GameManager.Name, startScore, startLevel);
            if (selectedOption is int selection)
            {
                switch (selection)
                {
                    case 1:
                        GameFiles.Character.CHARACTERCOLOR = "white";
                        break;
                    case 2:
                        GameFiles.Character.CHARACTERCOLOR = "blue";
                        break;
                    case 3:
                        GameFiles.Character.CHARACTERCOLOR = "purple";
                        break;
                    case 4:
                        GameFiles.Character.CHARACTERCOLOR = "red";
                        break;
                    default:
                        GameFiles.Character.CHARACTERCOLOR = "purple";
                        break;
                }
            }
            // LevelManager.NextLevel(true);
            LevelManager.NextLevel(true);
          //  Debug.WriteLine(GameManager.GameObjects.Count);
        }
    }
}
