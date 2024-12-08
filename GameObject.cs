using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class GameObject
    {
        protected Texture2D Texture;
        public Vector2 Position { get; set; }
        protected Color Color;
        protected float Rotation;
        protected float Size;
        protected float LayerDepth;
        protected Vector2 Origin;
        protected SpriteEffects SpriteEffect;
        public abstract Rectangle Collision { get; }

        public GameObject(Texture2D texture, Vector2 position, Color color, float rotation, float size, float layerDepth, Vector2 origin)
        {
            Texture = texture;
            Position = position;
            Color = color;
            Rotation = rotation;
            Size = size;
            LayerDepth = layerDepth;
            Origin = origin;
        }
        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Size, SpriteEffect, LayerDepth);
        }
        public abstract void OnCollision(GameObject gameObject);
    }
}
