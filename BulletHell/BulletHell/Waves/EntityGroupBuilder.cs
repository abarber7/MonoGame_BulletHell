namespace BulletHell.Waves
{
    using System;
    using System.Collections.Generic;
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

            return new EntityGroup(entityType, entityAmount, movementPatterns, spawnPositions, despawnPositions);
        }
    }
}
