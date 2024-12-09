using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace Platformer
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Remove and add new textures
        private const string _textures = "GameColors,mario-pauline-transparent,wall,ghost,pacman,tiles,pacman-character-yellow,pacman-character-pink,pacman-heart,empty,pacman_white,pacman_deathClip";
        private const string _sounds = "DeathSound,HardPop,FlameDamage,CoinPickupSound";
        private const string _music = "BackgroundMusic";
        private const string _font = "GameText";
        private const string _effects = "FlashEffect";

       // private PlayerController _playerController;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int heightOffset = 200;
            int widthOffset = 2;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - heightOffset;
            int fixedWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / widthOffset;

            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.PreferredBackBufferWidth = fixedWidth;
            _graphics.ApplyChanges();

            GameManager.SetUp(Window, Content, GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ResourceManager.LoadResources(Content, _textures, _sounds, _music, _font, _effects);

            //Rectangle[] playerWalking =
            //{
            //    new Rectangle(0, 0, 39, 39),
            //    new Rectangle(40, 0, 39, 39),
            //    new Rectangle(80, 0, 39, 39),
            //    new Rectangle(120, 0, 39, 39)
            //};

            //Dictionary<string, AnimationClip> playerAnimations = new Dictionary<string, AnimationClip>()
            //{
            //    {"Idle",  new AnimationClip(playerWalking, 12f)}
            //};

            //_playerController = new PlayerController(ResourceManager.GetTexture("pacman"), new Vector2(300, 300), Color.White, 0f, 1f, 0f, new Vector2(20, 20), playerAnimations);


            GameManager.ContentLoad();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            GameManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
