namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;

    internal class Bounce : MovementPattern
    {
        public Bounce(Dictionary<string, object> bounceProperties)
            : base()
        {
            this.Speed = (int)bounceProperties["speed"];
            this.velocity.X = Convert.ToSingle((int)bounceProperties["xVelocity"]) * this.Speed;
            this.velocity.Y = Convert.ToSingle((int)bounceProperties["yVelocity"]) * this.Speed;
            this.position.X = Convert.ToSingle((int)bounceProperties["xPosition"]);
            this.position.Y = Convert.ToSingle((int)bounceProperties["yPosition"]);
        }

        public override void Move()
        {
            if (this.IsTouchingTopOfScreen() || this.IsTouchingBottomOfScreen())
            {
                this.InvertYVelocity();
            }

            if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
            {
                this.InvertXVelocity();
            }

            base.Move();
        }
    }
}
