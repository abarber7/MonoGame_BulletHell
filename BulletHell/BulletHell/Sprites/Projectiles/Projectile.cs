namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal abstract class Projectile : Sprite, ICloneable
    {
        public Projectile(Dictionary<string, object> projectileProperties)
            : base(projectileProperties)
        {
        }

        public Sprite Parent { get; set; }

        public override void Update(GameTime gameTime, List<Sprite> sprits)
        {
            this.Move();
        }

        public void Move()
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
