namespace BulletHell
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using global::BulletHell.Game_Utilities;
    using global::BulletHell.Sprites;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.States;
    using global::BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class BulletHell : Game
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private List<Sprite> sprites;
        private List<Wave> waves;
        private double timeUntilNextWave = 0;
        private int lives = 3;
        private bool finalBossDefeated = false;

        private State _currentState;

        private State _nextState;

        public static Random Random;

        public static int ScreenWidth = 800;
        public static int ScreenHeight = 480;

        // Initialize screensize and other game properties
        public BulletHell()
        {
            Graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            UtlilityManager.Initialize(this.Content);
            Random = new Random();
        }

        public static GraphicsDeviceManager Graphics { get; set; }

        // Set any values that weren't set in the constructor for BulletHell
        protected override void Initialize()
        {
            IsMouseVisible = true;
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        // Load in content (sprites, assets, etc.)
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            _currentState = new MenuState(this, Graphics.GraphicsDevice, Content);
            _currentState.LoadContent();

        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _currentState = new MenuState(this, Graphics.GraphicsDevice, Content);
                _nextState = null;
                _currentState.LoadContent();
            }

            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.LoadContent();

                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            if (this.lives == 0 || this.finalBossDefeated)
            {
                this.EndGamePrompt();
            }

            base.Update(gameTime);
        }

        // This is called when the game should draw itself
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);


            _currentState.Draw(gameTime, spriteBatch);
            
            base.Draw(gameTime);
        }

    }
}
