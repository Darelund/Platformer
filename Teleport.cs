using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Teleport : GameObject
    {
        //If I feel like it I will add an animation to the teleport
        private Vector2 _positionToTeleportTo;

        public Teleport(Texture2D texture, Vector2 position, Color color, float rotation, float size, float layerDepth, Vector2 origin) : base(texture, position, color, rotation, size, layerDepth, origin)
        {
        }

        public override Rectangle Collision
        {
            get
            {
                return new Rectangle((int)Position.X - (int)Origin.X * (int)Size, (int)Position.Y - (int)Origin.Y * (int)Size, (int)(Texture.Width * Size), (int)(Texture.Height * Size));
            }
        }

        public override void OnCollision(GameObject gameObject)
        {
            if(gameObject is PlayerController)
            {
                var player = gameObject as PlayerController;
                Vector2 playerOffset = new Vector2(14.5f, 14.5f);
               // player.Position = new Vector2(player.Position.X + 32, 96 + playerOffset);
                player.Position = _positionToTeleportTo + playerOffset;
                player.GettingTeleported();
                //Also rotate it
            }
         //   Debug.WriteLine("Player");
        }

        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color, Rotation, Origin, Size, SpriteEffect, LayerDepth);
        }
        public void SetPosition(Vector2 pos)
        {
            _positionToTeleportTo = pos;
        }
    }
}
