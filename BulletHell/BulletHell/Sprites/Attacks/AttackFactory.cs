namespace BulletHell.Sprites.Attacks
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
    using BulletHell.Sprites.Attacks.Concrete_Attacks;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Waves;

    internal class AttackFactory
    {
        public static List<Attack> CreateAttacks(List<object> listOfAttackProperties)
        {
            List<Attack> attacks = new List<Attack>();

            foreach (Dictionary<string, object> attackProperties in listOfAttackProperties)
            {
                Attack attack = CreateAttack(attackProperties);

                attacks.Add(attack);
            }

            return attacks;
        }

        public static List<Attack> CreateAttacks(Dictionary<string, object> attackProperties)
        {
            List<Attack> attacks = new List<Attack>();

            Attack attack = CreateAttack(attackProperties);

            attacks.Add(attack);

            return attacks;
        }

        private static Attack CreateAttack(Dictionary<string, object> attackProperties)
        {
            Attack attack = null;

            string attackName = (string)attackProperties["attackName"];
            Projectile projectile = ProjectileFactory.CreateProjectile((Dictionary<string, object>)attackProperties["projectile"]);
            MovementPattern movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)attackProperties["movementPattern"]);

            Timer cooldownToAttack = EntityGroupBuilder.CreateTimer((float)attackProperties["cooldownToAttack"]);
            cooldownToAttack.Stop();
            cooldownToAttack.AutoReset = true;

            Timer cooldownToCreateProjectile = EntityGroupBuilder.CreateTimer((float)attackProperties["cooldownToCreateProjectile"]);
            cooldownToCreateProjectile.Stop();
            cooldownToCreateProjectile.AutoReset = true;

            switch (attackName)
            {
                case "basicLinear":
                    attack = new BasicLinear(projectile, movement, cooldownToAttack, cooldownToCreateProjectile);
                    break;
                case "circularHoming":
                    attack = new CircularHoming(projectile, (Circular)movement, cooldownToAttack, cooldownToCreateProjectile);
                    break;
                case "circularTriHoming":
                    attack = new CircularTriHoming(projectile, (Circular)movement, cooldownToAttack, cooldownToCreateProjectile);
                    break;
                case "circle":
                    int numberOfProjectiles = Convert.ToInt32((float)attackProperties["numberOfProjectiles"]);
                    float degreesToStart = (float)attackProperties["degreesToStart"];
                    float degreesToEnd = (float)attackProperties["degreesToEnd"];
                    attack = new Circle(projectile, movement, cooldownToAttack, cooldownToCreateProjectile, numberOfProjectiles, degreesToStart, degreesToEnd);
                    break;
                case "arrow":
                    int widthOfArrow = Convert.ToInt32((float)attackProperties["widthOfArrow"]);
                    attack = new Arrow(projectile, movement, cooldownToAttack, cooldownToCreateProjectile, widthOfArrow);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return attack;
        }
    }
}
