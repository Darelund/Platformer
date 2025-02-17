﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Platformer
{
    public enum PlatformType
    {
        Breakable,
        Unbreakable,
      //  Passable // maybe add later
    }
    public class Platform : GameObject
    {
        public PlatformType Type { get; set; }

        //Collision needs to be added
        public override Rectangle Collision => Rect;

        //Not used right now

        public Platform(Texture2D texture, Rectangle rect, PlatformType type, Color color, float rotation, float layerDepth) : base(texture, rect, color, rotation, layerDepth, Vector2.Zero /* Maybe add an origo to platforms*/ )
        {
            Type = type;
        }

        //public Platform(Vector2 pos, Texture2D texture, PlatformType type, Color color)
        //{
        //    Pos = pos;
        //    _texture = texture;
        //    Type = type;
        //    _color = color;
        //}
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rect, null, Color, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
        }
        public void SwitchTile(Texture2D newTexture)
        {
            Texture = newTexture;
        }

        public override void OnCollision(GameObject gameObject)
        {
          //  throw new NotImplementedException();
        }

    }
}
