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

            Vector2 startPosition;
            if (movementPatternProperties.ContainsKey("startXPosition") && movementPatternProperties.ContainsKey("startYPosition"))
            {
                startPosition.X = (float)movementPatternProperties["startXPosition"];
                startPosition.Y = (float)movementPatternProperties["startYPosition"];
            }
            else
            {
                startPosition = default(Vector2);
            }

            int speed;
            if (movementPatternProperties.ContainsKey("speed"))
            {
                speed = Convert.ToInt32((float)movementPatternProperties["speed"]);
            }
            else
            {
                speed = 0;
            }

            switch (movementPatternProperties["movementPatternType"])
            {
                case "playerInput":
                    // spawning
                    Vector2 spawnPosition;
                    spawnPosition.X = (float)movementPatternProperties["spawnXPosition"];
                    spawnPosition.Y = (float)movementPatternProperties["spawnYPosition"];

                    // movement pattern
                    movementPattern = new PlayerInput(spawnPosition, startPosition, speed);
                    break;
                case "linear":
                    Vector2 velocity;
                    velocity.X = (float)movementPatternProperties["xVelocity"];
                    velocity.Y = (float)movementPatternProperties["yVelocity"];

                    startPosition.X = (float)movementPatternProperties["xPosition"];
                    startPosition.Y = (float)movementPatternProperties["yPosition"];
                    movementPattern = new Linear(startPosition, velocity, speed);
                    break;
                case "backAndForth":
                    // spawning
                    spawnPosition.X = (float)movementPatternProperties["spawnXPosition"];
                    spawnPosition.Y = (float)movementPatternProperties["spawnYPosition"];

                    // movement pattern
                    startPosition.X = (float)movementPatternProperties["startXPosition"];
                    startPosition.Y = (float)movementPatternProperties["startYPosition"];

                    Vector2 endPosition;
                    endPosition.X = (float)movementPatternProperties["endXPosition"];
                    endPosition.Y = (float)movementPatternProperties["endYPosition"];

                    int numberOfCycles = Convert.ToInt32((float)movementPatternProperties["numberOfCycles"]);

                    movementPattern = new BackAndForth(startPosition, endPosition, speed, numberOfCycles);
                    break;
                case "static":
                    startPosition.X = (float)movementPatternProperties["xPosition"];
                    startPosition.Y = (float)movementPatternProperties["yPosition"];
                    movementPattern = new Static(startPosition);
                    break;
                case "pattern":
                    List<Vector2> points = new List<Vector2>();
                    List<object> listOfPoints = (List<object>)movementPatternProperties["points"];
                    foreach (List<object> point in listOfPoints)
                    {
                        points.Add(new Vector2((float)point[0], (float)point[1]));
                    }

                    numberOfCycles = Convert.ToInt32((float)movementPatternProperties["numberOfCycles"]);

                    movementPattern = new Pattern(points[0], speed, points, numberOfCycles);
                    break;
                case "runAndGun":
                    Vector2 stopPosition;
                    stopPosition.X = (float)movementPatternProperties["endXPosition"];
                    stopPosition.Y = (float)movementPatternProperties["endYPosition"];

                    // timer
                    System.Timers.Timer timer = new System.Timers.Timer((float)movementPatternProperties["timeBeforeExit"] * 1000);

                    movementPattern = new RunAndGun(startPosition, stopPosition, speed, timer);
                    break;
                case "bounce":
                    startPosition.X = (float)movementPatternProperties["startXPosition"];
                    startPosition.Y = (float)movementPatternProperties["startYPosition"];

                    movementPattern = new Bounce(startPosition, speed);
                    break;
                case "circular":
                    int numberOfTimesToCycle = Convert.ToInt32((float)movementPatternProperties["timesToCycles"]);
                    Vector2 originPosition;
                    originPosition.X = (float)movementPatternProperties["originXPosition"];
                    originPosition.Y = (float)movementPatternProperties["originYPosition"];
                    float radius = (float)movementPatternProperties["radius"];
                    float amountToIncreaseRadiusBy = (float)movementPatternProperties["amountToIncreaseRadiusBy"];
                    float degreesToRotate = (float)movementPatternProperties["degreesToRotate"];
                    float startingDegrees = (float)movementPatternProperties["startingDegrees"];

                    movementPattern = new Circular(numberOfTimesToCycle, originPosition, radius, amountToIncreaseRadiusBy, degreesToRotate, startingDegrees);
                    break;
                case "randomWithBounds":
                    int upperXBound = Convert.ToInt32((float)movementPatternProperties["upperXBound"]);
                    int lowerXBound = Convert.ToInt32((float)movementPatternProperties["lowerXBound"]);
                    int upperYBound = Convert.ToInt32((float)movementPatternProperties["upperYBound"]);
                    int lowerYBound = Convert.ToInt32((float)movementPatternProperties["lowerYBound"]);
                    timer = new System.Timers.Timer((float)movementPatternProperties["timeBeforeExit"] * 1000);

                    movementPattern = new RandomWithBounds(startPosition, speed, upperXBound, lowerXBound, upperYBound, lowerYBound, timer);
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
                        properties.Add(key, list[i]);
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

        public static List<Vector2> CreateListOfSpawnPositions(Dictionary<string, object> movementPatternProperties, int amountOfMovementPatterns)
        {
            List<Vector2> spawnPositions = new List<Vector2>();
            if (!(movementPatternProperties["spawnXPosition"] is List<object>))
            {
                for (int i = 0; i < amountOfMovementPatterns; i++)
                {
                    spawnPositions.Add(new Vector2((float)movementPatternProperties["spawnXPosition"], (float)movementPatternProperties["spawnYPosition"]));
                }
            }
            else
            {
                List<object> spawnXPositions = (List<object>)movementPatternProperties["spawnXPosition"];
                List<object> spawnYPositions = (List<object>)movementPatternProperties["spawnYPosition"];

                for (int i = 0; i < amountOfMovementPatterns; i++)
                {
                    spawnPositions.Add(new Vector2((float)spawnXPositions[i], (float)spawnYPositions[i]));
                }
            }

            return spawnPositions;
        }

        public static List<Vector2> CreateListOfDespawnPositions(Dictionary<string, object> movementPatternProperties, int amountOfMovementPatterns)
        {
            List<Vector2> despawnPositions = new List<Vector2>();
            if (!(movementPatternProperties["despawnXPosition"] is List<object>))
            {
                for (int i = 0; i < amountOfMovementPatterns; i++)
                {
                    despawnPositions.Add(new Vector2((float)movementPatternProperties["despawnXPosition"], (float)movementPatternProperties["despawnYPosition"]));
                }
            }
            else
            {
                List<object> spawnXPositions = (List<object>)movementPatternProperties["despawnXPosition"];
                List<object> spawnYPositions = (List<object>)movementPatternProperties["despawnYPosition"];

                for (int i = 0; i < amountOfMovementPatterns; i++)
                {
                    despawnPositions.Add(new Vector2((float)spawnXPositions[i], (float)spawnYPositions[i]));
                }
            }

            return despawnPositions;
        }
    }
}
