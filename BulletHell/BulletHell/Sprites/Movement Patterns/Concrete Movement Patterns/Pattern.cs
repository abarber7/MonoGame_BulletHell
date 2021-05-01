namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class Pattern : MovementPattern
    {
        private List<Vector2> points;
        private int numberOfCycles;
        private int currentCycle = 0;
        private int previousTargetPoinIndex;
        private int nextTargetPointIndex;

        public Pattern(Vector2 startPosition, int speed, List<Vector2> points, int numberOfCycles)
            : base(startPosition, speed)
        {
            this.points = points;
            this.numberOfCycles = numberOfCycles;

            this.previousTargetPoinIndex = 0;
            this.nextTargetPointIndex = 1;
        }

        public override void InitializeMovement()
        {
            this.CurrentPosition = this.startPosition;
            this.velocity = CalculateVelocity(this.startPosition, this.points[this.nextTargetPointIndex], this.Speed);
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.points[this.previousTargetPoinIndex], this.points[this.nextTargetPointIndex], this.velocity))
            {
                this.previousTargetPoinIndex = this.nextTargetPointIndex;
                this.nextTargetPointIndex = (this.nextTargetPointIndex + 1) % this.points.Count;
                if (this.previousTargetPoinIndex > this.nextTargetPointIndex)
                {
                    this.currentCycle++;
                }

                this.velocity = CalculateVelocity(this.points[this.previousTargetPoinIndex], this.points[this.nextTargetPointIndex], this.Speed);
            }

            if (this.currentCycle == this.numberOfCycles)
            {
                this.CompletedMovement = true;
            }

            base.Move();
        }
    }
}
