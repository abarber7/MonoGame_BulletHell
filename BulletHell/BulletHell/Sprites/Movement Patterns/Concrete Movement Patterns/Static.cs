﻿namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using Microsoft.Xna.Framework;

    internal class Static : MovementPattern
    {
        public Static(Vector2 startPosition)
            : base()
        {
            this.Position = startPosition;
        }
    }
}
