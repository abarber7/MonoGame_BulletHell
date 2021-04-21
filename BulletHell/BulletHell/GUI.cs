namespace BulletHell
{
    using System;
    using BulletHell.States;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GUI : Game
    {
        public static int ScreenWidth = 480;
        public static int ScreenHeight = 720;

        private SpriteBatch spriteBatch;

        // Initialize screensize and other game properties
        public GUI()
        {
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

            UtlilityManager.Initialize(this.Content, graphics);

            StateManager.ExitEvent += this.ExitGUI;
        }

        public void ExitGUI(object sender, EventArgs e)
        {
            this.Exit();
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            StateManager.Update(gameTime);

            base.Update(gameTime);
        }

        // This is called when the game should draw itself
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            StateManager.CurrentState.Draw(gameTime, this.spriteBatch);

            base.Draw(gameTime);
        }

        // Set any values that weren't set in the constructor for BulletHell
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();

            // Set the size of the game (fullscreen bad)
            GraphicManagers.GraphicsDeviceManager.PreferredBackBufferWidth = ScreenWidth;
            GraphicManagers.GraphicsDeviceManager.PreferredBackBufferHeight = ScreenHeight;
            GraphicManagers.GraphicsDeviceManager.ApplyChanges();
        }

        // Load in content (sprites, assets, etc.)
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            StateManager.CurrentState = new MenuState();
            StateManager.CurrentState.LoadContent();
        }
    }
}
