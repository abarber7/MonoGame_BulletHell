namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using Microsoft.Xna.Framework;

    internal class Circular : MovementPattern
    {
        public int cycleCount = 0;
        private Vector2 originPosition;
        private double radius;
        private double startingDegrees;
        private double degreesToRotate;
        private double currentDegrees;
        private bool exceededStartPosition = false;

        public Circular(Vector2 originPosition, double radius, double degreesToRotate, double startingDegrees)
        {
            this.originPosition = originPosition;
            this.radius = radius;
            this.startingDegrees = startingDegrees;
            this.degreesToRotate = degreesToRotate;
            this.currentDegrees = startingDegrees;
        }

        public override void Move()
        {
            this.position.X = Convert.ToSingle(this.originPosition.X + (Math.Cos(this.currentDegrees) * this.radius));
            this.position.Y = Convert.ToSingle(this.originPosition.X + (Math.Sin(this.currentDegrees) * this.radius));

            if (this.currentDegrees + this.degreesToRotate > this.startingDegrees)
            {
                this.cycleCount++;
                this.exceededStartPosition = true;
            }

            double newDegrees = this.currentDegrees + this.degreesToRotate;

            if (newDegrees >= 360)
            {
                this.exceededStartPosition = false;
            }

            this.currentDegrees = newDegrees % 360;
        }
    }
}
