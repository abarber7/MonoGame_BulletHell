namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Projectile : Sprite
    {
        public Projectile(Texture2D texture, Color color, MovementPattern movement, int damage)
            : base(texture, color, movement)
        {
            this.Damage = damage;
        }

        public Sprite Parent { get; set; }

        public int Damage { get; set; }

        // Serves as hitbox (extended lengthwise to account for bullet speed vs framerate)
        public override Rectangle Rectangle
        {
            get
            {
                Vector2 boxUpperLeftCorner = this.Movement.CurrentPosition;
                boxUpperLeftCorner.X -= this.Texture.Width + (this.Texture.Width / 4);
                boxUpperLeftCorner.Y -= (float)(this.Texture.Height + (this.Texture.Height * (3.5 / 2)));

                return new Rectangle(
                    boxUpperLeftCorner.ToPoint(),
                    new Point(this.Texture.Width * 2, (int)Math.Round(this.Texture.Height * 3.5)));
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            // Ignore collision if sprite is one who fired or if sprite is another projectile (for now)
            if (sprite != this.Parent && !(sprite is Projectile))
            {
                // Hit case 1: sprite is Player, and this projectile is from an Enemy
                // Hit case 2: sprite is an Enemy, and this projectile is from Player
                if ((sprite is Player && this.Parent is Enemy) ||
                    (sprite is Enemy && this.Parent is Player))
                {
                    this.IsRemoved = true;
                }
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

        public override object Clone()
        {
            Projectile newProjectile = this.MemberwiseClone() as Projectile;
            if (this.Movement != null)
            {
                MovementPattern newMovement = (MovementPattern)this.Movement.Clone();
                newProjectile.Movement = newMovement;
            }

            return newProjectile;
        }
    }
}
