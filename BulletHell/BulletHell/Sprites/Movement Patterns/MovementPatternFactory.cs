namespace BulletHell.Sprites.Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using Microsoft.Xna.Framework;

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
                    // spawning
                    Vector2 spawnPosition;
                    spawnPosition.X = Convert.ToSingle((int)movementPatternProperties["spawnXPosition"]);
                    spawnPosition.Y = Convert.ToSingle((int)movementPatternProperties["spawnYPosition"]);

                    // movement pattern
                    Vector2 startPosition;
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["startXPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["startYPosition"]);

                    Vector2 endPosition;
                    endPosition.X = Convert.ToSingle((int)movementPatternProperties["endXPosition"]);
                    endPosition.Y = Convert.ToSingle((int)movementPatternProperties["endYPosition"]);

                    int speed = (int)movementPatternProperties["speed"];

                    movementPattern = new BackAndForth(spawnPosition, startPosition, endPosition, speed);
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
                    // spawning
                    // Vector2 spawnPosition;
                    spawnPosition.X = Convert.ToSingle((int)movementPatternProperties["spawnXPosition"]);
                    spawnPosition.Y = Convert.ToSingle((int)movementPatternProperties["spawnYPosition"]);
                    /*int*/ speed = (int)movementPatternProperties["speed"];

                    // movement pattern
                    // Vector2 startPosition;
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["startXPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["startYPosition"]);

                    Vector2 stopPosition;
                    stopPosition.X = Convert.ToSingle((int)movementPatternProperties["endXPosition"]);
                    stopPosition.Y = Convert.ToSingle((int)movementPatternProperties["endYPosition"]);

                    // exit
                    Vector2 exitPosition;
                    exitPosition.X = Convert.ToSingle((int)movementPatternProperties["exitXPosition"]);
                    exitPosition.Y = Convert.ToSingle((int)movementPatternProperties["exitYPosition"]);

                    // timer
                    System.Timers.Timer timer = new System.Timers.Timer((double)movementPatternProperties["timeBeforeExit"] * 1000);

                    movementPattern = new RunAndGun(spawnPosition, startPosition, stopPosition, exitPosition, speed, timer);
                    break;
                case "bounce":
                    movementPattern = new Bounce(movementPatternProperties);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return movementPattern;
        }

        public static List<MovementPattern> CreateListOfMovementPatterns(Dictionary<string, object> movementPatternProperties, int amountOfMovementPatterns)
        {
            List<MovementPattern> movementPatterns = new List<MovementPattern>();

            for (int i = 0; i < amountOfMovementPatterns; i++)
            {
                Dictionary<string, object> properties = new Dictionary<string, object>();
                foreach (string key in movementPatternProperties.Keys)
                {
                    if (movementPatternProperties[key] is List<object>)
                    {
                        properties.Add(key, (int)((List<object>)movementPatternProperties[key])[i]);
                    }
                    else
                    {
                        properties.Add(key, movementPatternProperties[key]);
                    }
                }

                movementPatterns.Add(CreateMovementPattern(properties));
            }

            return movementPatterns;
        }
    }
}
