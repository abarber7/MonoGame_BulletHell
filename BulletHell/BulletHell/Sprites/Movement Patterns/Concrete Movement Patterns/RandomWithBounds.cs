namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using Microsoft.Xna.Framework;

    internal class RandomWithBounds : MovementPattern
    {
        private int upperHorizontalBound;
        private int lowerHorizontalBound;
        private int upperVerticalBound;
        private int lowerVerticalBound;

        public RandomWithBounds(Vector2 startPosition, float speed, int upperHorizontalBound, int lowerHorizontalBound, int upperVerticalBound, int lowerVerticalBound)
            : base(startPosition, speed)
        {
            this.upperHorizontalBound = upperHorizontalBound;
            this.lowerHorizontalBound = lowerHorizontalBound;
            this.upperVerticalBound = upperVerticalBound;
            this.lowerVerticalBound = lowerVerticalBound;
        }
    }
}
