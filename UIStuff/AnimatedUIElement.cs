using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class AnimatedUIElement : UIElement
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;

        private Point _currentFrame;
        private Point _frameSize;
        private Point _sheetSize;

        private int _millisecondsPerFrame;
        private float _timeSinceLastFrame = 0;

        public AnimatedUIElement(Texture2D texture, Vector2 position, Point currentFrame, Point frameSize, Point sheetSize, Color color, float size, Vector2 origin, int millisecondsPerFrame = 16, float layerDepth = 0, float rotation = 0)
            : base(position, color, size, origin, layerDepth, rotation)
        {
            _texture = texture;
            _millisecondsPerFrame = millisecondsPerFrame;
            _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);

            _currentFrame = currentFrame;
            _frameSize = frameSize;
            _sheetSize = sheetSize;
        }

        public override void Update(GameTime gameTime)
        {
            _timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timeSinceLastFrame >= _millisecondsPerFrame)
            {
                _timeSinceLastFrame -= _millisecondsPerFrame;

                _currentFrame.X++;
                if (_currentFrame.X >= _sheetSize.X)
                {
                    _currentFrame.X = 0;
                    _currentFrame.Y++;
                    if (_currentFrame.Y >= _sheetSize.Y)
                    {
                        _currentFrame.Y = 0;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, new Rectangle(_currentFrame.X * _frameSize.X, _currentFrame.Y * _frameSize.Y, _frameSize.X, _frameSize.Y), CurrentColor, 0f, Origin, Size, SpriteEffects.None, LayerDepth);
        }
    }
}
