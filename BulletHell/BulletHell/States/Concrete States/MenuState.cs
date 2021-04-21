namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Controls;
    using BulletHell.States.Emitters;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class MenuState : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private Texture2D mainMenuTexture;
        private Texture2D mainMenuTexture2;


        public MenuState()
          : base()
        {
            var buttonTexture = TextureFactory.GetTexture("Controls/Button");
            var buttonFont = TextureFactory.GetSpriteFont("Fonts/Font");
            this.mainMenuTexture = TextureFactory.GetTexture("Titles/Bo&Long");
            this.mainMenuTexture2 = TextureFactory.GetTexture("Titles/gotTo");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "New Game",
            };

            newGameButton.Click += this.NewGameButton_Click;

            var optionsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Options",
            };

            optionsButton.Click += this.OptionsButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit",
            };

            quitGameButton.Click += this.QuitGameButton_Click;

            this.components = new List<Component>()
            {
                newGameButton,
                optionsButton,
                quitGameButton,
            };
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GraphicManagers.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(this.mainMenuTexture, new Vector2(160, 20), Color.White);
            spriteBatch.Draw(this.mainMenuTexture2, new Vector2(60, 120), Color.White);


            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            this.snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in this.components)
            {
                component.Update(gameTime);
            }

            this.snowEmitter.Update(gameTime);
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            this.snowEmitter = new SnowEmitter(new SpriteLike(TextureFactory.Content.Load<Texture2D>("Particles/Snow")));
        }

        public override void Draw(GameTime gameTime)
        {
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new Options());
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new DifficultyState());
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            StateManager.ExitEvent(null, e);
        }
    }
}
