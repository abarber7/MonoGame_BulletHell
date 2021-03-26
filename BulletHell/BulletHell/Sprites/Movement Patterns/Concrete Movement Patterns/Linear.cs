namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;

    internal class Linear : MovementPattern
    {
        public Linear(Dictionary<string, object> linearProperties)
            : base()
        {
            this.Speed = (int)linearProperties["speed"];
            this.velocity.X = Convert.ToSingle((int)linearProperties["xVelocity"]);
            this.velocity.Y = Convert.ToSingle((int)linearProperties["yVelocity"]);
        }

        public override void Move()
        {
            base.Move();
        }
    }
}
