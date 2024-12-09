using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public abstract class GameObject
    {
        protected Texture2D Texture;

        protected Vector2 Position { get; set; }
        protected Rectangle Rect;
        protected Color Color;
        protected float Rotation;
        protected float LayerDepth;
        protected Vector2 Origin;
        protected SpriteEffects SpriteEffect;
        public abstract Rectangle Collision { get; }

        public GameObject(Texture2D texture, Rectangle rect, Color color, float rotation, float layerDepth, Vector2 origin)
        {
            Texture = texture;
            Rect = rect;
            Color = color;
            Rotation = rotation;
            LayerDepth = layerDepth;
            Origin = origin;
        }
        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rect, null, Color, Rotation, Origin, SpriteEffect, LayerDepth);
        }
        public abstract void OnCollision(GameObject gameObject);
    }
}
