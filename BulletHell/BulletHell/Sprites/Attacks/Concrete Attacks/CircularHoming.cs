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
        private Circular movement;

        public override MovementPattern Movement { get => this.movement; set => this.movement = (Circular)value; }

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

            if (this.movement.IsMovementDone())
            {
                this.IsRemoved = true;
            }
        }

        protected override void CreateProjectile(List<Sprite> sprites)
        {
            Projectile newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            newProjectile.Movement.Parent = newProjectile;
            newProjectile.Movement.CurrentPosition = this.movement.GetActualPosition();

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Parent = this.Attacker;
            sprites.Add(newProjectile);

            newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            newProjectile.Movement.Parent = newProjectile;

            targetPosition = GameState.GetPlayerPosition();
            newProjectile.Movement.CurrentPosition = this.movement.GetActualPosition(180);
            velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Parent = this.Attacker;
            sprites.Add(newProjectile);
        }

        private void Move()
        {
            this.Movement.Move();
        }

        public override object Clone()
        {
            CircularHoming newAttack = (CircularHoming)this.MemberwiseClone();
            if (this.Movement != null)
            {
                Circular newMovement = (Circular)this.Movement.Clone();
                newAttack.Movement = newMovement;
            }

            Projectile newProjectile = (Projectile)this.ProjectileToLaunch.Clone();
            newAttack.ProjectileToLaunch = newProjectile;
            if (this.Attacker != null)
            {
                Sprite newAttacker = (Sprite)this.Attacker.Clone();
                newAttack.Attacker = newAttacker;
            }

            return newAttack;
        }
    }
}
