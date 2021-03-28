namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Projectile : Sprite, ICloneable
    {
        public Projectile(Texture2D texture, Color color, MovementPattern movement)
            : base(texture, color, movement)
        {
        }

        public Sprite Parent { get; set; }

        // Serves as hitbox (larger than usual to account for speed vs framerate)
        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)this.Movement.Position.X - (this.Texture.Width / 2),
                    (int)this.Movement.Position.Y - (this.Texture.Height / 2),
                    (int)Math.Round(this.Texture.Width * 1.5),
                    (int)Math.Round(this.Texture.Height * 3.5));
            }
        }

        // A translation matrix for collision
        // Source: https://github.com/Oyyou/MonoGame_Tutorials/blob/master/MonoGame_Tutorials/Tutorial019/Sprites/Sprite.cs
        public override Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-new Vector2(this.Rectangle.Width / 2, this.Rectangle.Height / 2), 0)) * // Rectangle width and height standing in for origin
                  Matrix.CreateTranslation(new Vector3(new Vector2(this.Movement.Position.X, this.Movement.Position.Y), 0)); // Position represented by movement pattern position
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprits)
        {
            this.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            // Ignore collision if sprite is one who fired or if sprite is another projectile (for now)
            if (sprite != this.Parent && !(sprite is Projectile))
            {
                this.IsRemoved = true;
            }
        }

        public virtual void Move()
        {
            if (this.OutOfBounds())
            {
                this.IsRemoved = true;
            }

            this.Movement.Move();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool OutOfBounds()
        {
            if (this.Movement.IsTouchingLeftOfScreen() ||
                this.Movement.IsTouchingRightOfScreen() ||
                this.Movement.IsTouchingBottomOfScreen() ||
                this.Movement.IsTouchingTopOfScreen())
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
