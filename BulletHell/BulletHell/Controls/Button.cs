namespace BulletHell.Controls
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Button : Component
    {
        private MouseState currentMouse;
        private SpriteFont font;
        private bool isHovering;
        private MouseState previousMouse;
        private Texture2D texture;

        public Button(Texture2D texture, SpriteFont font)
        {
            this.texture = texture;

            this.font = font;

            this.PenColor = Color.Black;
        }

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get => new Rectangle((int)this.Position.X, (int)this.Position.Y, this.texture.Width, this.texture.Height);
        }

        public string Text { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (this.isHovering)
            {
                color = Color.Gray;
            }

            spriteBatch.Draw(this.texture, this.Rectangle, color);

            if (!string.IsNullOrEmpty(this.Text))
            {
                var x = (this.Rectangle.X + (this.Rectangle.Width / 2)) - (this.font.MeasureString(this.Text).X / 2);
                var y = (this.Rectangle.Y + (this.Rectangle.Height / 2)) - (this.font.MeasureString(this.Text).Y / 2);

                spriteBatch.DrawString(this.font, this.Text, new Vector2(x, y), this.PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.previousMouse = this.currentMouse;
            this.currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(this.currentMouse.X, this.currentMouse.Y, 1, 1);

            this.isHovering = false;

            if (mouseRectangle.Intersects(this.Rectangle))
            {
                this.isHovering = true;

                if (this.currentMouse.LeftButton == ButtonState.Released && this.previousMouse.LeftButton == ButtonState.Pressed)
                {
                    this.Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}