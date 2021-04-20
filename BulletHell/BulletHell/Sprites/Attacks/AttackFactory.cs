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

            switch (attackName)
            {
                case "basicLinear":
                    attack = new BasicLinear(projectile, (Static)movement);
                    break;
                case "circularHoming":
                    attack = new CircularHoming(projectile, (Circular)movement);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return attack;
        }
    }
}
