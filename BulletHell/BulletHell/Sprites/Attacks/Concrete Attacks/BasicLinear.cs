namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    internal class BasicLinear : Attack
    {
        public BasicLinear(Projectile projectile, MovementPattern movement, float cooldownToCreateProjectile)
            : base(projectile, movement, cooldownToCreateProjectile)
        {
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            this.CreateProjectile(sprites);
            this.IsRemoved = true;
        }

        protected override void CreateProjectile(List<Sprite> sprites)
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
            sprites.Add(newProjectile);
        }
    }
}
