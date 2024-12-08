using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal class AnimatedSpriteUI : AnimatedUIElement
    {
        public AnimatedSpriteUI(Texture2D texture, Vector2 position, Point currentFrame, Point frameSize, Point sheetSize, Color color, float size, Vector2 origin, int millisecondsPerFrame = 16, float layerDepth = 0, float rotation = 0) : base(texture, position, currentFrame, frameSize, sheetSize, color, size, origin, millisecondsPerFrame, layerDepth, rotation)
        {
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
