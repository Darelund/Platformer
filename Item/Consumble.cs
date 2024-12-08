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
    public class Consumble : Item
    {
        public Consumble(Texture2D texture, Vector2 position, Rectangle rect, Color color, float rotation, float size, float layerDepth, Vector2 origin) : base(texture, position, rect, color, rotation, size, layerDepth, origin)
        {
            _minScore = 10;
            _maxScore = 50;
            _score = _random.Next(_minScore, _maxScore);
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject is PlayerController)
            {
                var player = (PlayerController)gameObject;
                int healthToGive = 1;
                player.RecieveHealth(healthToGive);

                GameManager.GameObjects.Remove(this);
                ScoreManager.UpdateScore(_score);
                AudioManager.PlaySoundEffect("CoinPickupSound");
            }
        }
        public override void Update(GameTime gameTime)
        {
                Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
