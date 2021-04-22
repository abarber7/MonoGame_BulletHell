namespace BulletHell.Waves
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Game_Utilities;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Movement_Patterns;

    internal class EntityGroupBuilder
    {
        public static EntityGroup CreateEntityGroup(Dictionary<string, object> entityGroupProperties)
        {
            Entity entityType = GameLoader.GetEnemy((string)entityGroupProperties["entityType"]);
            int entityAmount = Convert.ToInt32((double)entityGroupProperties["entityAmount"]);
            List<MovementPattern> movementPatterns = MovementPatternFactory.CreateListOfMovementPatterns((Dictionary<string, object>)entityGroupProperties["movementPattern"], entityAmount);

            return new EntityGroup(entityType, entityAmount, movementPatterns);
        }
    }
}
