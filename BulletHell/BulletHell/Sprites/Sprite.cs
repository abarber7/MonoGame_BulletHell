namespace BulletHell.Sprites
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Sprite
    {
        private readonly Color[] textureData;
        private bool isRemoved = false;
        private Color color = Color.White;

        public Sprite(Texture2D texture, Color color, MovementPattern movement)
        {
            this.Texture = texture;
            this.Color = color;
            this.Movement = movement;
            this.textureData = new Color[texture.Width * texture.Height]; // array size is pixel amount in the texture
            this.Texture.GetData(this.textureData);
        }

        public Texture2D Texture { get; set; }

        public MovementPattern Movement { get; set; }

        public Color Color
        {
            get => this.color;
            set => this.color = value;
        }

        public bool IsRemoved
        {
            get => this.isRemoved;
            set => this.isRemoved = value;
        }

        // Serves as hitbox
        public virtual Rectangle Rectangle
        {
            get => new Rectangle(
                    new Point((int)this.Movement.Position.X, (int)this.Movement.Position.Y),
                    new Point(this.Texture.Width, this.Texture.Height));
        }

        public virtual void Update(GameTime gametime, List<Sprite> sprites)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Movement.Position, null, this.Color, this.Movement.Rotation, this.Movement.Origin, 1, SpriteEffects.None, 0);
        }

        public virtual void OnCollision(Sprite sprite)
        {
        }
    }
}