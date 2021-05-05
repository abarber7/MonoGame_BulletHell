namespace BulletHell.States.Concrete_States.GameState
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ScrollingBackground
    {
        public Texture2D Texture;
        public Rectangle Rectangle;

        public ScrollingBackground(Rectangle rectangle)
        {
            this.Texture = TextureFactory.GetTexture("background");
            this.Rectangle = rectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }

        public void Update()
        {
            this.Rectangle.Y += 1;
        }
    }
}
