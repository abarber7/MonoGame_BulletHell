﻿namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Projectile : Sprite, ICloneable
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
                return new Rectangle(
                    new Point((int)this.Movement.Position.X - this.Texture.Width, (int)this.Movement.Position.Y - (int)Math.Round(this.Texture.Height * 2.5)),
                    new Point((int)Math.Round(this.Texture.Width * 2.5), (int)Math.Round(this.Texture.Height * 3.5)));
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
                // Allow projectiles to pass through friendlies
                if ((!sprite.GetType().IsSubclassOf(this.Parent.GetType()) || !this.Parent.GetType().IsSubclassOf(sprite.GetType()))
                    && sprite.GetType() != this.Parent.GetType())
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
