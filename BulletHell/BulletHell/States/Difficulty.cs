using BulletHell.Controls;
using BulletHell.Game_Utilities;
using BulletHell.States.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.States
{
    public class DifficultyState : State
    {
        private List<Component> _components;
        private SnowEmitter _snowEmitter;
        private SpriteBatch spriteBatch;
        private Texture2D selectDifficultyTexture;


        public object GraphicsDevice { get; private set; }

        public DifficultyState(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            selectDifficultyTexture = _content.Load<Texture2D>("Titles/SelectDifficulty");

            var newGameEasyButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Easy",
            };

            newGameEasyButton.Click += NewGameEasyButton_Click;

            var newGameNormalButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Normal",
            };

            newGameNormalButton.Click += NewGameNormalButton_Click;

            var newGameHardButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Hard",
            };

            newGameHardButton.Click += NewGameHardButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Main Menu",
            };

            returnButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
      {
        newGameEasyButton,
        newGameNormalButton,
        newGameHardButton,
        returnButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin();
            spriteBatch.Draw(selectDifficultyTexture, new Vector2(40, 50), Color.Black);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            _snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void NewGameHardButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGameDictionary("Test2");

            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void NewGameNormalButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGameDictionary("Test");

            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void NewGameEasyButton_Click(object sender, EventArgs e)
        {
            GameLoader.LoadGameDictionary("Test");

            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);

            _snowEmitter.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            _snowEmitter = new SnowEmitter(new Emitters.SpriteLike(_content.Load<Texture2D>("Particles/Snow")));
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}
