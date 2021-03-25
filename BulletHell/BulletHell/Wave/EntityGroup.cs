namespace BulletHell.Sprites
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Entities;
    using Microsoft.Xna.Framework;

    internal class EntityGroup
    {
        private Entity entityType;
        private int entityAmount;
        private List<Vector2> startingPositions;
        private List<Vector2> endingPositions;
        private List<Vector2> startPositions;

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
