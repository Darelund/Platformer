using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public class Button : StaticUIElement
    {
        //enum States
        //{
        //    Default,
        //    Hover,
        //    Pressed
        //}
        private SpriteFont _font;
        (Color defaultColor, Color hoverColor, Color pressedColor) _colors;
        private string _text;

        private bool _hasBeenPressed = false;
        private float _pressedTimer;
        private float _pressedDuration = 0.2f;
        int centerOffset = 2;

        public event Action<object> OnPressed;
        private object _clickParameter;

        public Rectangle Collision
        {
            get
            {
                int xCenterCollisionOffset = (int)(_font.MeasureString(_text).X * Size) / centerOffset;
                int yCenterCollisionOffset = (int)(_font.MeasureString(_text).Y * Size) / centerOffset;
                return new Rectangle((int)Position.X - xCenterCollisionOffset, (int)Position.Y - yCenterCollisionOffset, (int)(_font.MeasureString(_text).X * Size), (int)(_font.MeasureString(_text).Y * Size));
            }
        }


        public Button(SpriteFont font, (Color defaultColor, Color hoverColor, Color pressedColor) colors, Vector2 pos, Vector2 origin, object clickParameter, Action<object> onPressed = null, string text = "PlaceHolder", float size = 1f, float layerDepth = 0, float rotation = 0) : base(pos, colors.defaultColor, size, origin, layerDepth, rotation)
        {
            _font = font;
            _colors = colors;
            Position = pos;
            _text = text;
            Size = size;
            Origin = new Vector2((int)(_font.MeasureString(_text).X * Size) / centerOffset, (int)(_font.MeasureString(_text).Y * Size) / centerOffset);
            OnPressed = onPressed;
            _clickParameter = clickParameter;
        }



        public override void Update(GameTime gameTime)
        {
            if (_pressedTimer > 0)
            {
                _pressedTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_pressedTimer <= 0)
                {
                    _pressedTimer = 0;
                    OnPressed?.Invoke(_clickParameter);
                    _hasBeenPressed = false;
                }
            }
            if (Collision.Intersects(InputManager.MouseOver()))
            {
                CurrentColor = _colors.hoverColor;
                if (InputManager.LeftClick() && !_hasBeenPressed)
                {
                    CurrentColor = _colors.pressedColor;
                    _pressedTimer = _pressedDuration;
                    _hasBeenPressed = true;

                }
            }
            else
            {
                CurrentColor = _colors.defaultColor;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, Position, CurrentColor, Rotation, Origin, Size, SpriteEffects.None, LayerDepth);
        }
    }
}
