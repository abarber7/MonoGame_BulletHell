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
    public class RebindKeys : State
    {
        private List<Component> _components;
        private SnowEmitter _snowEmitter;
        private SpriteBatch spriteBatch;


        public object GraphicsDevice { get; private set; }

        public RebindKeys(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");


            var upButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(150, 150),
                Text = "Up",
            };

            upButton.Click += UpButton_Click;

            var downButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(150, 250),
                Text = "Down",
            };

            downButton.Click += DownButton_Click;

            var leftButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(50, 200),
                Text = "Left",
            };

            leftButton.Click += LeftButton_Click;

            var rightButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 200),
                Text = "Right",
            };

            rightButton.Click += RightButton_Click;


            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Return",
            };

            returnButton.Click += ReturnButton_Click;

            var fireButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 200),
                Text = "Fire",
            };

            fireButton.Click += FireButton_Click;


            _components = new List<Component>()
      {
        upButton,
        downButton,
        leftButton,
        rightButton,
        fireButton,
        returnButton,
      };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _game.GraphicsDevice.Clear(Color.DarkSalmon);

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

        private void UpButton_Click(object sender, EventArgs e)
        {
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
        }

        private void FireButton_Click(object sender, EventArgs e)
        {
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new Options(_game, _graphicsDevice, _content));
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
