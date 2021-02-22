namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using System.Collections.Generic;

    internal class BounceBullet : Projectile
    {
        private int bounceCount = 0;
        private int numberOfTimesToBounce;

        public BounceBullet(Dictionary<string, object> bulletProperties)
            : base(bulletProperties)
        {
            this.numberOfTimesToBounce = (int)bulletProperties["bounceTimes"];
        }

        public override void Move()
        {
            if (this.OutOfBounds())
            {
                if (this.bounceCount == this.numberOfTimesToBounce)
                {
                    this.IsRemoved = true;
                }

                this.bounceCount++;
            }

            this.Movement.Move();
        }
    }
}
