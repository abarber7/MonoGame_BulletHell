namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Controls;
    using BulletHell.States.Emitters;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Options : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private Texture2D optionsTexture;

        public Options()
          : base()
        {
            var buttonTexture = TextureFactory.GetTexture("Controls/Button");
            var buttonFont = TextureFactory.GetSpriteFont("Fonts/Font");
            this.optionsTexture = TextureFactory.GetTexture("Titles/options");

            var configureKeysButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 500),
                Text = "Configure Controls",
            };

            configureKeysButton.Click += this.ConfigureKeysButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 550),
                Text = "Main Menu",
            };

            returnButton.Click += this.ReturnButton_Click;

            this.components = new List<Component>()
            {
                configureKeysButton,
                returnButton,
            };
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GraphicManagers.GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();

            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.Draw(this.optionsTexture, new Vector2(75, 50), Color.Black);

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
        }

        public override void Draw(GameTime gameTime)
        {
        }

        private void ConfigureKeysButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new RebindKeys());
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new MenuState());
        }
    }
}
