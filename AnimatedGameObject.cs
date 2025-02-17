﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
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
                return new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, Rect.Width, Rect.Height);
            }
        }
        public AnimatedGameObject(Texture2D texture, Rectangle rect, Color color, float rotation, float layerDepth, Vector2 origin, Dictionary<string, AnimationClip> animationClips) : base(texture, rect, color, rotation, layerDepth, origin)
        {
            _animationClips = animationClips;
            //Todo maybe make it possible to choose what animation to start at?
            _currentClip = _animationClips[_animationClips.Keys.First()];
            Position = new Vector2(rect.X, rect.Y); 
        }

        public override void Update(GameTime gameTime)
        {
            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentClip.Update(_deltaTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
           // spriteBatch.Draw(Texture, Rect, _currentClip.GetCurrentSourceRectangle(), Color, Rotation, Origin, SpriteEffect, LayerDepth);
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, Rect.Width, Rect.Height), _currentClip.GetCurrentSourceRectangle(), Color, Rotation, Origin, SpriteEffect, LayerDepth);

            spriteBatch.DrawRectangle(new Rectangle((int)Position.X - (int)Origin.X, (int)Position.Y - (int)Origin.Y, Rect.Width, Rect.Height), Color.White);
        }
        protected void SwitchAnimation(string name)
        {
            if (_currentClip != _animationClips[name])
                _currentClip = _animationClips[name];
        }

    }
}
