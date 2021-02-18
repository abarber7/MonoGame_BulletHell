namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 startPosition;
        private Vector2 endPosition;

        public BackAndForth(Dictionary<string, object> backAndForthProperties)
            : base(backAndForthProperties)
        {
            this.Speed = (float)backAndForthProperties["speed"];
            this.startPosition.X = (float)backAndForthProperties["xStartPosition"];
            this.startPosition.Y = (float)backAndForthProperties["yStartPosition"];
            this.endPosition.X = (float)backAndForthProperties["xEndPosition"];
            this.endPosition.Y = (float)backAndForthProperties["yEndPosition"];
            this.velocity.X = this.Speed / (this.endPosition.X - this.startPosition.X);
            this.velocity.Y = this.Speed / (this.endPosition.Y - this.startPosition.Y);
        }

        public override void Move()
        {
        }
    }
}
