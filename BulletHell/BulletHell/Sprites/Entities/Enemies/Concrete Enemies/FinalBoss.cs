namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using global::BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    internal class FinalBoss : Enemy
    {
        private int previousTime = 0;

        public FinalBoss(Dictionary<string, object> finalBossProperties)
            : base(finalBossProperties)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);

            if (this.previousTime != (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.Attack(sprites);
            }

            this.previousTime = (int)gameTime.TotalGameTime.TotalSeconds;
        }

        private new void Attack(List<Sprite> sprites)
        {
            // TODO: needs refactoring and moved to Attack object
            /*Projectile newProjectile = this.Projectile.Clone() as Projectile;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.velocity = this.Movement.velocity;
            newProjectile.Movement.Position = this.Movement.Position;
            int projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement.velocity.X = 0;
            newProjectile.Movement.velocity.Y = 1;
            newProjectile.Movement.velocity.X *= projectileSpeed;
            newProjectile.Movement.velocity.Y *= projectileSpeed;
            newProjectile.Movement.Position = this.Movement.Position;
            newProjectile.Parent = this;

            sprites.Add(newProjectile);

            newProjectile = this.Projectile.Clone() as Projectile;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.velocity = this.Movement.velocity;
            newProjectile.Movement.Position = this.Movement.Position;
            projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement.velocity.X = 0;
            newProjectile.Movement.velocity.Y = 1;
            newProjectile.Movement.velocity.X *= projectileSpeed;
            newProjectile.Movement.velocity.Y *= projectileSpeed;
            newProjectile.Movement.velocity.X += 2;
            newProjectile.Parent = this;

            sprites.Add(newProjectile);

            newProjectile = this.Projectile.Clone() as Projectile;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.velocity = this.Movement.velocity;
            newProjectile.Movement.Position = this.Movement.Position;
            projectileSpeed = newProjectile.Movement.Speed;
            newProjectile.Movement.velocity.X = 0;
            newProjectile.Movement.velocity.Y = 1;
            newProjectile.Movement.velocity.X *= projectileSpeed;
            newProjectile.Movement.velocity.Y *= projectileSpeed;
            newProjectile.Movement.velocity.X -= 2;
            newProjectile.Parent = this;

            sprites.Add(newProjectile);*/
        }
    }
}
