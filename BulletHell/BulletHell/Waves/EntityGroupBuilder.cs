namespace BulletHell.Waves
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.Sprites.Movement_Patterns;

    internal class EntityGroupBuilder
    {
        public static EntityGroup CreateEntityGroup(Dictionary<string, object> entityGroupProperties)
        {
            Entity entityType = EntityFactory.CreateEntity((Dictionary<string, object>)entityGroupProperties["entityProperties"]);
            int entityAmount = (int)entityGroupProperties["entityAmount"];
            List<MovementPattern> movementPatterns;
            if (entityGroupProperties.ContainsKey("movementPattern"))
            {
                movementPatterns = MovementPatternFactory.CreateListOfMovementPatterns((Dictionary<string, object>)entityGroupProperties["movementPattern"], entityAmount);
            }
            else
            {
                movementPatterns = new List<MovementPattern>();
            }

            return new EntityGroup(entityType, entityAmount, movementPatterns);
        }
    }
}
