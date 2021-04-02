namespace BulletHell.Sprites.Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns;
    using Microsoft.Xna.Framework;

    internal class MovementPatternFactory
    {
        public static MovementPattern CreateMovementPattern(Dictionary<string, object> movementPatternProperties)
        {
            MovementPattern movementPattern = null;
            switch (movementPatternProperties["movementPatternType"])
            {
                case "playerInput":
                    // spawning
                    Vector2 spawnPosition;
                    spawnPosition.X = Convert.ToSingle((int)movementPatternProperties["spawnXPosition"]);
                    spawnPosition.Y = Convert.ToSingle((int)movementPatternProperties["spawnYPosition"]);

                    // movement pattern
                    Vector2 startPosition;
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["startXPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["startYPosition"]);

                    int speed = (int)movementPatternProperties["speed"];

                    movementPattern = new PlayerInput(spawnPosition, startPosition, speed);
                    break;
                case "linear":
                    Vector2 velocity;
                    velocity.X = Convert.ToSingle((int)movementPatternProperties["xVelocity"]);
                    velocity.Y = Convert.ToSingle((int)movementPatternProperties["yVelocity"]);
                    speed = (int)movementPatternProperties["speed"];
                    movementPattern = new Linear(velocity, speed);
                    break;
                case "backAndForth":
                    // spawning
                    spawnPosition.X = Convert.ToSingle((int)movementPatternProperties["spawnXPosition"]);
                    spawnPosition.Y = Convert.ToSingle((int)movementPatternProperties["spawnYPosition"]);

                    // movement pattern
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["startXPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["startYPosition"]);

                    Vector2 endPosition;
                    endPosition.X = Convert.ToSingle((int)movementPatternProperties["endXPosition"]);
                    endPosition.Y = Convert.ToSingle((int)movementPatternProperties["endYPosition"]);

                    speed = (int)movementPatternProperties["speed"];

                    movementPattern = new BackAndForth(spawnPosition, startPosition, endPosition, speed);
                    break;
                case "static":
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["xPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["yPosition"]);
                    movementPattern = new Static(startPosition);
                    break;
                case "pattern":
                    speed = (int)movementPatternProperties["speed"];
                    List<Vector2> points = new List<Vector2>();
                    List<List<int>> listOfPoints = (List<List<int>>)movementPatternProperties["points"];
                    foreach (List<int> point in listOfPoints)
                    {
                        points.Add(new Vector2(point[0], point[1]));
                    }

                    movementPattern = new Pattern(points, speed);
                    break;
                case "semicircle":
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["startXPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["startYPosition"]);
                    endPosition.X = Convert.ToSingle((int)movementPatternProperties["endXPosition"]);
                    endPosition.Y = Convert.ToSingle((int)movementPatternProperties["endYPosition"]);
                    speed = (int)movementPatternProperties["speed"];
                    bool half1Or2 = (bool)movementPatternProperties["half1Or2"];
                    movementPattern = new Semicircle(startPosition, endPosition, speed, half1Or2);
                    break;
                case "runAndGun":
                    // spawning
                    // Vector2 spawnPosition;
                    spawnPosition.X = Convert.ToSingle((int)movementPatternProperties["spawnXPosition"]);
                    spawnPosition.Y = Convert.ToSingle((int)movementPatternProperties["spawnYPosition"]);
                    /*int*/
                    speed = (int)movementPatternProperties["speed"];

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
                    speed = (int)movementPatternProperties["speed"];
                    velocity.X = Convert.ToSingle((int)movementPatternProperties["xVelocity"]) * speed;
                    velocity.Y = Convert.ToSingle((int)movementPatternProperties["yVelocity"]) * speed;
                    startPosition.X = Convert.ToSingle((int)movementPatternProperties["startXPosition"]);
                    startPosition.Y = Convert.ToSingle((int)movementPatternProperties["startYPosition"]);

                    movementPattern = new Bounce(startPosition, velocity, speed);
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
                    if (movementPatternProperties[key] is List<object> list)
                    {
                        properties.Add(key, (int)list[i]);
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
