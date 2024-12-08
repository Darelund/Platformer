using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class StaticUIElement : UIElement
    {
        public StaticUIElement(Vector2 position, Color color, float size, Vector2 origin, float layerDepth = 0, float rotation = 0) : base(position, color, size, origin, layerDepth, rotation)
        {
        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
