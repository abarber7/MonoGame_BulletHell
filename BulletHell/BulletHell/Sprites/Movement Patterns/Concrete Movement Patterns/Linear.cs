using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    class Linear : MovementPattern
    {
        public Linear(Dictionary<string, object> linearProperties) : base(linearProperties)
        {
            velocity.X = Convert.ToSingle((Int32)linearProperties["xVelocity"]);
            velocity.Y = Convert.ToSingle((Int32)linearProperties["yVelocity"]);
        }

        public override void Move()
        {
            this.position += this.velocity;
        }
    }
}
