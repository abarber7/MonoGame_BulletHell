using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns
{
    class MovementPatternFactory
    {
        public static MovementPattern createMovementPattern(Dictionary<string, object> movementPatternProperties)
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
                case "BackAndForth":
                    movementPattern = new BackAndForth(movementPatternProperties);
                    break;
                case "Static":
                    movementPattern = new Static(movementPatternProperties);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }
            return movementPattern;
        }
    }
}
