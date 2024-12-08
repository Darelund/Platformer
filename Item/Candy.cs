using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Candy : Item
    {
        public Candy(Texture2D texture, Vector2 position, Rectangle rect, Color color, float rotation, float size, float layerDepth, Vector2 origin) : base(texture, position, rect, color, rotation, size, layerDepth, origin)
        {
            _minScore = 10;
            _maxScore = 50;
            _score = _random.Next(_minScore, _maxScore);


            //De fuckar på något sätt
            _rect = new Rectangle(0, 0, 15, 15);
            Size = 0.5f;
            
        }
        public override void OnCollision(GameObject gameObject)
        {
            //Maybe add it back to a pool and respawn it
            if (gameObject is PlayerController)
            {
                GameManager.GameObjects.Remove(this);
                ScoreManager.UpdateScore(_score);
                int resetRotation = 0;
                Rotation = resetRotation;
                AudioManager.PlaySoundEffect("CoinPickupSound");
            }
            // GameManager.GameObjects.Remove(this);
        }
        public override void Update(GameTime gameTime)
        {
            Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
       
    }
}
