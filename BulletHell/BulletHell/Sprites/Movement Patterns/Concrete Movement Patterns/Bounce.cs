namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;

    internal class Bounce : MovementPattern
    {
        public Bounce(Dictionary<string, object> bounceProperties)
            : base(bounceProperties)
        {
            this.Speed = (int)bounceProperties["speed"];
            this.velocity.X = Convert.ToSingle((int)bounceProperties["xVelocity"]) * this.Speed;
            this.velocity.Y = Convert.ToSingle((int)bounceProperties["yVelocity"]) * this.Speed;
            this.Position.X = Convert.ToSingle((int)bounceProperties["xPosition"]);
            this.Position.Y = Convert.ToSingle((int)bounceProperties["yPosition"]);
        }

        public override void Move()
        {
            if (this.IsTouchingTopOfScreen() || this.IsTouchingBottomOfScreen())
            {
                this.velocity.Y = -this.velocity.Y;
            }

            if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
            {
                this.velocity.X = -this.velocity.X;
            }

            base.Move();
        }
    }
}
