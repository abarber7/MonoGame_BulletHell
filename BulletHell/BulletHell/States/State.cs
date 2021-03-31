﻿namespace BulletHell.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class State
    {
        public State()
        {
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void LoadContent();

        public abstract void PostUpdate(GameTime gameTime);

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}
