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
            switch (movementPatternProperties["movementPatternType"])
            {
                case "playerInput":
                    movementPattern = new PlayerInput(movementPatternProperties);
                    break;
                case "linear":
                    movementPattern = new Linear(movementPatternProperties);
                    break;
                case "backAndForth":
                    movementPattern = new BackAndForth(movementPatternProperties);
                    break;
                case "static":
                    movementPattern = new Static(movementPatternProperties);
                    break;
                case "pattern":
                    movementPattern = new Pattern(movementPatternProperties);
                    break;
                case "semicircle":
                    movementPattern = new Semicircle(movementPatternProperties);
                    break;
                case "runAndGun":
                    movementPattern = new RunAndGun(movementPatternProperties);
                    break;
                case "bounce":
                    movementPattern = new Bounce(movementPatternProperties);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return movementPattern;
        }
    }
}
