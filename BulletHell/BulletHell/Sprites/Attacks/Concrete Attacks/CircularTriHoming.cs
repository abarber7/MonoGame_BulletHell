namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.States;
    using Microsoft.Xna.Framework;

    internal class CircularTriHoming : Attack
    {
        public new Circular Movement;

        public CircularTriHoming(Projectile projectile, Circular circularMovement, double cooldownToCreateProjectile)
            : base(projectile, circularMovement, cooldownToCreateProjectile)
        {
            this.Movement = circularMovement;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer > this.projectileSpawnCooldown)
            {
                this.timer = 0;
                this.CreateProjectile(sprites);
            }

            this.Move();

            if (this.Movement.IsMovementDone())
            {
                this.IsRemoved = true;
            }
        }

        protected override void CreateProjectile(List<Sprite> sprites)
        {
            Projectile newProjectile = this.projectile.Clone() as Projectile;
            newProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = this.Movement.CalculateVelocity(this.Movement.Position, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.Position = this.Movement.GetActualPosition();
            newProjectile.Parent = this.Attacker;
            sprites.Add(newProjectile);

            Projectile secondProjectile = this.projectile.Clone() as Projectile;
            secondProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
            secondProjectile.Movement.Parent = secondProjectile;

            Vector2 secondVelocity = default(Vector2);
            secondVelocity.X = (float)((velocity.X * Math.Cos(120 * (Math.PI / 180))) - (velocity.Y * Math.Sin(120 * (Math.PI / 180))));
            secondVelocity.Y = (float)((velocity.X * Math.Sin(120 * (Math.PI / 180))) + (velocity.Y * Math.Cos(120 * (Math.PI / 180))));

            secondProjectile.Movement.Velocity = secondVelocity;
            secondProjectile.Movement.Position = this.Movement.GetActualPosition();
            secondProjectile.Parent = this.Attacker;
            sprites.Add(secondProjectile);

            Projectile thirdProjectile = this.projectile.Clone() as Projectile;
            thirdProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
            thirdProjectile.Movement.Parent = thirdProjectile;

            Vector2 thirdVelocity = default(Vector2);
            thirdVelocity.X = (float)((velocity.X * Math.Cos(240 * (Math.PI / 180))) - (velocity.Y * Math.Sin(240 * (Math.PI / 180))));
            thirdVelocity.Y = (float)((velocity.X * Math.Sin(240 * (Math.PI / 180))) + (velocity.Y * Math.Cos(240 * (Math.PI / 180))));

            thirdProjectile.Movement.Velocity = thirdVelocity;
            thirdProjectile.Movement.Position = this.Movement.GetActualPosition();
            thirdProjectile.Parent = this.Attacker;
            sprites.Add(thirdProjectile);
        }

        private void Move()
        {
            this.Movement.Move();
        }
    }
}
