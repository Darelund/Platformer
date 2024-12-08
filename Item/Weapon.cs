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

    public class Weapon : Item
    {
        public ItemState state { get; private set; } = ItemState.pickupState;
        protected float _lifeTimeDuration = 5f;
        protected float _remainingTimeLeft = 0;

        private PlayerController _target;

        public Weapon(Texture2D texture, Vector2 position, Rectangle rect, Color color, float rotation, float size, float layerDepth, Vector2 origin) : base(texture, position, rect, color, rotation, size, layerDepth, origin)
        {
            _minScore = 250;
            _maxScore = 500;
            _score = _random.Next(_minScore, _maxScore);

            _remainingTimeLeft = _lifeTimeDuration;
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (state == ItemState.usingState) return;

            if (gameObject is PlayerController)
            {
                _target = (PlayerController)gameObject;
                _target.PlayerState = PlayerState.Attacking;

                Color flashColor = Color.Red;
                var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), _lifeTimeDuration, _target, flashColor);
                GameManager.AddFlashEffect(flash);
                _target.IsImmune = true;
                flash.OnFlashing += _target.ImmuneHandler;

                state = ItemState.usingState;

                ScoreManager.UpdateScore(_score);
                AudioManager.PlaySoundEffect("CoinPickupSound");

                int resetRotation = 0;
                Rotation = resetRotation;
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (state == ItemState.pickupState)
                Rotation += (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
            {
                _remainingTimeLeft -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_remainingTimeLeft <= 0)
                {
                    _target.PlayerState = PlayerState.Walking;
                    GameManager.GameObjects.Remove(this);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == ItemState.usingState) return;
            base.Draw(spriteBatch);
        }
    }
}
