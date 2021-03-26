namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class Semicircle : MovementPattern
    {
        private Vector2 startPosition;
        private Vector2 centerPosition;
        private Vector2 endPosition;
        private float radius;
        private Vector2 previousPosition;
        private Vector2 nextPosition;
        private bool half1Or2; // Circles have two semicircles, 1 or 2 denoted by true or false.
        private float count = 0;

        public Semicircle(Vector2 startPosition, Vector2 endPosition, int speed, bool half1Or2)
            : base()
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.Speed = speed;
            this.half1Or2 = half1Or2;
            this.Position = this.startPosition;
            this.centerPosition = new Vector2((this.startPosition.X + this.endPosition.X) / 2, (this.startPosition.Y + this.endPosition.Y) / 2);
            this.SetRadius();
            this.SetCenter();
            this.previousPosition = new Vector2(this.startPosition.X, this.startPosition.Y);
            this.nextPosition = this.CalculateNextPosition();
            this.velocity = this.CalculateVelocity(this.previousPosition, this.nextPosition, this.Speed);
        }

        public override void Move()
        {
            if (this.ExceededPosition(this.previousPosition, this.nextPosition, this.velocity))
            {
                this.previousPosition = new Vector2(this.nextPosition.X, this.nextPosition.Y);
                this.nextPosition = this.CalculateNextPosition();
                this.velocity = this.CalculateVelocity(this.previousPosition, this.nextPosition, this.Speed);
            }

            base.Move();
        }

        private void SetCenter()
        {
            double radsq = (double)(this.radius * this.radius);
            double q = Math.Sqrt((double)(
                ((this.endPosition.X - this.startPosition.X) * (this.endPosition.X - this.startPosition.X)) +
                ((this.endPosition.Y - this.startPosition.Y) * (this.endPosition.Y - this.startPosition.Y))));
            double x3 = (double)((this.startPosition.X + this.endPosition.X) / 2);
            double y3 = (double)((this.startPosition.Y + this.endPosition.Y) / 2);
            double baseX = Math.Sqrt(radsq - Math.Pow(q / 2, 2)) * (((double)(this.startPosition.Y - this.endPosition.Y)) / q);
            double baseY = Math.Sqrt(radsq - Math.Pow(q / 2, 2)) * (((double)(this.endPosition.X - this.startPosition.X)) / q);

            float centerX = (float)(x3 - baseX);
            float centerY = (float)(y3 - baseY);
            this.centerPosition = new Vector2(centerX, centerY);
        }

        private void SetRadius()
        {
            double portionX = Math.Pow((double)(this.endPosition.X - this.startPosition.X), 2);
            double portionY = Math.Pow((double)(this.endPosition.Y - this.startPosition.Y), 2);
            this.radius = (float)(Math.Sqrt(portionX + portionY) / 2);
        }

        private Vector2 CalculateNextPosition()
        {
            this.count++;
            float nextX;
            float nextY;

            if (this.count > 6)
            {
                this.count = 1;
            }

            if (this.half1Or2)
            {
                nextX = this.centerPosition.X - (this.radius * (float)Math.Cos(30 * this.count));
                nextY = this.centerPosition.Y - (this.radius * (float)Math.Sin(30 * this.count)); // (float)Math.Sqrt(Math.Abs((double)((this.radius * this.radius) - (nextX * nextX))));
                return new Vector2(nextX, nextY);
            }
            else
            {
                nextX = this.centerPosition.X + (this.radius * (float)Math.Cos(30 * this.count));
                nextY = this.centerPosition.Y + (this.radius * (float)Math.Sin(30 * this.count)); // (float)Math.Sqrt(Math.Abs((double)((this.radius * this.radius) - (nextX * nextX))));
                return new Vector2(nextX, nextY);
            }
        }
    }
}
