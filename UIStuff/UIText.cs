using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class UIText : StaticUIElement
    {
        private SpriteFont _font;
        public string _text;
        int centerOffset = 2;
        public UIText(SpriteFont font, string text, Vector2 position, Color color, float size, Vector2 origin, float layerDepth = 0, float rotation = 0) : base(position, color, size, origin, layerDepth, rotation)
        {
            _font = font;
            _text = text;
            Origin = new Vector2((int)(_font.MeasureString(_text).X * Size) / centerOffset, (int)(_font.MeasureString(_text).Y * Size) / centerOffset);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, Position, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2? offset = null)
        {
            var drawPos = offset ?? Position;
            spriteBatch.DrawString(_font, _text, drawPos, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
        public override void Update(GameTime gameTime)
        {
        }
        public void UpdateText(string newText)
        {
            _text = newText;
        }
    }
}
