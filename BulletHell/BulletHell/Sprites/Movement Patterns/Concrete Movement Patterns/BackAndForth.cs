namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Timers;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 endPosition;

        private Timer timer = new Timer(15000); // timer for exit at 15000 mili seconds

        public BackAndForth(Vector2 startPosition, Vector2 endPosition, float speed)
            : base(startPosition, speed)
        {
            this.endPosition = endPosition;
        }

        public override void InitializeMovement()
        {
            this.currentPosition = this.startPosition;
            this.CurrentSpeed = this.Speed;
            this.Velocity = this.CalculateVelocity(this.startPosition, this.endPosition, this.CurrentSpeed);
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.startPosition, this.endPosition, this.Velocity))
            {
                this.Velocity = -this.Velocity;
            }

            base.Move();
        }
    }
}
