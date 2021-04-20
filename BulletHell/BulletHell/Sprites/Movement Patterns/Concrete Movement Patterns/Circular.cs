namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using Microsoft.Xna.Framework;

    internal class Circular : MovementPattern
    {
        public int cycleCount = 0;
        private double radius;
        private double startingDegrees;
        private double degreesToRotate;
        private double currentDegrees;
        private bool exceededStartPosition = false;

        public Circular(Vector2 originPosition, double radius, double degreesToRotate, double startingDegrees)
        {
            this.Origin = originPosition;
            this.radius = radius;
            this.startingDegrees = startingDegrees;
            this.degreesToRotate = degreesToRotate;
            this.currentDegrees = startingDegrees;
        }

        public override void Move()
        {
            this.position.X = Convert.ToSingle(this.Origin.X + (Math.Cos(this.currentDegrees * (Math.PI / 180)) * this.radius));
            this.position.Y = Convert.ToSingle(this.Origin.X + (Math.Sin(this.currentDegrees * (Math.PI / 180)) * this.radius));

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
