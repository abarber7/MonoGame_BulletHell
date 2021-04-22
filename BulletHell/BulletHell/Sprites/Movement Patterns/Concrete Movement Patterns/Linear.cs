namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using Microsoft.Xna.Framework;

    internal class Linear : MovementPattern
    {
        public Linear(Vector2 startPosition, int speed)
            : base(startPosition, speed)
        {
        }

        public override void Move()
        {
            base.Move();
        }
    }
}
