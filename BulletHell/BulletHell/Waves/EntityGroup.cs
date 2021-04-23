namespace BulletHell.Waves
{
    using System.Collections.Generic;
    using BulletHell.Sprites;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;

    internal class EntityGroup
    {
        private Entity entityType;
        private int entityAmount;
        private List<MovementPattern> movementPatterns;
        private List<Vector2> spawnPositions;
        private List<Vector2> despawnPositions;

        public EntityGroup(Entity entityType, int entityAmount, List<MovementPattern> movementPatterns, List<Vector2> spawnPositions, List<Vector2> despawnPositions = null)
        {
            this.entityType = entityType;
            this.entityAmount = entityAmount;
            this.movementPatterns = movementPatterns;
            this.spawnPositions = spawnPositions;
            this.despawnPositions = despawnPositions;
        }

        public void CreateEntities(List<Sprite> sprites)
        {
            for (int i = 0; i < this.entityAmount; i++)
            {
                Entity enemy = (Entity)this.entityType.Clone();
                enemy.SpawnPosition = this.spawnPositions[i];
                if (this.despawnPositions.Count == this.entityAmount)
                {
                    enemy.DespawnPosition = this.despawnPositions[i];
                }

                enemy.Movement = this.movementPatterns[i];
                enemy.Movement.Parent = enemy;

                sprites.Add(enemy);
            }
        }
    }
}
