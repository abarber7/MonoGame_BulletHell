namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 startPosition;
        private Vector2 endPosition;
        private bool goingTowardsEnd;

        public BackAndForth(Dictionary<string, object> backAndForthProperties)
            : base(backAndForthProperties)
        {
            this.Speed = Convert.ToSingle((int)backAndForthProperties["speed"]);
            this.startPosition.X = Convert.ToSingle((int)backAndForthProperties["xStartPosition"]);
            this.startPosition.Y = Convert.ToSingle((int)backAndForthProperties["yStartPosition"]);
            this.endPosition.X = Convert.ToSingle((int)backAndForthProperties["xEndPosition"]);
            this.endPosition.Y = Convert.ToSingle((int)backAndForthProperties["yEndPosition"]);
            this.Position = this.startPosition;
            this.velocity.X = this.Speed / (this.endPosition.X - this.startPosition.X);
            this.velocity.Y = this.Speed / (this.endPosition.Y - this.startPosition.Y);
            this.goingTowardsEnd = true;
        }

        public override void Move()
        {
            if 

            if (this.goingTowardsEnd)
            {
                this.Position += this.velocity;
            }
            else
            {
                this.Position -= this.velocity;
            }
        }
    }
}
