namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    internal class Circle : Attack
    {
        private int numberOfProjectiles;
        private double degreesToStart;
        private double degreesToEnd;

        public Circle(Projectile projectile, MovementPattern movement, double cooldownToCreateProjectile, int numberOfProjectiles, double degreesToStart, double degreesToEnd)
            : base(projectile, movement, cooldownToCreateProjectile)
        {
            this.numberOfProjectiles = numberOfProjectiles;
            this.degreesToStart = degreesToStart;
            this.degreesToEnd = degreesToEnd;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            this.CreateProjectile(sprites);
            this.IsRemoved = true;
        }

        protected override void CreateProjectile(List<Sprite> sprites)
        {
            double degreesToIncrement = (this.degreesToEnd - this.degreesToStart) / this.numberOfProjectiles;

            for (int i = 0; i < this.numberOfProjectiles; i++)
            {
                Projectile newProjectile = this.projectile.Clone() as Projectile;
                int projectileSpeed = newProjectile.Movement.Speed;
                newProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
                newProjectile.Movement.Parent = newProjectile;
                Vector2 velocity = newProjectile.Movement.Velocity;
                velocity.X = Convert.ToSingle(projectileSpeed * Math.Cos((this.degreesToStart + (degreesToIncrement * i)) * (Math.PI / 180)));
                velocity.Y = Convert.ToSingle(projectileSpeed * Math.Sin((this.degreesToStart + (degreesToIncrement * i)) * (Math.PI / 180)));
                newProjectile.Movement.Velocity = velocity;
                newProjectile.Movement.Position = this.Movement.Position;
                newProjectile.Parent = this.Attacker;
                sprites.Add(newProjectile);
            }
        }
    }
}
