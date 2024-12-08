using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class UIImage : StaticUIElement
    {
        private Texture2D _texture;

        private Rectangle _imageSourceRect;


        public UIImage(Texture2D texture, Vector2 position, Color color, float size, Vector2 origin, Rectangle imageRect, float layerDepth = 0, float rotation = 0) : base(position, color, size, origin, layerDepth, rotation)
        {
            _texture = texture;
            _imageSourceRect = imageRect;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, _imageSourceRect, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2? offset = null)
        {
            var drawPos = offset ?? Position;
            spriteBatch.Draw(_texture, drawPos, _imageSourceRect, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public override void Update(GameTime gameTime)
        {
        }
    }
}
