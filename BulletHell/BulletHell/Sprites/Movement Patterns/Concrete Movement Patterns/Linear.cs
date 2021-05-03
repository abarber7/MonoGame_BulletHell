namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using Microsoft.Xna.Framework;

    internal class Linear : MovementPattern
    {
        public Linear(Vector2 startPosition, Vector2 velocity, int speed)
            : base(startPosition, speed)
        {
            this.Velocity = velocity;
        }

        public override void Move()
        {
            base.Move();
        }
    }
}
