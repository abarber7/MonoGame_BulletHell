namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class Pattern : MovementPattern
    {
        private List<Vector2> points;
        private int previousTargetPoinIndex;
        private int nextTargetPointIndex;

        public Pattern(Dictionary<string, object> patternProperties)
            : base()
        {
            this.Speed = (int)patternProperties["speed"];
            this.points = new List<Vector2>();
            List<List<int>> listOfPoints = (List<List<int>>)patternProperties["points"];
            foreach (List<int> point in listOfPoints)
            {
                this.points.Add(new Vector2(point[0], point[1]));
            }

            this.previousTargetPoinIndex = 0;
            this.nextTargetPointIndex = 1;
            this.Position = this.points[0];
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
