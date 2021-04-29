namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System.Collections.Generic;
    using System.Timers;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.States;
    using Microsoft.Xna.Framework;

    internal class CircularHoming : Attack
    {
        private Circular movement;

        public override MovementPattern Movement { get => this.movement; set => this.movement = (Circular)value; }

        public CircularHoming(Projectile projectile, Circular circularMovement, Timer cooldownToAttack, Timer cooldownToCreateProjectile)
            : base(projectile, circularMovement, cooldownToAttack, cooldownToCreateProjectile)
        {
            this.Movement = circularMovement;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.Move();

            if (this.movement.IsMovementDone())
            {
                this.IsRemoved = true;
                // this.CooldownToAttack.Stop();
                this.CooldownToCreateProjectile.Stop();
            }
        }

        public override void CreateProjectile(object source, ElapsedEventArgs args)
        {
            // this.PauseTimersWhileDebugging(source as Timer);

            Projectile newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            newProjectile.Movement.Parent = newProjectile;
            newProjectile.Movement.CurrentPosition = this.movement.GetActualPosition();

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Parent = this.Attacker;
            GameState.Projectiles.Add(newProjectile);

            newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            newProjectile.Movement.Parent = newProjectile;

            targetPosition = GameState.GetPlayerPosition();
            newProjectile.Movement.CurrentPosition = this.movement.GetActualPosition(180);
            velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Parent = this.Attacker;
            GameState.Projectiles.Add(newProjectile);
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

            newAttack.Attacker = this.Attacker;

            newAttack.NumberOfTimesAttacksHaveExecuted = 0;

            newAttack.isClone = true;

            Timer newCooldownToAttackTimer = new Timer(newAttack.CooldownToAttack.Interval);
            newCooldownToAttackTimer.AutoReset = newAttack.CooldownToAttack.AutoReset;
            newCooldownToAttackTimer.Enabled = newAttack.CooldownToAttack.Enabled;
            newAttack.CooldownToAttack = newCooldownToAttackTimer;

            Timer newCooldownToCreateProjectile = new Timer(newAttack.CooldownToCreateProjectile.Interval);
            newCooldownToCreateProjectile.AutoReset = newAttack.CooldownToCreateProjectile.AutoReset;
            newCooldownToCreateProjectile.Enabled = newAttack.CooldownToCreateProjectile.Enabled;
            newAttack.CooldownToCreateProjectile = newCooldownToCreateProjectile;

            return newAttack;
        }
    }
}
