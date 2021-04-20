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
        private float timer;
        private float delayToAttack = 0.25f;

        public new Circular Movement;

        public CircularHoming(Projectile projectile, Circular circularMovement)
            : base(projectile, circularMovement)
        {
            this.Movement = circularMovement;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer > this.delayToAttack)
            {
                this.timer = 0;
                this.Move();
                this.CreateProjectile(sprites);

                if (this.Movement.cycleCount >= this.numberOfCycles)
                {
                    this.IsRemoved = true;
                }
            }
        }

        public override void CreateProjectile(List<Sprite> sprites)
        {
            Projectile newProjectile = this.Projectile.Clone() as Projectile;
            newProjectile.Movement = this.Projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = this.Movement.CalculateVelocity(this.Movement.Position, targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.Position = this.Movement.Position;
            newProjectile.Parent = this;
            sprites.Add(newProjectile);
        }

        private void Move()
        {
            this.Movement.Move();
        }
    }
}
