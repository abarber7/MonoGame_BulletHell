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
        private int numberOfCycles = 1;

        public new Circular Movement;

        public CircularHoming(Projectile projectile, Circular circularMovement)
            : base(projectile, circularMovement)
        {
            this.Movement = circularMovement;
        }

        public override void Update(GameTime gametime, List<Sprite> sprites)
        {
            this.CreateProjectile(sprites);
            this.Move();

            if (this.Movement.cycleCount >= this.numberOfCycles)
            {
                this.IsRemoved = true;
            }
        }

        public override void CreateProjectile(List<Sprite> sprites)
        {
            Projectile newProjectile = this.Projectile.Clone() as Projectile;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = this.Movement.CalculateVelocity(this.Rectangle.Center.ToVector2(), targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.Position = new Vector2(this.Rectangle.Center.X, this.Rectangle.Center.Y);
            newProjectile.Parent = this;
            sprites.Add(newProjectile);
        }

        private void Move()
        {
            this.Movement.Move();
        }
    }
}
