using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Platformer
{
    public enum PlayerState
    {
        Idle,
        Walking,
        Sprinting,
        Attacking,
        Jumping,
        Dying
    }
    public class PlayerController : AnimatedGameObject
    {
        public PlayerState PlayerState = PlayerState.Idle;
        private Vector2 direction;
        private Vector2 newDirection;
        private Vector2 destination;
        bool moving = false;
        private Platform _lastCollidedPlatform;

        private float _maxHealth = 3;
        private float _currentHealth;
        public float Health
        {
            get => _currentHealth;
        }
        public bool IsImmune { get; set; } = false;
        private bool _inAttackMode = false;
        private bool _isActive = true;

        private float normalSpeed = 75;
        private float _sprintSpeed = 50;
        private float _fallSpeed = 50f;

        private bool _isGrounded = false;
        private bool _isSprinting;
        private bool _isAttacking;

        private bool _isJumping;
        private bool _canJump = true;
        private float _timer = 0;
        private bool _canCollide = true;

        protected float Speed
        {
            get
            {
                if (!_isGrounded)
                {
                    return _fallSpeed;
                }
                else
                {
                    if (_isSprinting)
                    {
                        return normalSpeed + _sprintSpeed;
                    }
                    else
                    {
                        return normalSpeed;
                    }
                }
            }
        }

        private const string _attackAnim = "Attack";
        private const string _dieAnim = "Die";
        private const string _idleAnim = "Idle";
        private const string _walkAnim = "Walk";
        private const string _jumpAnim = "Jump";
        private const string _sprintAnim = "Sprint";
        private const string _climbAnim = "Climb";

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
        public PlayerController(Texture2D texture, Rectangle rect, Color color, float rotation, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, rect, color, rotation, layerDepth, origin, animationClips)
        {
            _currentHealth = _maxHealth;
            _instance = this;
            Position = new Vector2(rect.X, rect.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (!_isActive) return;

            StateMachine(gameTime);
            Debug.WriteLine(_isGrounded);
            if(_isGrounded && !_canJump)
            {
                Position = new Vector2(Position.X, Position.Y - 3);
                _timer += (float)gameTime.TotalGameTime.TotalSeconds;

                if(_timer > 3)
                {
                    _canJump = true;
                    _timer = 0;
                    _canCollide = true;
                }
            }
            else if(!_isGrounded)
            {
                //Apply gravity
                Position = new Vector2(Position.X, Position.Y + 1);
            }
            if(_isGrounded && (Rect.Right < _lastCollidedPlatform.Collision.Left || Rect.Left > _lastCollidedPlatform.Collision.Right))
            {
                _isGrounded = false;
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height), _currentClip.GetCurrentSourceRectangle(), Color, Rotation, Origin, SpriteEffect, LayerDepth);
            base.Draw(spriteBatch);
            if( _isGrounded )
                spriteBatch.DrawRectangle(_lastCollidedPlatform.Collision, Color.Red);
        }
        private void StateMachine(GameTime gameTime)
        {
            direction = InputManager.GetMovement();
           _isSprinting = InputManager.IsLeftShiftDown();
           _isAttacking = InputManager.LeftClick();
            if(_canJump)
           _isJumping = InputManager.IsSpacebarDown();

            switch (PlayerState)
            {
                case PlayerState.Idle:
                    if (_isAttacking) ChangeState(PlayerState.Attacking);
                    else if (_isJumping) ChangeState(PlayerState.Jumping);
                    else if (direction != Vector2.Zero) ChangeState(_isSprinting ? PlayerState.Sprinting : PlayerState.Walking);
                    break;

                case PlayerState.Walking:
                    if (direction == Vector2.Zero) ChangeState(PlayerState.Idle);
                    else if (_isJumping) ChangeState(PlayerState.Jumping);
                    else if (_isAttacking) ChangeState(PlayerState.Attacking);
                    else if (_isSprinting) ChangeState(PlayerState.Sprinting);
                    Move(gameTime);
                    break;

                case PlayerState.Sprinting:
                    if (!_isSprinting || direction == Vector2.Zero) ChangeState(PlayerState.Walking);
                    else if (_isJumping) ChangeState(PlayerState.Jumping);
                    else if (_isAttacking) ChangeState(PlayerState.Attacking);
                    Move(gameTime);
                    break;

                case PlayerState.Attacking:
                    if (!_isAttacking) ChangeState(PlayerState.Idle);
                    break;

                case PlayerState.Jumping:
                    _canJump = false;
                    _canCollide = false;
                    if (!_isJumping) ChangeState(PlayerState.Idle);
                    break;

                case PlayerState.Dying:
                    Debug.WriteLine("Dying");
                    break;
            }
        }
        private void ChangeState(PlayerState newState)
        {
           if(PlayerState == newState) return;

           PlayerState = newState;
            switch (PlayerState)
            {
                case PlayerState.Idle:
                    SwitchAnimation(_idleAnim);
                    break;
                case PlayerState.Walking:
                    SwitchAnimation(_walkAnim);
                    break;
                case PlayerState.Sprinting:
                    SwitchAnimation(_sprintAnim);
                    break;
                case PlayerState.Attacking:
                    SwitchAnimation(_attackAnim);
                    break;
                case PlayerState.Jumping:
                    SwitchAnimation(_jumpAnim);
                    break;
                case PlayerState.Dying:
                    SwitchAnimation(_dieAnim);
                    break;
            }
        }
        private void Move(GameTime gameTime)
        {
            AnimationFlip();
            Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        private void AnimationFlip()
        {
            if (direction.X != 0) SpriteEffect = direction.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
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
            if (!_isActive || !_canCollide) return;
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
            if (gameObject is Platform)
            {
                _isGrounded = true;
                Rect.Y = gameObject.Collision.Top-Rect.Height;
                Position = new Vector2(Position.X, Rect.Y + 12);

                _lastCollidedPlatform = (Platform)gameObject;
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
