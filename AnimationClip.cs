using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class AnimationClip
    {
        enum PlayMode { Play, Pause };
        Rectangle[] srcRects;
        private int CurrentAnimationRect = 0;
        PlayMode playMode = PlayMode.Play;
        float animTime = 0.0f;
        float speed;
        public AnimationClip(Rectangle[] srcRects, float speed)
        {
            this.srcRects = srcRects;
            this.speed = speed;
        }
        public void SetPlay() { playMode = PlayMode.Play; }
        public void SetPause() { playMode = PlayMode.Pause; }
        public void SetSpeed(float speed) { this.speed = speed; }
        public void Update(float dt)
        {
            if (playMode == PlayMode.Pause) return;
            animTime += dt * speed;
        }
        public Rectangle GetCurrentSourceRectangle()
        {
            int rect_index = (int)animTime % srcRects.Length;
            CurrentAnimationRect = rect_index;
            return srcRects[rect_index];
        }
        public bool HasLoopedOnce()
        {
            if (CurrentAnimationRect >= srcRects.Length - 1)
            {
                CurrentAnimationRect = 0;
                Rewind();
                return true;
            }
            return false;
        }
        //public bool HaveChangedAnimationRect()
        //{

        //}
        public void Mirror()
        {
            //  SpriteEffects.
        }
        private void Rewind()
        {
            animTime = 0.0f;
        }
    }
}
