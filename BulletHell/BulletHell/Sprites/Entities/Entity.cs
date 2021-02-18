namespace BulletHell.Sprites.Entities
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using global::BulletHell.Sprites.Projectiles;

    internal abstract class Entity : Sprite
    {
        // private int healthPoints;
        protected Projectile Projectile;
        public ushort AttackSpeed = 1;

        protected Entity(Dictionary<string, object> entityProperties)
            : base(entityProperties)
        {
            // healthPoints = (int)entityProperties["healthPoints"];
            this.Projectile = ProjectileFactory.CreateProjectile((Dictionary<string, object>)entityProperties["projectile"]);
        }

        protected void Attack(List<Sprite> sprites)
        {
            Projectile newProjectile = this.Projectile.Clone() as Projectile;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Position = this.Movement.Position;
            newProjectile.Parent = this;

            sprites.Add(newProjectile);
        }
    }
}
