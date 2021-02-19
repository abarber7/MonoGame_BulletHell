namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 startPosition;
        private Vector2 endPosition;

        public BackAndForth(Dictionary<string, object> backAndForthProperties)
            : base(backAndForthProperties)
        {
            this.Speed = (int)backAndForthProperties["speed"];
            this.startPosition.X = Convert.ToSingle((int)backAndForthProperties["xStartPosition"]);
            this.startPosition.Y = Convert.ToSingle((int)backAndForthProperties["yStartPosition"]);
            this.endPosition.X = Convert.ToSingle((int)backAndForthProperties["xEndPosition"]);
            this.endPosition.Y = Convert.ToSingle((int)backAndForthProperties["yEndPosition"]);
            this.Position = this.startPosition;
            this.velocity = this.CalculateVelocity(this.startPosition, this.endPosition, this.Speed);
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.startPosition, this.endPosition, this.velocity))
            {
                this.velocity = -this.velocity;
            }

            base.Move();
        }
    }
}
