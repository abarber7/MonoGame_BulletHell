namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using Microsoft.Xna.Framework;

    internal class Bounce : MovementPattern
    {
        public Bounce(Vector2 startPosition, int speed)
            : base(startPosition, speed)
        {
        }

        public override void Move()
        {
            if (this.IsTouchingTopOfScreen() || this.IsTouchingBottomOfScreen())
            {
                this.InvertYVelocity();
            }

            if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
            {
                this.InvertXVelocity();
            }

            base.Move();
        }
    }
}
