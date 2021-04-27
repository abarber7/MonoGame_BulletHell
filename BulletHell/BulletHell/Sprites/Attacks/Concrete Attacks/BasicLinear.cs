namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
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
            this.IsRemoved = true;
            this.CooldownToAttack.Stop();
            this.CooldownToCreateProjectile.Stop();
        }

        public override void CreateProjectile(object source, ElapsedEventArgs args)
        {
            Projectile newProjectile = this.ProjectileToLaunch.Clone() as Projectile;
            float projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement = this.ProjectileToLaunch.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;
            Vector2 velocity = newProjectile.Movement.Velocity;
            velocity.Normalize();
            velocity.X *= Convert.ToSingle(projectileSpeed);
            velocity.Y *= Convert.ToSingle(projectileSpeed);
            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.CurrentPosition = this.Movement.CurrentPosition;
            newProjectile.Parent = this.Attacker;
            GameState.Projectiles.Add(newProjectile);
        }
    }
}
