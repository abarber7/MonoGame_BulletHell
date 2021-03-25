namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Projectiles.Concrete_Projectiles;

    internal class ProjectileFactory
    {
        public static Projectile CreateProjectile(Dictionary<string, object> projectileProperties)
        {
            Projectile projectile = null;
            switch (projectileProperties["projectileType"])
            {
                case "bullet":
                    projectile = new Bullet(projectileProperties);
                    break;
                case "bouncingBullet":
                    projectile = new BouncingBullet(projectileProperties);
                    break;
                case "bounceBullet":
                    projectile = new BounceBullet(projectileProperties);
                    break;
                default:
                    throw new Exception("Invalid Projectile Type");
            }

            return projectile;
        }
    }
}
