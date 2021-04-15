namespace BulletHell.Sprites.Attacks
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Attacks.Concrete_Attacks;
    using BulletHell.Sprites.Projectiles;

    internal class AttackFactory
    {
        public static Attack CreateAttack(Dictionary<string, object> attackProperties)
        {
            Attack attack = null;

            string attackName = (string)attackProperties["attackName"];
            Projectile projectile = ProjectileFactory.CreateProjectile((Dictionary<string, object>)attackProperties["projectile"]);

            switch (attackName)
            {
                case "homing":
                    attack = new Homing(projectile);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return attack;
        }
    }
}
