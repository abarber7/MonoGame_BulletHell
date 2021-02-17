using BulletHell.Sprites.Projectiles.Concrete_Projectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Projectiles
{
    class ProjectileFactory
    {
        public static Projectile createProjectile(Dictionary<string, object> projectileProperties)
        {
            Projectile entity = null;
            switch (projectileProperties["projectileType"])
            {
                case "bullet":
                    entity = new Bullet(projectileProperties);
                    break;
                default:
                    throw new Exception("Invalid Projectile Type");
            }
            return entity;
        }
    }
}
