namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Timers;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 endPosition;
        private int numberOfCycles;
        private int currentCycle = 0;
        private bool headingTowardEndPosition = false;

        public BackAndForth(Vector2 startPosition, Vector2 endPosition, float speed, int numberOfCycles)
            : base(startPosition, speed)
        {
            this.endPosition = endPosition;
            this.numberOfCycles = numberOfCycles;
        }

        public override void InitializeMovement()
        {
            this.CurrentPosition = this.startPosition;
            this.CurrentSpeed = this.Speed;
            this.Velocity = CalculateVelocity(this.startPosition, this.endPosition, this.CurrentSpeed);
            this.headingTowardEndPosition = true;
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.startPosition, this.endPosition, this.Velocity))
            {
                this.Velocity = -this.Velocity;
                if (this.headingTowardEndPosition == true)
                {
                    this.headingTowardEndPosition = false;
                }
                else
                {
                    this.currentCycle++;
                    this.headingTowardEndPosition = true;
                }
            }

            if (this.currentCycle == this.numberOfCycles)
            {
                this.CompletedMovement = true;
            }

            base.Move();
        }
    }
}
