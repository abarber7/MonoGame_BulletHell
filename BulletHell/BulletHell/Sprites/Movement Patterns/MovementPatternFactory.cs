namespace BulletHell.Sprites.Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;

    internal class MovementPatternFactory
    {
        public static MovementPattern CreateMovementPattern(Dictionary<string, object> movementPatternProperties)
        {
            MovementPattern movementPattern = null;
            movementPattern = movementPatternProperties["movementPatternType"] switch
            {
                "playerInput" => new PlayerInput(movementPatternProperties),
                "linear" => new Linear(movementPatternProperties),
                "backAndForth" => new BackAndForth(movementPatternProperties),
                "static" => new Static(movementPatternProperties),
                "pattern" => new Pattern(movementPatternProperties),
                "semicircle" => new Semicircle(movementPatternProperties),
                _ => throw new Exception("Invalid Entity"),
            };

            return movementPattern;
        }
    }
}
