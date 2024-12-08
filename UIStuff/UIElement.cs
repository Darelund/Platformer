using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class UIElement
    {
        public Vector2 Position { get; set; }
        protected Color CurrentColor;
        protected float Size;
        protected float LayerDepth;
        protected float Rotation;
        protected Vector2 Origin;

        public UIElement(Vector2 position, Color color, float size, Vector2 origin, float layerDepth = 0f, float rotation = 0)
        {
            Position = position;
            CurrentColor = color;
            Size = size;
            LayerDepth = layerDepth;
            Rotation = rotation;
            Origin = origin;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
