namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Projectiles.Concrete_Projectiles;

    internal class ProjectileFactory
    {
        public static Projectile CreateProjectile(Dictionary<string, object> projectileProperties)
        {
            return projectileProperties["projectileType"] switch
            {
                "bullet" => new Bullet(projectileProperties),
                "bouncingBullet" => new BouncingBullet(projectileProperties),
                "bounceBullet" => new BounceBullet(projectileProperties),
                _ => throw new Exception("Invalid Projectile Type"),
            };
        }
    }
}
