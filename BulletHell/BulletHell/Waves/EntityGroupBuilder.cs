namespace BulletHell.Waves
{
    using System;
    using System.Collections.Generic;
    using System.Timers;
    using BulletHell.Game_Utilities;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;

    internal class EntityGroupBuilder
    {
        public static EntityGroup CreateEntityGroup(Dictionary<string, object> entityGroupProperties)
        {
            Entity entityType = GameLoader.GetEnemy((string)entityGroupProperties["entityType"]);
            int entityAmount = Convert.ToInt32((float)entityGroupProperties["entityAmount"]);

            List<Timer> delaysTillSpawn = null;
            if (entityGroupProperties["delayTillSpawn"] is List<object>)
            {
                delaysTillSpawn = CreateListOfTimers((List<object>)entityGroupProperties["delayTillSpawn"]);
            }
            else if (entityGroupProperties["delayTillSpawn"] is float)
            {
                Timer timer = CreateTimer((float)entityGroupProperties["delayTillSpawn"]);
                delaysTillSpawn = new List<Timer>() { timer };
            }

            Dictionary<string, object> movementPatternProperties = (Dictionary<string, object>)entityGroupProperties["movementPattern"];
            List<MovementPattern> movementPatterns = MovementPatternFactory.CreateListOfMovementPatterns(movementPatternProperties, entityAmount);
            List<Vector2> spawnPositions = MovementPatternFactory.CreateListOfSpawnPositions(movementPatternProperties, entityAmount);
            List<Vector2> despawnPositions;
            if (movementPatternProperties.ContainsKey("despawnXPosition") && movementPatternProperties.ContainsKey("despawnYPosition"))
            {
                despawnPositions = MovementPatternFactory.CreateListOfDespawnPositions(movementPatternProperties, entityAmount);
            }
            else
            {
                despawnPositions = spawnPositions;
            }

            return new EntityGroup(entityType, entityAmount, delaysTillSpawn, movementPatterns, spawnPositions, despawnPositions);
        }

        public static Timer CreateTimer(float timeInSeconds)
        {
            Timer timer;
            if (timeInSeconds > 0)
            {
                timer = new Timer(timeInSeconds * 1000);
                timer.Stop();
            }
            else
            {
                timer = new Timer(1);
            }

            return timer;
        }

        private static List<Timer> CreateListOfTimers(List<object> listOfTimes)
        {
            List<Timer> timers = new List<Timer>();
            listOfTimes.ForEach(item =>
            {
                Timer timer = CreateTimer((float)item);
                timers.Add(timer);
            });
            return timers;
        }
    }
}
