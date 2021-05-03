namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Timers;
    using Microsoft.Xna.Framework;

    internal class RandomWithBounds : MovementPattern
    {
        private int upperXBound;
        private int lowerXBound;
        private int upperYBound;
        private int lowerYBound;
        private Timer timer;
        private Random random = new Random();
        private Vector2 previousPosition;
        private Vector2 nextPosition;

        public RandomWithBounds(Vector2 startPosition, float speed, int upperXBound, int lowerXBound, int upperYBound, int lowerYBound, Timer timer)
            : base(startPosition, speed)
        {
            this.upperXBound = upperXBound;
            this.lowerXBound = lowerXBound;
            this.upperYBound = upperYBound;
            this.lowerYBound = lowerYBound;
            this.timer = timer;
            this.timer.Elapsed += this.MovementComplete;
            this.timer.Stop();
        }

        public override void InitializeMovement()
        {
            this.CurrentPosition = this.startPosition;
            this.CurrentSpeed = this.Speed;
            this.previousPosition = this.startPosition;
            this.nextPosition = this.GetNextPosition();
            this.Velocity = CalculateVelocity(this.startPosition, this.nextPosition, this.CurrentSpeed);
            this.timer.Start();
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.previousPosition, this.nextPosition, this.Velocity))
            {
                this.previousPosition = this.nextPosition;
                this.nextPosition = this.GetNextPosition();
                this.Velocity = CalculateVelocity(this.previousPosition, this.nextPosition, this.CurrentSpeed);
            }

            base.Move();
        }

        private void MovementComplete(object source, ElapsedEventArgs e)
        {
            this.CompletedMovement = true; // change bool so the entity will exit
        }

        private Vector2 GetNextPosition()
        {
            Vector2 newPosition;
            do
            {
                float xPosition = this.random.Next(this.lowerXBound, this.upperXBound);
                float yPosition = this.random.Next(this.lowerYBound, this.upperYBound);
                newPosition = new Vector2(xPosition, yPosition);
            }
            while (Vector2.Distance(this.CurrentPosition, newPosition) < 100);

            return newPosition;
        }
    }
}
