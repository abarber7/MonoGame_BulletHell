namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class BouncingBullet : Projectile
    {
        // max bounce number
        private int maxBounces = 5;

        public BouncingBullet(Dictionary<string, object> bouncingBulletProperties)
            : base(bouncingBulletProperties)
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
                    this.Movement.velocity = -this.Movement.velocity;
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

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
        }
    }
}