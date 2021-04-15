namespace BulletHell.Sprites
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Projectiles;

    internal abstract class Attack
    {
        public Projectile projectile;

        protected Attack(Projectile projectile)
        {
            this.projectile = projectile;
        }

        public virtual void DoAttack(List<Sprite> sprites, Sprite parent)
        {

        }
    }
}
