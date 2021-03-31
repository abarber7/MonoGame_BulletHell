namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Controls;
    using global::BulletHell.States.Emitters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class MenuState : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private SpriteBatch spriteBatch;
        private Texture2D mainMenuTexture;

        public MenuState(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            this.mainMenuTexture = content.Load<Texture2D>("Titles/whiteMainMenu");

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
            this.game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(this.mainMenuTexture, new Vector2(150, 50), Color.White);

            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            this.snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
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
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

            this.snowEmitter = new SnowEmitter(new Emitters.SpriteLike(this.content.Load<Texture2D>("Particles/Snow")));
        }

        public override void Draw(GameTime gameTime)
        {
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new Options(this.game, this.graphicsDevice, this.content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new DifficultyState(this.game, this.graphicsDevice, this.content));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            this.game.Exit();
        }
    }
}
