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
    public enum EnemyType
    {
        Dumb,
        Smart,
        Follow
    }
    public enum EnemyState
    {
        Roaming,
        Fleeing
    }
    public class EnemyController : AnimatedGameObject
    {
        private Vector2 destination;
        private Vector2 direction;
        private bool moving = false;
        private int _points = 50;
        private float _speed = 2;

        private EnemyType _enemyType;
        private EnemyState _enemyState = EnemyState.Roaming;


        private float _health = 1;
        public float Health
        {
            get => _health;
            private set => _health = value;
        }
        public EnemyController(Texture2D texture, Vector2 position, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, color, rotation, size, layerDepth, origin, animationClips)
        {
            Random ran = new Random();
            int enemyType = ran.Next(0, 3);
            _enemyType = (EnemyType)enemyType;

            if(_enemyType == EnemyType.Smart)
            {
                direction = GetRandomDirection();
            }
            Debug.WriteLine(_enemyType);
        }

        public override void OnCollision(GameObject gameObject)
        {
            if(gameObject is PlayerController)
            {
                var player = gameObject as PlayerController;
                if (player.PlayerState == PlayerState.Attacking)
                {
                    int score = 500;
                    ScoreManager.UpdateScore(score);
                    GameManager.GameObjects.Remove(this);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!moving)
            {
                UpdateDirection(direction);
            }
            else
            {
                Position += direction * (Level.TileSize.X * _speed) * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Check if we are near enough to the destination, if we are, stop moving, which means find a new direction
                if (Vector2.Distance(Position, destination) < 1)
                {
                    Position = destination;
                    moving = false;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
        private void HandleAnimation(Vector2 dir, GameTime gameTime)
        {
            //if (dir.Length() <= 0) return;
            //else
            //{
            //    AnimationFlip(-dir);
            //}
            //base.Update(gameTime);
        }
      
        public void UpdateDirection(Vector2 dir)
        {
            if(PlayerController.Instance.PlayerState == PlayerState.Attacking)
            {
                _enemyState = EnemyState.Fleeing;
                SwitchAnimation("Fleeing1");
            }
            else if (PlayerController.Instance.PlayerState == PlayerState.Walking)
            {
                _enemyState = EnemyState.Roaming;
                SwitchAnimation("Roaming");
            }

            if (_enemyState == EnemyState.Roaming)
            {
             
                switch (_enemyType)
                {
                    case EnemyType.Dumb:
                        direction = GetRandomDirection();
                        if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                        {
                            var newDestination = Position + (direction * Level.TileSize);
                            destination = newDestination;
                            moving = true;
                        }
                        break;
                    case EnemyType.Smart:
                        if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                        {
                            var newDestination = Position + (direction * Level.TileSize);
                            destination = newDestination;
                            moving = true;
                        }
                        else
                        {
                            direction = GetRandomDirection();
                            if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                            {
                                var newDestination = Position + (direction * Level.TileSize);
                                destination = newDestination;
                                moving = true;
                            }
                        }
                        break;
                    case EnemyType.Follow:

                        var playerPos = PlayerController.Instance.Position;

                        //First check what direction the player is in
                        var diff = playerPos - Position;
                        //Choose direction based on X/Y
                        if(Math.Abs(diff.X) > Math.Abs(diff.Y))
                        {
                            direction = diff.X > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                        }
                        else if (Math.Abs(diff.Y) > Math.Abs(diff.X))
                        {
                            direction = diff.Y > 0 ? new Vector2(0, 1) : new Vector2(0, -1);
                        }
                       //Check if that direction is possible to go to
                        if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                        {
                            var newDestination = Position + (direction * Level.TileSize);
                            destination = newDestination;
                            moving = true;
                        }
                        else//If blocked do a bunch of bullshit to reach the target
                        {
                            if (Math.Abs(diff.X) > Math.Abs(diff.Y))
                            {
                                direction = diff.X > 0 ? new Vector2(0, -1) : new Vector2(0, 1);
                            }
                            else if (Math.Abs(diff.Y) > Math.Abs(diff.X))
                            {
                                direction = diff.Y > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                            }
                            if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                            {
                                var newDestination = Position + (direction * Level.TileSize);
                                destination = newDestination;
                                moving = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Flee from the player (opposite direction)
                var playerPos = PlayerController.Instance.Position;
                var diff = Position - playerPos;
                if (Math.Abs(diff.X) > Math.Abs(diff.Y))
                {
                    direction = diff.X > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                }
                else
                {
                    direction = diff.Y > 0 ? new Vector2(0, 1) : new Vector2(0, -1);
                }
                if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                {
                    var newDestination = Position + (direction * Level.TileSize);
                    destination = newDestination;
                    moving = true;
                }
                else//If blocked do a bunch of bullshit to reach the target
                {
                    if (Math.Abs(diff.X) > Math.Abs(diff.Y))
                    {
                        direction = diff.X > 0 ? new Vector2(0, -1) : new Vector2(0, 1);
                    }
                    else if (Math.Abs(diff.Y) > Math.Abs(diff.X))
                    {
                        direction = diff.Y > 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                    }
                    if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                    {
                        var newDestination = Position + (direction * Level.TileSize);
                        destination = newDestination;
                        moving = true;
                    }
                    else
                    {
                        if (Math.Abs(diff.X) > Math.Abs(diff.Y))
                        {
                            direction = diff.X < 0 ? new Vector2(0, -1) : new Vector2(0, 1);
                        }
                        else if (Math.Abs(diff.Y) > Math.Abs(diff.X))
                        {
                            direction = diff.Y < 0 ? new Vector2(1, 0) : new Vector2(-1, 0);
                        }
                        if (!LevelManager.GetCurrentLevel.IsTileWall(Position, direction, this))
                        {
                            var newDestination = Position + (direction * Level.TileSize);
                            destination = newDestination;
                            moving = true;
                        }
                    }
                }
            }
        }
        private Vector2 GetRandomDirection()
        {
            Random ran = new Random();
            int randomDirection = ran.Next(0, 4);
            Vector2 direction;

            return direction = randomDirection switch
            {
                0 => new Vector2(0, 1),
                1 => new Vector2(0, -1),
                2 => new Vector2(1, 0),
                3 => new Vector2(-1, 0),
            };
        }
    }
}
