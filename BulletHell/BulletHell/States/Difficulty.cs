namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Controls;
    using global::BulletHell.Game_Utilities;
    using global::BulletHell.States.Emitters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class DifficultyState : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private SpriteBatch spriteBatch;
        private Texture2D selectDifficultyTexture;

        public DifficultyState(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            this.selectDifficultyTexture = content.Load<Texture2D>("Titles/SelectDifficulty");

            var newGameEasyButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Easy",
            };

            newGameEasyButton.Click += this.NewGameEasyButton_Click;

            var newGameNormalButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Normal",
            };

            newGameNormalButton.Click += this.NewGameNormalButton_Click;

            var newGameHardButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Hard",
            };

            newGameHardButton.Click += this.NewGameHardButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Main Menu",
            };

            returnButton.Click += this.QuitGameButton_Click;

            this.components = new List<Component>()
              {
                newGameEasyButton,
                newGameNormalButton,
                newGameHardButton,
                returnButton,
              };
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.game.GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();
            spriteBatch.Draw(this.selectDifficultyTexture, new Vector2(40, 50), Color.Black);

            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            this.snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
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

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        private void NewGameHardButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGameDictionary("demo");

            this.game.ChangeState(new GameState(this.game, this.graphicsDevice, this.content));
        }

        private void NewGameNormalButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGameDictionary("demo");

            this.game.ChangeState(new GameState(this.game, this.graphicsDevice, this.content));
        }

        private void NewGameEasyButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGameDictionary("demo");

            this.game.ChangeState(new GameState(this.game, this.graphicsDevice, this.content));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            this.game.ChangeState(new MenuState(this.game, this.graphicsDevice, this.content));
        }
    }
}
