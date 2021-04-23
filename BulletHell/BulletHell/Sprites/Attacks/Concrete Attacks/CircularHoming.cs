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

        public CircularHoming(Projectile projectile, Circular circularMovement, float cooldownToCreateProjectile)
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
            Projectile newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            newProjectile.Movement = this.ProjectileToLaunch.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;
            newProjectile.Movement.CurrentPosition = this.Movement.GetActualPosition();

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Parent = this.Attacker;
            sprites.Add(newProjectile);

            newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            newProjectile.Movement = this.ProjectileToLaunch.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;

            targetPosition = GameState.GetPlayerPosition();
            newProjectile.Movement.CurrentPosition = this.Movement.GetActualPosition(180);
            velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Parent = this.Attacker;
            sprites.Add(newProjectile);
        }

        private void Move()
        {
            this.Movement.Move();
        }
    }
}
