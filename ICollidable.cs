using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    internal class ICollidable
    {
        public Rectangle Collision { get; set; }
        public void OnCollision(GameObject gameObject)
        {
            // Code to execute if it did collide
        }
    }
}
