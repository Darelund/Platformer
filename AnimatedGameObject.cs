using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public abstract class AnimatedGameObject : GameObject
    {
        protected Dictionary<string, AnimationClip> _animationClips;
        protected AnimationClip _currentClip;
        private float _deltaTime;
        public override Rectangle Collision
        {
            get
            {
                Rectangle rec = _currentClip.GetCurrentSourceRectangle();
                return new Rectangle((int)Position.X - (int)Origin.X * (int)Size, (int)Position.Y - (int)Origin.Y * (int)Size, rec.Width * (int)Size, rec.Height * (int)Size);
            }
        }
        public AnimatedGameObject(Texture2D texture, Vector2 position, Color color, float rotation, float size, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, position, color, rotation, size, layerDepth, origin)
        {
            _animationClips = animationClips;
            //Todo maybe make it possible to choose what animation to start at?
            _currentClip = _animationClips[_animationClips.Keys.First()];
        }

        public override void Update(GameTime gameTime)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentClip.Update(_deltaTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, _currentClip.GetCurrentSourceRectangle(), Color, Rotation, Origin, Size, SpriteEffect, LayerDepth);
        }
        protected void SwitchAnimation(string name)
        {
            if (_currentClip != _animationClips[name])
                _currentClip = _animationClips[name];
        }

    }
}
