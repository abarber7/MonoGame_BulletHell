namespace BulletHell.States.Emitters
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SpriteLike : Component, ICloneable
    {
        public Vector2 Position;
        public Vector2 Velocity;

        protected Texture2D texture;

        public SpriteLike(Texture2D texture)
        {
            this.texture = texture;

            this.Opacity = 1f;

            this.Origin = new Vector2(this.texture.Width / 2, this.texture.Height / 2);
        }

        public float Opacity { get; set; }

        public Vector2 Origin { get; set; }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)(this.Position.X - this.Origin.X), (int)(this.Position.Y - this.Origin.Y), (int)(this.texture.Width * this.Scale), (int)(this.texture.Height * this.Scale));
            }
        }

        public bool IsRemoved { get; set; }

        public override void Update(GameTime gameTime)
        {
            this.Position += this.Velocity;

            if (this.Rectangle.Top > GUI.ScreenHeight)
            {
                this.IsRemoved = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.Position, null, Color.HotPink * this.Opacity, this.Rotation, this.Origin, this.Scale, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
