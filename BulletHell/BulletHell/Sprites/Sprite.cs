namespace BulletHell.Sprites
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Sprite
    {
        private bool isRemoved = false;
        private Color color = Color.White;

        public Sprite(Texture2D texture, Color color, MovementPattern movement)
        {
            this.Texture = texture;
            this.Color = color;
            this.Movement = movement;
        }

        public Texture2D Texture { get; set; }

        public MovementPattern Movement { get; set; }

        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        public bool IsRemoved
        {
            get
            {
                return this.isRemoved;
            }

            set
            {
                this.isRemoved = value;
            }
        }

        // Serves as hitbox
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)this.Movement.Position.X - (this.Texture.Width / 2), (int)this.Movement.Position.Y - (this.Texture.Height / 2), this.Texture.Width, this.Texture.Height);
            }
        }

        public virtual void Update(GameTime gametime, List<Sprite> sprits)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Movement.Position, null, this.Color, this.Movement.Rotation, this.Movement.Origin, 1, SpriteEffects.None, 0);
        }

        protected bool IsTouchingLeftSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Right + this.Movement.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRightSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Left + this.Movement.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTopSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Movement.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottomSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Top + this.Movement.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool HasCollidedWithASprite(Sprite sprite)
        {
            if ((this.Movement.Velocity.X > 0 && this.IsTouchingLeftSideOfSprite(sprite)) || (this.Movement.Velocity.X < 0 && this.IsTouchingRightSideOfSprite(sprite)) || (this.Movement.Velocity.Y > 0 && this.IsTouchingTopSideOfSprite(sprite)) || (this.Movement.Velocity.Y < 0 && this.IsTouchingBottomSideOfSprite(sprite)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}