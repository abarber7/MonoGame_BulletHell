namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class BouncingBullet : Projectile
    {
        // max bounce number
        private int maxBounces = 5;

        public BouncingBullet(Texture2D texture, Color color, MovementPattern movement, int damage)
            : base(texture, color, movement, damage)
        {
        }

        public override void Move()
        {
            // if you can still bounce
            if (this.maxBounces != 0)
            {
                // when bullet is out of bounds reverse its velocity and reduce maxBounces
                if (this.OutOfBounds())
                {
                    this.Movement.Velocity = -this.Movement.Velocity;
                    this.maxBounces--;
                }
            }

            // if you can no longer bounce remove the bullet when it is out of bounds
            else
            {
                if (this.OutOfBounds())
                {
                    this.IsRemoved = true;
                }
            }

            this.Movement.Move();
        }
    }
}