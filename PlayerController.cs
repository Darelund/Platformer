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
    public enum PlayerState
    {
        Walking,
        Attacking
    }
    public class PlayerController : AnimatedGameObject
    {
        public PlayerState PlayerState = PlayerState.Walking;
        private Vector2 direction;
        private Vector2 newDirection;
        private Vector2 destination;
        bool moving = false;

        private float _maxHealth = 3;
        private float _currentHealth;
        public float Health
        {
            get => _currentHealth;
        }
        public bool IsImmune { get; set; } = false;
        private bool _inAttackMode = false;
        private bool _isActive = true;
        private float _speed = 50;

        private const string _attackAnim = "Attack";
        private const string _dieAnim = "Die";
        private const string _idleAnim = "Idle";

        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                else
                    return null;
            }
        }
        public PlayerController(Texture2D texture, Vector2 position, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, color, rotation, size, layerDepth, origin, animationClips)
        {
            _currentHealth = _maxHealth;
            _instance = this;
        }

        public override void Update(GameTime gameTime)
        {
            if (!_isActive) return;

            if (!moving)
            {
                newDirection = GetNewDirection();

                if (newDirection.Length() != 0)
                    direction = newDirection;

                if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                {
                    var newDestination = Position + (direction * Level.TileSize);
                    destination = newDestination;
                    moving = true;
                }
            }
            else
            {
                Position += direction * (Level.TileSize.X + _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(Position, destination) < 1)
                {
                    Position = destination;
                    moving = false;
                }

                if(InputManager.DebugButton())
                {
                    TakeDamage(1);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        private Vector2 GetNewDirection()
        {
            Vector2 direction = Vector2.Zero;
           

            direction = InputManager.GetMovement();
            if (LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
            {
                direction = Vector2.Zero;
                //Not necessary but makes the code clearer
                //Or I actually think I need to set it to false
                //Or I do think No I don't need it because I set the direction to zero, so it will go back to false in the Update either way
                //Eh lets just leave it
                moving = false;
            }
            else
            {
                if (direction.Length() != 0)
                {
                    if (direction.X != 0)
                    {
                        if (direction.X == -1)
                        {
                            Rotation = MathHelper.ToRadians(180);
                        }
                        else
                        {
                            Rotation = MathHelper.ToRadians(0);
                        }
                    }
                    else
                    {
                        if (direction.Y == -1)
                        {
                            Rotation = MathHelper.ToRadians(270);
                        }
                        else
                        {
                            Rotation = MathHelper.ToRadians(90);
                        }
                    }
                }
            }
           
            return direction;
        }
        
        public void TakeDamage(int amount)
        {
            //Maybe add thís back later
            // if(!IsImmune)
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                _isActive = false;
                HighScore.UpdateScore(GameManager.Name, ScoreManager.PlayerScore, LevelManager.LevelIndex);
                string deathSound = "DeathSound";

                //Maybe add a death animation
                //SwitchAnimation(_dieAnim);
                //string newTexture = "pacman_deathClip";
                //Texture = ResourceManager.GetTexture(newTexture);

                AudioManager.PlaySoundEffect(deathSound);
            }
            Debug.WriteLine(_currentHealth);
        }
        public void RecieveHealth(int amount)
        {
            if(_currentHealth < _maxHealth)
            {
                _currentHealth += amount;
            }
        }
        public override void OnCollision(GameObject gameObject)
        {
            if (!_isActive) return;
            if (gameObject is EnemyController && PlayerState == PlayerState.Walking)
            {
                // var enemsy = (EnemyController)gameObject;
                if (!IsImmune)
                {
                    Debug.WriteLine("Taking damage");
                    float volume = 0.5f;
                    string damageSound = "FlameDamage";
                    AudioManager.PlaySoundEffect(damageSound, volume);
                    float flashTime = 2f;
                    Color flashColor = Color.White;
                    var flash = new FlashEffect(ResourceManager.GetEffect("FlashEffect"), flashTime, this, flashColor);
                    GameManager.AddFlashEffect(flash);
                    IsImmune = true;
                    flash.OnFlashing += ImmuneHandler;
                    //YEs memory leak, so what??? Or is it?
                    TakeDamage(1);
                }
            }
        }
        public void ImmuneHandler(bool immune)
        {
            IsImmune = immune;
        }
        public void GettingTeleported()
        {
           // direction = Vector2.Zero;
            moving = false;
        }
    }
}
