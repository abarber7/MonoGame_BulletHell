namespace BulletHell.Waves
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;

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
                Entity entity = (Entity)this.entityType.Clone();
                entity.Movement = this.movementPatterns[i];
                entity.Projectile.Movement.Parent = entity;
                entity.Movement.Parent = entity;
                sprites.Add(entity);
            }
        }
    }
}
