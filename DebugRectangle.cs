using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public static class DebugRectangle
    {
        private static Texture2D texture;
        public static void Init(GraphicsDevice graphicsDevice, int width, int height)
        {
            texture = new Texture2D(graphicsDevice, width, height);
            Color[] color = new Color[width * height];
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = Color.Green; //Always green for debug, or maybe red
            }
            texture.SetData(color);
        }
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}
