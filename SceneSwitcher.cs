using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pacman.GameManager;

namespace Pacman
{
    public class SceneSwitcher
    {
        private bool isFading = false;
        private float fadeAlpha = 0.0f;
        private Color _fadeColor;
        private const float fadeSpeed = 0.5f; // Controls how fast the fade happens
        private GameState _currentGameState;
        private GameWindow _window;
        private GraphicsDevice _device;

        private bool _currentlySwitching = false;
        public SceneSwitcher(GameWindow window, GraphicsDevice device)
        {
            _window = window;
            _device = device;
            OnMainMenu += OnSwitchScene;
            OnPlaying += OnSwitchScene;
            OnGameOver += OnSwitchScene;
            OnWin += OnSwitchScene;
            OnPause += OnSwitchScene;
        }
        public void Update(GameTime gameTime)
        {
            if (_currentlySwitching && !isFading)
            {
                isFading = true;
                fadeAlpha = 0.0f;       // Start the fade with transparent
            }

            // Handle the fading effect if it's active
            if (isFading)
            {
                fadeAlpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; // Gradually increase alpha

                // Once the fade is fully opaque (alpha >= 1.0), switch the scene
                if (fadeAlpha >= 1.0f)
                {
                    LoadScene();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw your game scene here

            // If fading, draw the fade effect
            if (isFading)
            {
                spriteBatch.Draw(GetFadeTexture(), new Rectangle(0, 0, _window.ClientBounds.Width, _window.ClientBounds.Height), _fadeColor * fadeAlpha);
            }
        }

        private Texture2D GetFadeTexture()
        {
            // Create a single-pixel texture used for the fade effect
            Texture2D fadeTexture = new Texture2D(_device, 1, 1);
            fadeTexture.SetData(new[] { Color.White }); // White texture will be tinted by the fadeColor
            return fadeTexture;
        }

        private void LoadScene()
        {
            isFading = false;
            _currentlySwitching = false;
            ChangeGameState(_currentGameState);
        }

        public void OnSwitchScene(Color fadeColor, GameState gameState)
        {
            if (!_currentlySwitching)
            {
                _currentlySwitching = true;
                _fadeColor = fadeColor;
                _currentGameState = gameState;
            }
        }
    }
}
