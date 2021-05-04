namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Controls;
    using BulletHell.Game_Utilities;
    using BulletHell.States.Emitters;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class DifficultyState : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private Texture2D selectDifficultyTexture;
        private Texture2D selectDifficultyTexture2;

        public DifficultyState()
          : base()
        {
            var buttonTexture = TextureFactory.GetTexture("Controls/Button");
            var buttonFont = TextureFactory.GetSpriteFont("Fonts/Font");
            this.selectDifficultyTexture = TextureFactory.GetTexture("Titles/SELECT");
            this.selectDifficultyTexture2 = TextureFactory.GetTexture("Titles/DIFFICULTY");

            var bossButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(2, 300),
                Text = "Boss",
            };

            bossButton.Click += this.BossButton_Click;

            var newGameEasyButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 450),
                Text = "Easy",
            };

            newGameEasyButton.Click += this.NewGameEasyButton_Click;

            var newGameNormalButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 500),
                Text = "Normal",
            };

            newGameNormalButton.Click += this.NewGameNormalButton_Click;

            var newGameHardButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 550),
                Text = "Hard",
            };

            newGameHardButton.Click += this.NewGameHardButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 600),
                Text = "Main Menu",
            };

            returnButton.Click += this.QuitGameButton_Click;

            this.components = new List<Component>()
              {
                newGameEasyButton,
                newGameNormalButton,
                newGameHardButton,
                returnButton,
                bossButton,
              };
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GraphicManagers.GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();
            spriteBatch.Draw(this.selectDifficultyTexture, new Vector2(50, 50), Color.Black);
            spriteBatch.Draw(this.selectDifficultyTexture2, new Vector2(30, 120), Color.Black);

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
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            this.snowEmitter = new SnowEmitter(new SpriteLike(TextureFactory.Content.Load<Texture2D>("Particles/Snow")));
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
            GameLoader.LoadGame("hard");

            StateManager.ChangeState(new GameState());
        }

        private void BossButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGame("boss");

            StateManager.ChangeState(new GameState());
        }

        private void NewGameNormalButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGame("normal");

            StateManager.ChangeState(new GameState());
        }

        private void NewGameEasyButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGame("easy");

            StateManager.ChangeState(new GameState());
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new MenuState());
        }
    }
}
