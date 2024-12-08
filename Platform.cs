using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Texture2D _texture;

        //Collision needs to be added
        public override Rectangle Collision => new Rectangle(0, 0, 0, 0);

        private Rectangle _sourceRec;

        public Platform(Texture2D texture, Vector2 position, PlatformType type, Color color, float rotation, float size, float layerDepth) : base(texture, position, color, rotation, size, layerDepth, Vector2.Zero /* Maybe add an origo to platforms*/ )
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
            //If I have time add so that the platforms also dynamically tile
            //This is old code but could probably change it.
            //  TileEditor.rectMap.TryGetValue(LevelManager.GetCurrentLevel.GetTileKey(Pos.ToPoint()), out Rectangle sourceRect);

            spriteBatch.Draw(_texture, Position, _sourceRec, Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

          //  if (Type == TileType.Breakable || Type == TileType.Unbreakable)
          //  {
          //      TileEditor.rectMap.TryGetValue(LevelManager.GetCurrentLevel.GetTileKey(Pos.ToPoint()), out Rectangle sourceRect);

          //      spriteBatch.Draw(_texture, Pos, sourceRect, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
          //  }
          //else
          //  {
          //      spriteBatch.Draw(_texture, Pos, _sourceRec, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
          //  }
        }
        public void SwitchTile(Texture2D newTexture)
        {
            _texture = newTexture;
        }

        public override void OnCollision(GameObject gameObject)
        {
            throw new NotImplementedException();
        }
    }
}
