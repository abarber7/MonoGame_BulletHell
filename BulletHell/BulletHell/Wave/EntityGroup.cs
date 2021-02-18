namespace BulletHell.Sprites
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Entities;

    internal class EntityGroup
    {
        Entity entityType;
        int entityAmount;

        public EntityGroup(Dictionary<string, object> entityGroupProperties)
        {
            this.entityType = EntityFactory.CreateEntity((Dictionary<string, object>)entityGroupProperties["entityProperties"]);
            this.entityAmount = (int)entityGroupProperties["entityAmount"];
        }

        public void CreateEntities(List<Sprite> sprites)
        {
            for (int i = 0; i < this.entityAmount; i++)
            {
                sprites.Add(this.entityType);
            }
        }
    }
}
