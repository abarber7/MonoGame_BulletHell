namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Controls;
    using global::BulletHell.States.Emitters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class GameOverLose : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private SpriteBatch spriteBatch;
        private Texture2D gameOverTexture;

        public GameOverLose(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            this.gameOverTexture = content.Load<Texture2D>("Titles/GameOver");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Play Again",
            };

            newGameButton.Click += this.NewGameButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Main Menu",
            };

            returnButton.Click += this.ReturnButton_Click;

            var exitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Exit Game",
            };

            exitGameButton.Click += this.ExitGameButton_Click;

            this.components = new List<Component>()
            {
                newGameButton,
                returnButton,
                exitGameButton,
            };
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.game.GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();
            spriteBatch.Draw(this.gameOverTexture, new Vector2(90, 50), Color.Black);

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

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new DifficultyState(this.game, this.graphicsDevice, this.content));
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new MenuState(this.game, this.graphicsDevice, this.content));
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            this.game.Exit();
        }
    }
}
