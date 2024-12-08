using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public enum TileType
    {
        Wall,
        Unbreakable,
        Path
    }
    public class Tile
    {
        public TileType Type { get; set; }
        private Texture2D _texture;
        public Vector2 Pos { get; private set; }
        public char Name { get; set; }
        private Color _color;
        private const float _FallSpeed = 90f;
        private Rectangle _sourceRec;
        public Tile(Vector2 pos, Texture2D texture, TileType type, Color color, char name)
        {
            Pos = pos;
            _texture = texture;
            Type = type;
            _color = color;
            Name = name;
        }
        public virtual void Update(GameTime gameTime)
        {
            
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Type == TileType.Wall || Type == TileType.Unbreakable)
            {
                TileEditor.rectMap.TryGetValue(LevelManager.GetCurrentLevel.GetTileKey(Pos.ToPoint()), out Rectangle sourceRect);

                spriteBatch.Draw(_texture, Pos, sourceRect, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }
          else
            {
                spriteBatch.Draw(_texture, Pos, _sourceRec, _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }
        }
        public void SwitchTile(Texture2D newTexture)
        {
            _texture = newTexture;
        }
    }
}
