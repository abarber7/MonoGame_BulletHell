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
        private Color color = Color.White;
        private MovementPattern movement;

        public virtual MovementPattern Movement { get => this.movement; set => this.movement = value; }

        public Sprite(Texture2D texture, Color color, MovementPattern movement)
        {
            this.Texture = texture;
            this.Color = color;
            this.Movement = movement;
        }

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
                Vector2 upperLeftCorner = this.Movement.CurrentPosition;
                upperLeftCorner.X -= this.Texture.Width / 2;
                upperLeftCorner.Y -= this.Texture.Height / 2;
                return new Rectangle(
                    upperLeftCorner.ToPoint(),
                    new Point(this.Texture.Width, this.Texture.Height));
            }
        }

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
                Vector2 upperLeftCorner = this.Movement.CurrentPosition;
                upperLeftCorner.X -= this.Texture.Width / 2;
                upperLeftCorner.Y -= this.Texture.Height / 2;
                spriteBatch.Draw(this.Texture, upperLeftCorner, null, this.Color, this.Movement.Rotation, this.Movement.Origin, 1, SpriteEffects.None, 0);
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