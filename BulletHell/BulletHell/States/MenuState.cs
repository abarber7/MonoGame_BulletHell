using BulletHell.Controls;
using BulletHell.States.Emitters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private SnowEmitter _snowEmitter;
        private SpriteBatch spriteBatch;


        public object GraphicsDevice { get; private set; }

        public MenuState(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var difficultyButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Select Difficulty",
            };

            difficultyButton.Click += DifficultyButton_Click;


            var optionsButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Options",
            };

            optionsButton.Click += OptionsButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 350),
                Text = "Quit",
            };

            quitGameButton.Click += QuitGameButton_Click;


            _components = new List<Component>()
      {
        newGameButton,
        difficultyButton,
        optionsButton,
        quitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            _snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void DifficultyButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Select Difficulty");
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Options");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
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
            _game.Exit();
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
