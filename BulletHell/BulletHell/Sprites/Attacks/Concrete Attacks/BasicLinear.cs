namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Timers;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.States;
    using Microsoft.Xna.Framework;

    internal class BasicLinear : Attack
    {
        public BasicLinear(Projectile projectile, MovementPattern movement, Timer cooldownToAttack, Timer cooldownToCreateProjectile)
            : base(projectile, movement, cooldownToAttack, cooldownToCreateProjectile)
        {
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            if (this.NumberOfTimesToLaunchProjectiles <= this.NumberOfTimesProjectilesHaveLaunched || this.Attacker.IsRemoved)
            {
                this.IsRemoved = true;
                this.CooldownToAttack.Stop();
                Debug.WriteLineIf(this.Attacker is Enemy, DateTime.Now + ": " + this.Attacker.GetHashCode() + " is stopping the Cooldown to Attack Timer for " + this.GetHashCode());
                this.CooldownToCreateProjectile.Stop();
                Debug.WriteLineIf(this.Attacker is Enemy, DateTime.Now + ": " + this.Attacker.GetHashCode() + " is stopping the Cooldown to Create Projectile Timer for " + this.GetHashCode());
            }
        }

        public override void CreateProjectile(object source, ElapsedEventArgs args)
        {
            // this.ToggleTimer(source as Timer);

            Projectile newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            float projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement = this.ProjectileToLaunch.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;
            Vector2 velocity = newProjectile.Movement.Velocity;
            velocity.Normalize();
            velocity.X *= Convert.ToSingle(projectileSpeed);
            velocity.Y *= Convert.ToSingle(projectileSpeed);
            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.CurrentPosition = this.Attacker.Rectangle.Center.ToVector2(); // previously, in case of issues: this.Movement.CurrentPosition;
            newProjectile.Parent = this.Attacker;
            GameState.Projectiles.Add(newProjectile);

            this.NumberOfTimesProjectilesHaveLaunched++;

            // this.ToggleTimer(source as Timer);
        }
    }
}
