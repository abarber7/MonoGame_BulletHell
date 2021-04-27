namespace BulletHell.Sprites.Attacks
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
    using BulletHell.Sprites.Attacks.Concrete_Attacks;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using BulletHell.Sprites.Projectiles;

    internal class AttackFactory
    {
        public static List<Attack> CreateAttacks(List<Dictionary<string, object>> listOfAttackProperties)
        {
            List<Attack> attacks = new List<Attack>();

            foreach (Dictionary<string, object> attackProperties in listOfAttackProperties)
            {
                Attack attack = null;

                string attackName = (string)attackProperties["attackName"];
                Projectile projectile = ProjectileFactory.CreateProjectile((Dictionary<string, object>)attackProperties["projectile"]);
                MovementPattern movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)attackProperties["movementPattern"]);
                Timer projectileSpawnCooldown = new Timer((float)attackProperties["projectileSpawnCooldown"]);

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
                        int numberOfProjectiles = Convert.ToInt32((float)attackProperties["numberOfProjectiles"]);
                        float degreesToStart = (float)attackProperties["degreesToStart"];
                        float degreesToEnd = (float)attackProperties["degreesToEnd"];
                        attack = new Circle(projectile, movement, projectileSpawnCooldown, numberOfProjectiles, degreesToStart, degreesToEnd);
                        break;
                    case "arrow":
                        int widthOfArrow = Convert.ToInt32((float)attackProperties["widthOfArrow"]);
                        attack = new Arrow(projectile, movement, projectileSpawnCooldown, widthOfArrow);
                        break;
                    default:
                        throw new Exception("Invalid Entity");
                }

                attacks.Add(attack);
            }

            return attacks;
        }

        public static Attack DownCastAttack(object attack)
        {
            switch (attack)
            {
                case BasicLinear _:
                    return attack as BasicLinear;
                case CircularHoming _:
                    return attack as CircularHoming;
                case CircularTriHoming _:
                    return attack as CircularTriHoming;
                case Circle _:
                    return attack as Circle;
                case Arrow _:
                    return attack as Arrow;
                default:
                    return null;
            }
        }
    }
}
