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

        public override void Update(GameTime gameTime, List<Sprite> sprits)
        {
            this.Move();
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
            if (this.Movement.IsTouchingLeftOfScreen() || this.Movement.IsTouchingRightOfScreen() || this.Movement.IsTouchingBottomOfScreen() || this.Movement.IsTouchingTopOfScreen())
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
