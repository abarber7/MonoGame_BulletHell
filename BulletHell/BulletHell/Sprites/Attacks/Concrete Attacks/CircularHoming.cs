namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.States;
    using Microsoft.Xna.Framework;

    internal class CircularHoming : Attack
    {
        public new Circular Movement;

        public CircularHoming(Projectile projectile, Circular circularMovement, double cooldownToCreateProjectile)
            : base(projectile, circularMovement, cooldownToCreateProjectile)
        {
            this.Movement = circularMovement;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer > this.cooldownToCreateProjectile)
            {
                this.timer = 0;
                this.CreateProjectile(sprites);
            }

            this.Move();
        }

        protected override void CreateProjectile(List<Sprite> sprites)
        {
            Projectile newProjectile = this.projectile.Clone() as Projectile;
            newProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;

            newProjectile.Movement.Position = this.Movement.ActualPosition;
            newProjectile.Parent = this;
            sprites.Add(newProjectile);
        }

        private void Move()
        {
            this.Movement.Move();
        }
    }
}
