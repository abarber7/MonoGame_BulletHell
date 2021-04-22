namespace BulletHell.Sprites.Attacks
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Attacks.Concrete_Attacks;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using BulletHell.Sprites.Projectiles;

    internal class AttackFactory
    {
        public static Attack CreateAttack(Dictionary<string, object> attackProperties)
        {
            Attack attack = null;

            string attackName = (string)attackProperties["attackName"];
            Projectile projectile = ProjectileFactory.CreateProjectile((Dictionary<string, object>)attackProperties["projectile"]);
            MovementPattern movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)attackProperties["movementPattern"]);
            double projectileSpawnCooldown = (double)attackProperties["projectileSpawnCooldown"];

            switch (attackName)
            {
                case "basicLinear":
                    attack = new BasicLinear(projectile, movement, projectileSpawnCooldown);
                    break;
                case "circularHoming":
                    attack = new CircularHoming(projectile, (Circular)movement, projectileSpawnCooldown);
                    break;
                case "circularTriHoming":
                    attack = new CircularTriHoming(projectile, (Circular)movement, projectileSpawnCooldown);
                    break;
                case "circle":
                    int numberOfProjectiles = Convert.ToInt32((double)attackProperties["numberOfProjectiles"]);
                    double degreesToStart = (double)attackProperties["degreesToStart"];
                    double degreesToEnd = (double)attackProperties["degreesToEnd"];
                    attack = new Circle(projectile, movement, projectileSpawnCooldown, numberOfProjectiles, degreesToStart, degreesToEnd);
                    break;
                case "arrow":
                    int widthOfArrow = Convert.ToInt32((double)attackProperties["widthOfArrow"]);
                    attack = new Arrow(projectile, movement, projectileSpawnCooldown, widthOfArrow);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return attack;
        }
    }
}
