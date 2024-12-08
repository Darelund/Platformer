using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class StaticBackground : StaticUIElement
    {
        private Texture2D _texture;
        public StaticBackground(Texture2D texture, Vector2 position, Color color, float size, Vector2 origin, float layerDepth = 0, float rotation = 0) : base(position, color, size, origin, layerDepth, rotation)
        {
            _texture = texture;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
    }
}
