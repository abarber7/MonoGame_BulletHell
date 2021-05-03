namespace BulletHell.Sprites
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Sprite : ICloneable
    {
        protected bool isRemoved = false;
        protected float textureScale = 1;
        protected Color originalColor;
        private Color color = Color.White;
        private MovementPattern movement;

        public Sprite(Texture2D texture, Color color, MovementPattern movement)
        {
            this.Texture = texture;
            this.Color = color;
            this.originalColor = color;
            this.Movement = movement;
        }

        public virtual MovementPattern Movement { get => this.movement; set => this.movement = value; }

        public Texture2D Texture { get; set; }

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
            get
            {
                return new Rectangle(
                    this.UpperLeftCorner.ToPoint(),
                    new Point(this.TextureWidth, this.TextureHeight));
            }
        }

        public Vector2 UpperLeftCorner
        {
            get
            {
                Vector2 upperLeftCorner = this.Movement.CurrentPosition;
                upperLeftCorner.X -= this.TextureWidth / 2;
                upperLeftCorner.Y -= this.TextureHeight / 2;
                return upperLeftCorner;
            }
        }

        public int TextureWidth { get => Convert.ToInt32(this.Texture.Width * this.textureScale); }

        public int TextureHeight { get => Convert.ToInt32(this.Texture.Height * this.textureScale); }

        public virtual object Clone()
        {
            Sprite newSprite = (Sprite)this.MemberwiseClone();
            if (this.Movement != null)
            {
                MovementPattern newMovement = (MovementPattern)this.Movement.Clone();
                newSprite.Movement = newMovement;
            }

            return newSprite;
        }

        public virtual void Update(GameTime gametime, List<Sprite> sprites)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.Texture != null)
            {
                spriteBatch.Draw(this.Texture, this.UpperLeftCorner, null, this.Color, this.Movement.Rotation, this.Movement.Origin, this.textureScale, SpriteEffects.None, 0);
            }
        }

        public virtual void OnCollision(Sprite sprite)
        {
        }

        public void CheckForCollision(List<Sprite> sprites)
        {
            for (int i = sprites.Count - 1; i >= 0; i--)
            {
                // Check for hitbox collision
                if (this.Rectangle.Intersects(sprites[i].Rectangle))
                {
                    this.OnCollision(sprites[i]);
                    sprites[i].OnCollision(this);
                }
            }
        }

        public Vector2 GetCenterOfSprite()
        {
            return this.Movement.CurrentPosition;
        }
    }
}