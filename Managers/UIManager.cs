using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pacman.GameManager;

namespace Pacman
{
    public class UIManager
    {
        private static List<UIElement> _MainMenuElements;
        private static List<UIElement> _PlayingElements;
        private static List<UIElement> _PauseElements;
        private static List<UIElement> _GameOverElements;
        private static List<UIElement> _VictoryElements;
        private static PlayerHUD _PlayerHUD;

        private static Vector2 _middleOfScreen = new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2);
        private const string _fontUsed = "GameText";
        private static readonly Action<object> _objectState = GameManager.ChangeGameState;

        //MainMenu
        //Animated sprites
        private const string _maintex = "pacman";
        private static readonly Vector2 _mainAnimatedSpritePos1 = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 200, 200);
        private static readonly Vector2 _mainAnimatedSpritePos2 = new Vector2(GameManager.Window.ClientBounds.Width / 2 + 200, 200);
        private static readonly (Point currentFrame, Point frameSize, Point sheetSize) _mainSpriteAnimationConfig = (new Point(0, 0), new Point(40, 40), new Point(4, 0));
        private static readonly Color _mainAnimatedSpritesColor = Color.White;
        private const float _mainAnimatedSpritesSize = 2f;
        private static readonly Vector2 _mainAnimatedSpritesOrigin = Vector2.Zero;
        private const int _mainAnimatedSpritesSpeed = 100;


       //Button
        private static readonly (Color color1, Color color2, Color color3) _MainMenuButtonColors = (Color.White, Color.LightBlue, Color.DarkBlue);
        private static readonly Vector2 _BackgroundButton = new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2);
        private static readonly Vector2 _BackgroundButtonOrigin = Vector2.Zero;
        private const GameManager.GameState _selectCharacterGameState = GameState.SelectCharacter;
        private const string _mainText = "Play";
        private const float _mainMenuTextSize = 1f;
        private const float _mainMenuTextLayerDepth = 0f;

        


        //Playing
        // private const string _playingbackgroundtex = "Background1";
        //private static readonly Vector2 _backgroundPlayingPos = new Vector2(-200, 0);
        //private static readonly Color _backgroundPlayingColor = Color.White;
        //private const float _playingbackgroundSize = 1f;
        //private static readonly Vector2 _backgroundPlayingOrigin = Vector2.Zero;
        //private const float _playingbackgroundLayerDepth = 1f;


        //Pause

        //GameOver
        //private const string _gameOvertex = "pacman_deathClip";
        //private static readonly Vector2 _backgroundGameOverPos = Vector2.Zero;
        //private static readonly Color _backgroundDeathColor = Color.White;
        //private const float _deathbackgroundSize = 1f;
        //private static readonly Vector2 _backgroundDeathOrigin = Vector2.Zero;
        //private const float _deathbackgroundLayerDeph = 1f;

        private const string _gameOvertex = "pacman_deathClip";
        private static readonly Vector2 _gameOveranimatedSpritePos = _middleOfScreen;
        private static readonly (Point currentFrame, Point frameSize, Point sheetSize) _gameOverSpriteAnimationConfig = (new Point(0, 0), new Point(240, 200), new Point(4, 5));
        private static readonly Color _gameOverSpriteAnimationDeathColor = Color.White;
        private const float _gameOverSpriteAnimationSize = 2f;
        private static readonly Vector2 _gameOverSpriteAnimationSizeOrigin = new Vector2(120, 100);
        private const int _gameOverSpriteAnimationSpeed = 200;
        private const float _gameOverSpriteAnimationLayerDepth = 1f;

        private static readonly (Color color1, Color color2, Color color3) _GameOverButtonColors = (Color.White, Color.Red, Color.DarkRed);
        private static readonly Vector2 _GameOverButtonPos = new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 300);
        private static readonly Vector2 _GameOverButtonOrigin = Vector2.Zero;
        private const GameManager.GameState _GameOverState = GameState.Restart;
       // private static readonly Action<object> _gameOver = GameManager.ChangeGameState;
        private const string _gameOverText = "Play Again?";


        //Victory
        private static readonly (Color color1, Color color2, Color color3) _victoryButtonColors = (Color.Green, Color.Red, Color.DarkRed);
        private static readonly Vector2 _playingPos = new Vector2(GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2 + 100);
        private static readonly Vector2 _playingOrigin = Vector2.Zero;
        private const GameManager.GameState _victoryState = GameState.Victory;
       // private static readonly Action<object> _victory = GameManager.ChangeGameState;
        private const string _victoryText = "Victory";



        public static void LoadContent()
        {
            _MainMenuElements = new List<UIElement>();
            _PlayingElements = new List<UIElement>();
            _PauseElements = new List<UIElement>();
            _GameOverElements = new List<UIElement>();
            _VictoryElements = new List<UIElement>();
            _PlayerHUD = new PlayerHUD();

            //_MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture(_maintex1), _staticBackgroundPosition, _mainColor1, _mainSize1, _staticBackgroundOrigin, _mainlayerDepth));
            //_MainMenuElements.Add(new StaticBackground(ResourceManager.GetTexture(_maintex1), _staticBackgroundPosition, _mainColor2, _mainSize2, _staticBackgroundOrigin, _mainlayerDepth));
            _MainMenuElements.Add(new Button(ResourceManager.GetSpriteFont(_fontUsed), _MainMenuButtonColors, _BackgroundButton, _BackgroundButtonOrigin, _selectCharacterGameState, _objectState, _mainText, _mainMenuTextSize, _mainMenuTextLayerDepth));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture(_maintex), _mainAnimatedSpritePos1, _mainSpriteAnimationConfig.currentFrame, _mainSpriteAnimationConfig.frameSize, _mainSpriteAnimationConfig.sheetSize, _mainAnimatedSpritesColor, _mainAnimatedSpritesSize, _mainAnimatedSpritesOrigin, _mainAnimatedSpritesSpeed));
            _MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture(_maintex), _mainAnimatedSpritePos2, _mainSpriteAnimationConfig.currentFrame, _mainSpriteAnimationConfig.frameSize, _mainSpriteAnimationConfig.sheetSize, _mainAnimatedSpritesColor, _mainAnimatedSpritesSize, _mainAnimatedSpritesOrigin, _mainAnimatedSpritesSpeed));
            //_MainMenuElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture(_maintex3), _animatedSprite2Pos, _spriteAnimationConfig2.currentFrame, _spriteAnimationConfig2.frameSize, _spriteAnimationConfig2.sheetSize, _animatedSpritesColor, _animatedSpritesSize, _animatedSpritesOrigin, _animatedSpritesSpeed2));

            // _PlayingElements.Add(new StaticBackground(ResourceManager.GetTexture(_playingbackgroundtex), _backgroundPlayingPos, _backgroundPlayingColor, _playingbackgroundSize, _backgroundPlayingOrigin, _playingbackgroundLayerDepth));

            _GameOverElements.Add(new AnimatedSpriteUI(ResourceManager.GetTexture(_gameOvertex), _gameOveranimatedSpritePos, _gameOverSpriteAnimationConfig.currentFrame, _gameOverSpriteAnimationConfig.frameSize, _gameOverSpriteAnimationConfig.sheetSize, _gameOverSpriteAnimationDeathColor, _gameOverSpriteAnimationSize, _gameOverSpriteAnimationSizeOrigin, _gameOverSpriteAnimationSpeed, _gameOverSpriteAnimationLayerDepth));
            //_GameOverElements.Add(new StaticBackground(ResourceManager.GetTexture(_gameOvertex), _backgroundGameOverPos, _backgroundDeathColor, _deathbackgroundSize, _backgroundDeathOrigin, _deathbackgroundLayerDeph));
            _GameOverElements.Add(new Button(ResourceManager.GetSpriteFont(_fontUsed), _GameOverButtonColors, _GameOverButtonPos, _GameOverButtonOrigin, _GameOverState, _objectState, _gameOverText));

            _VictoryElements.Add(new Button(ResourceManager.GetSpriteFont(_fontUsed), _victoryButtonColors, _playingPos, _playingOrigin, _victoryState, _objectState, _victoryText));
        }
        public static void Update(GameTime gameTime)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.MainMenu:
                    foreach (UIElement element in _MainMenuElements)
                    {
                        element.Update(gameTime);
                    }
                    break;
                case GameState.SelectCharacter:
                    break;
                case GameManager.GameState.Playing:
                    foreach (UIElement element in _PlayingElements)
                    {
                        element.Update(gameTime);
                    }
                    _PlayerHUD.Update(gameTime);
                    break;
                case GameManager.GameState.Pause:
                    break;
                case GameManager.GameState.GameOver:
                    foreach (UIElement element in _GameOverElements)
                    {
                        element.Update(gameTime);
                    }
                    break;
                case GameManager.GameState.Victory:
                    break;
            }
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (GameManager.CurrentGameState)
            {
                case GameManager.GameState.MainMenu:
                    foreach (UIElement element in _MainMenuElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.SelectCharacter:
                    break;
                case GameManager.GameState.Playing:
                    foreach (UIElement element in _PlayingElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    _PlayerHUD.Draw(spriteBatch);
                    break;
                case GameManager.GameState.Pause:
                    break;
                case GameManager.GameState.GameOver:
                    foreach (UIElement element in _GameOverElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameManager.GameState.Victory:
                    foreach (UIElement element in _VictoryElements)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
            }
        }
    }
}
