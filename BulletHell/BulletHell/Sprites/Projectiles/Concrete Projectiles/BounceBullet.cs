namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class BounceBullet : Projectile
    {
        private int bounceCount = 0;
        private int numberOfTimesToBounce;

        public BounceBullet(Texture2D texture, Color color, MovementPattern movement, int damage, int timesToBounce)
            : base(texture, color, movement, damage)
        {
            this.numberOfTimesToBounce = timesToBounce;
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
