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
    public class GameOverLose : State
    {
        private List<Component> _components;
        private SnowEmitter _snowEmitter;
        private SpriteBatch spriteBatch;


        public object GraphicsDevice { get; private set; }

        public GameOverLose(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");


            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Play Again",
            };

            newGameButton.Click += NewGameButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Main Menu",
            };

            returnButton.Click += ReturnButton_Click;

            var ExitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Exit Game",
            };

            ExitGameButton.Click += ExitGameButton_Click;



            _components = new List<Component>()
      {
        newGameButton,
        returnButton,
        ExitGameButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.Red);

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            _snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
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

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new DifficultyState(_game, _graphicsDevice, _content));
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
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
