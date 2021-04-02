namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Entity : Sprite, ICloneable
    {
        public Projectile Projectile;
        public ushort AttackSpeed = 1;

        protected Entity(Texture2D texture, Color color, MovementPattern movement, Projectile projectile)
            : base(texture, color, movement)
        {
            this.Projectile = projectile;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        protected void Attack(List<Sprite> sprites)
        {
            // TODO: needs refactoring and moved to Attack object
            Projectile newProjectile = this.Projectile.Clone() as Projectile;
            int projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
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
