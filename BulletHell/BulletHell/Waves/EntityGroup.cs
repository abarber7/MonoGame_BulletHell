namespace BulletHell.Waves
{
    using System.Collections.Generic;
    using BulletHell.Sprites;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Movement_Patterns;

    internal class EntityGroup
    {
        private Entity entityType;
        private int entityAmount;
        private List<MovementPattern> movementPatterns;

        public EntityGroup(Entity entityType, int entityAmount, List<MovementPattern> movementPatterns)
        {
            this.entityType = entityType;
            this.entityAmount = entityAmount;
            this.movementPatterns = movementPatterns;
        }

        public void CreateEntities(List<Sprite> sprites)
        {
            for (int i = 0; i < this.entityAmount; i++)
            {
                Entity enemy = (Entity)this.entityType.Clone();

                if (this.entityAmount > 1)
                {
                    enemy.Movement = this.movementPatterns[i];
                    enemy.Projectile.Movement.Parent = enemy;
                    enemy.Movement.Parent = enemy;
                }

                sprites.Add(enemy);
            }
        }
    }
}
