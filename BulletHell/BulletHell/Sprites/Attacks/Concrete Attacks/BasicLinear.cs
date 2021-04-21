namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    internal class BasicLinear : Attack
    {
        public BasicLinear(Projectile projectile, MovementPattern movement, double cooldownToCreateProjectile)
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
            Projectile newProjectile = this.projectile.Clone() as Projectile;
            int projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;
            Vector2 velocity = newProjectile.Movement.Velocity;
            velocity.Normalize();
            velocity.X *= projectileSpeed;
            velocity.Y *= projectileSpeed;
            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.Position = new Vector2(this.Rectangle.Center.X, this.Rectangle.Center.Y);
            newProjectile.Parent = this;
            sprites.Add(newProjectile);
        }
    }
}
