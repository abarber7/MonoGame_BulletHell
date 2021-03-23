using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected BulletHell _game;

        #endregion

        public State(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void LoadContent();

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}
