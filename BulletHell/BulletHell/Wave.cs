using BulletHell.Sprites.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites
{
    class EntityGroup
    {
        Entity entityType;
        int entityAmount;

        public EntityGroup(Dictionary<string, object> entityGroupProperties)
        {
            entityType = EntityFactory.createEntity((Dictionary<string, object>)entityGroupProperties["entityProperties"]);
            entityAmount = (int)entityGroupProperties["entityAmount"];
        }

        public void CreateEntities(List<Sprite> sprites)
        {
            for(int i = 0; i < entityAmount; i++)
            {
                sprites.Add(entityType);
            }
        }
    }
    
    class Wave
    {
        int waveNumber;
        int waveDuration;
        List<EntityGroup> entityGroups = new List<EntityGroup>();

        public Wave(Dictionary<string, object> waveProperties)
        {
            waveNumber = (int)waveProperties["waveNumber"];
            waveDuration = (int)waveProperties["waveDuration"];

            foreach(Dictionary<string, object> entityGroupProperties in (List<Dictionary<string, object>>)waveProperties["entityGroups"])
            {
                entityGroups.Add(new EntityGroup(entityGroupProperties));
            }
        }

        public void CreateWave(List<Sprite> sprites)
        {
            foreach (EntityGroup entityGroup in entityGroups)
            {
                entityGroup.CreateEntities(sprites);
            }
        }
    }
}
