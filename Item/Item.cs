using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{

    public enum ItemState
    {
        pickupState,
        usingState
    }
    public abstract class Item : GameObject
    {
        protected int _minScore;
        protected int _maxScore;
        protected Random _random = new Random();
        protected int _score;
        protected Rectangle _rect;
        public override Rectangle Collision
        {
            get
            {
                return new Rectangle((int)Position.X - (int)(Origin.X * Size), (int)Position.Y - (int)(Origin.Y * Size), (int)(_rect.Width * Size), (int)(_rect.Height * Size));
            }
        }

        

        public Item(Texture2D texture, Vector2 position, Rectangle rect, Color color, float rotation, float size, float layerDepth, Vector2 origin) : base(texture, position, color, rotation, size, layerDepth, origin)
        {
            _rect = rect;
        }
        public abstract override void OnCollision(GameObject gameObject);
        public abstract override void Update(GameTime gameTime);
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, _rect, Color, Rotation, Origin, Size, SpriteEffect, LayerDepth);
        }

    }
}
