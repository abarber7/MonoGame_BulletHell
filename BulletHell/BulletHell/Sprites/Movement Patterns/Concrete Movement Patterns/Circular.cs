namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using Microsoft.Xna.Framework;

    internal class Circular : MovementPattern
    {
        public Vector2 ActualPosition;
        private int numberOfTimesToCycle;
        private double radius;
        private double startingDegrees;
        private double degreesToRotate;
        private double currentDegrees;

        private int currentCycle = 0;
        private bool exceededStartPosition = false;

        public Circular(int numberOfTimesToCycle, Vector2 actualPosition, double radius, double degreesToRotate, double startingDegrees)
        {
            this.numberOfTimesToCycle = numberOfTimesToCycle;
            this.ActualPosition = actualPosition;
            this.radius = radius;
            this.startingDegrees = startingDegrees;
            this.degreesToRotate = degreesToRotate;
            this.currentDegrees = startingDegrees;
        }

        public override void Move()
        {
            this.ActualPosition.X = Convert.ToSingle(this.Position.X + (Math.Cos(this.currentDegrees * (Math.PI / 180)) * this.radius));
            this.ActualPosition.Y = Convert.ToSingle(this.Position.Y + (Math.Sin(this.currentDegrees * (Math.PI / 180)) * this.radius));

            if (this.currentDegrees + this.degreesToRotate > this.startingDegrees && !this.exceededStartPosition)
            {
                this.currentCycle++;
                this.exceededStartPosition = true;
            }

            double newDegrees = this.currentDegrees + this.degreesToRotate;

            if (newDegrees >= 360)
            {
                this.exceededStartPosition = false;
            }

            this.currentDegrees = newDegrees % 360;

            if (this.currentCycle > this.numberOfTimesToCycle)
            {
                this.Parent.IsRemoved = true;
            }
        }
    }
}
