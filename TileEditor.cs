using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    public static class TileEditor
    {
        //Under construction
        //Orkade inte göra klart skitet
        public static Dictionary<string, Rectangle> rectMap =
        new Dictionary<string, Rectangle>
        {
            { "0000", new Rectangle(0, 0, 32, 32) },
            { "0100", new Rectangle(33, 0, 32, 32) },
            { "1000", new Rectangle(66, 0, 32, 32) },
            { "1100", new Rectangle(99, 0, 32, 32) },
            { "0001", new Rectangle(0, 33, 32, 32) },
            { "0101", new Rectangle(33, 33, 32, 32) },
            { "1001", new Rectangle(66, 33, 32, 32) },
            { "1101", new Rectangle(99, 33, 32, 32) },
            { "0010", new Rectangle(0, 66, 32, 32) },
            { "0110", new Rectangle(33, 66, 32, 32) },
            { "1010", new Rectangle(66, 66, 32, 32) },
            { "1110", new Rectangle(99, 66, 32, 32) },
            { "0011", new Rectangle(0, 99, 32, 32) },
            { "0111", new Rectangle(33, 99, 32, 32) },
            { "1011", new Rectangle(66, 99, 32, 32) },
            { "1111", new Rectangle(99, 99, 32, 32) }
        };

        private static Texture2D Pixel { get; set; }
        private static bool IsEnabled { get; set; }

        private static bool IsKeyPressed = false;



        public static bool IsEditingMap { get; set; }

        private static SpriteFont _buttontexture = ResourceManager.GetSpriteFont("GameText");
        private static readonly (Color color1, Color color2, Color color3) _defaultButtonColors = (Color.White, Color.LightBlue, Color.DarkBlue);
        private static readonly Vector2 _defaultPosition = new Vector2(GameManager.Window.ClientBounds.Width / 2 - 200, 100);
        private static readonly Vector2 _defaultOrigin = Vector2.Zero;
        private const string _buttonDisplayText = "Open";
        private const float _buttonDefaultSize = 1;
        private const float _buttonDefaultLayderDepth = 0.1f;
        private static int _buttonValue = LevelManager.LevelIndex;
        private static readonly Action<object> _buttonMethod = OpenMapFile;

        private static Button _button = new Button(_buttontexture, _defaultButtonColors, _defaultPosition, _defaultOrigin, _buttonValue, _buttonMethod, _buttonDisplayText, _buttonDefaultSize, _buttonDefaultLayderDepth);
      //  private static UIImage _Image1 = new UIImage()


        private static Rectangle Background = new Rectangle(GameManager.Window.ClientBounds.Width / 4, GameManager.Window.ClientBounds.Height / 10, GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 2);

        private static Rectangle TopBackground = new Rectangle(GameManager.Window.ClientBounds.Width / 4, GameManager.Window.ClientBounds.Height / 10, GameManager.Window.ClientBounds.Width / 2, GameManager.Window.ClientBounds.Height / 18);
        private static Rectangle switchInputRec = new Rectangle(GameManager.Window.ClientBounds.Width / 2 + 100, GameManager.Window.ClientBounds.Height / 5 + 130, 25, 25);
        private static Rectangle instantFireRateRec = new Rectangle(GameManager.Window.ClientBounds.Width / 2 + 100, GameManager.Window.ClientBounds.Height / 5 + 7, 25, 25);

        private static SpriteFont headerFont = ResourceManager.GetSpriteFont("GameText");

        // Rainbow grejer, klass bör addas snart

        static Color[] RainbowColors = new Color[]
        {
            new Color(255, 0, 0),
            new Color(255, 128, 0),
            new Color(255, 255, 0),
            new Color(0, 255, 0),
            new Color(0, 255, 128),
            new Color(0, 255, 255),
            new Color(0, 128, 255),
            new Color(0, 0, 255),
        };

        private static Color rainbowColor = new Color(0, 0, 0);
        private static int rainbowColorI = 0;
        private static Stopwatch watch = new Stopwatch();

        private static bool _bsChecker = false;

        static TileEditor()
        {
            Pixel = new Texture2D(GameManager.Device, 1, 1);
            Pixel.SetData(new Color[] { Color.White });

            watch.Start();
        }

        public static void Update()
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.H) && !IsKeyPressed)
            {
                IsKeyPressed = true;
                IsEnabled = !IsEnabled;
            }

            if (state.IsKeyUp(Keys.H))
            {
                IsKeyPressed = false;
            }

            if (watch.ElapsedMilliseconds >= 200)
            {
                rainbowColor = RainbowColors[rainbowColorI];
                rainbowColorI++;
                watch.Restart();

                if (rainbowColorI >= 8)
                {
                    rainbowColorI = 0;
                }
            }

            if (InputManager.CurrentMouse.LeftButton == ButtonState.Pressed && !_bsChecker)
            {
                float targetDistanceFromButtons = 25f;
                float targetFireRateDistance = Vector2.Distance(InputManager.CurrentMouse.Position.ToVector2(), new Vector2(instantFireRateRec.X, instantFireRateRec.Y));
                if (targetFireRateDistance < targetDistanceFromButtons)
                {
                    _bsChecker = true;
                    IsEditingMap = !IsEditingMap;
                }
               
            }
            if (InputManager.CurrentMouse.LeftButton == ButtonState.Released)
            {
                _bsChecker = false;
            }
        }

        public static void Draw(SpriteBatch batch)
        {
            if (!IsEnabled) return;
           


            if(InputManager.CurrentKeyboard.IsKeyDown(Keys.P))
            {
                OpenMapFile(1);
            }

        }
        private static void OpenMapFile(object levelNum)
        {
            var toNum = (int)levelNum;

            string mapToLoadString = toNum switch
            {
                1 => "Content/Map1.txt",
                2 => "Content/Map2.txt",
                3 => "Content/Map3.txt",
                _ => "ThisWillNeverHappen"
            };
            
            Process process = Process.Start("notepad.exe", mapToLoadString);

            // Wait for the process to exit
            process.WaitForExit();

            // Reload the map
            Debug.WriteLine("Notepad has closed. Reloading the map...");
            LevelManager.SpecificLevel(toNum, true);
        }
    }
}
