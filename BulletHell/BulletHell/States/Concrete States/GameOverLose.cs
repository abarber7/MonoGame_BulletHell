namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Controls;
    using BulletHell.States.Emitters;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public class GameOverLose : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private Texture2D gameOverTexture;
        private Texture2D gameOverTexture2;
        private Song song;

        public GameOverLose()
          : base()
        {
            var buttonTexture = TextureFactory.GetTexture("Controls/Button");
            var buttonFont = TextureFactory.GetSpriteFont("Fonts/Font");
            this.gameOverTexture = TextureFactory.GetTexture("Titles/GAME");
            this.gameOverTexture2 = TextureFactory.GetTexture("Titles/OVER");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 500),
                Text = "Play Again",
            };

            newGameButton.Click += this.NewGameButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 550),
                Text = "Main Menu",
            };

            returnButton.Click += this.ReturnButton_Click;

            var exitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 600),
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
            GraphicManagers.GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();
            spriteBatch.Draw(this.gameOverTexture, new Vector2(90, 50), Color.Black);
            spriteBatch.Draw(this.gameOverTexture2, new Vector2(90, 140), Color.Black);

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

            this.snowEmitter = new SnowEmitter(new SpriteLike(TextureFactory.GetTexture("Particles/Snow")));
            this.song = TextureFactory.Content.Load<Song>("Songs/panda");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(this.song);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new DifficultyState());
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new MenuState());
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            StateManager.ExitEvent(null, e);
        }
    }
}
