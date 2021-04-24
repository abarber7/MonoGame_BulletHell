namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using Microsoft.Xna.Framework;

    internal class Circular : MovementPattern
    {
        private int numberOfTimesToCycle;
        private float radius;
        private float amountToIncreaseRadiusBy;
        private float startingDegrees;
        private float degreesToRotate;
        private float currentDegrees;

        private int currentCycle = 0;
        private bool exceededStartPosition = false;

        public Circular(int numberOfTimesToCycle, Vector2 originPosition, float radius, float amountToIncreaseRadiusBy, float degreesToRotate, float startingDegrees)
            : base(originPosition, 0)
        {
            this.numberOfTimesToCycle = numberOfTimesToCycle;
            this.CurrentPosition = originPosition;
            this.radius = radius;
            this.amountToIncreaseRadiusBy = amountToIncreaseRadiusBy;
            this.startingDegrees = startingDegrees;
            this.degreesToRotate = degreesToRotate;
            this.currentDegrees = startingDegrees;
        }

        public override void Move()
        {
            this.radius += this.amountToIncreaseRadiusBy;

            if (this.currentDegrees + this.degreesToRotate > this.startingDegrees && !this.exceededStartPosition)
            {
                this.currentCycle++;
                this.exceededStartPosition = true;
            }

            float newDegrees = this.currentDegrees + this.degreesToRotate;

            if (newDegrees >= 360)
            {
                this.exceededStartPosition = false;
            }

            this.currentDegrees = newDegrees % 360;
        }

        public Vector2 GetActualPosition(float degreeOffset = 0)
        {
            Vector2 actualPosition = default(Vector2);
            actualPosition.X = Convert.ToSingle(this.CurrentPosition.X + (Math.Cos((this.currentDegrees + degreeOffset) * (Math.PI / 180)) * this.radius));
            actualPosition.Y = Convert.ToSingle(this.CurrentPosition.Y + (Math.Sin((this.currentDegrees + degreeOffset) * (Math.PI / 180)) * this.radius));
            return actualPosition;
        }

        public bool IsMovementDone()
        {
            if (this.currentCycle > this.numberOfTimesToCycle)
            {
                return true;
            }

            return false;
        }

        public new object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
