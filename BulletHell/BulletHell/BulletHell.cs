namespace BulletHell
{
    using System;
    using global::BulletHell.States;
    using global::BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class BulletHell : Game
    {
        public static Random Random;
        public static int ScreenWidth = 800;
        public static int ScreenHeight = 480;

        private SpriteBatch spriteBatch;
        private State currentState;
        private State nextState;

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

        public void ChangeState(State state)
        {
            this.nextState = state;
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.currentState = new MenuState(this, Graphics.GraphicsDevice, this.Content);
                this.nextState = null;
                this.currentState.LoadContent();
            }

            if (this.nextState != null)
            {
                this.currentState = this.nextState;
                this.nextState = null;
                this.currentState.LoadContent();
            }

            this.currentState.Update(gameTime);
            this.currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        // This is called when the game should draw itself
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.currentState.Draw(gameTime, this.spriteBatch);

            base.Draw(gameTime);
        }

        // Set any values that weren't set in the constructor for BulletHell
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            // TODO: Add your initialization logic here
            base.Initialize();
        }

        // Load in content (sprites, assets, etc.)
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.currentState = new MenuState(this, Graphics.GraphicsDevice, this.Content);
            this.currentState.LoadContent();
        }
    }
}
