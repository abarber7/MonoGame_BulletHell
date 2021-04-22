namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class Pattern : MovementPattern
    {
        private List<Vector2> points;
        private int previousTargetPoinIndex;
        private int nextTargetPointIndex;

        public Pattern(Vector2 startPosition, int speed, List<Vector2> points)
            : base(startPosition, speed)
        {
            this.points = points;

            this.previousTargetPoinIndex = 0;
            this.nextTargetPointIndex = 1;
            this.CurrentPosition = this.points[0];
            this.velocity = this.CalculateVelocity(this.points[this.previousTargetPoinIndex], this.points[this.nextTargetPointIndex], this.Speed);
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.points[this.previousTargetPoinIndex], this.points[this.nextTargetPointIndex], this.velocity))
            {
                this.previousTargetPoinIndex = this.nextTargetPointIndex;
                this.nextTargetPointIndex = (this.nextTargetPointIndex + 1) % this.points.Count;
                this.velocity = this.CalculateVelocity(this.points[this.previousTargetPoinIndex], this.points[this.nextTargetPointIndex], this.Speed);
            }

            base.Move();
        }
    }
}
