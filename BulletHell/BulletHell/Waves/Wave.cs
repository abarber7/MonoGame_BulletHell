namespace BulletHell.Waves
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites;

    internal class Wave
    {
        public int WaveNumber;
        public float WaveDuration;
        private List<EntityGroup> entityGroups = new List<EntityGroup>();

        public Wave(Dictionary<string, object> waveProperties)
        {
            this.WaveNumber = Convert.ToInt32((float)waveProperties["waveNumber"]);
            this.WaveDuration = (float)waveProperties["waveDuration"];

            foreach (object entityGroupProperties in (List<object>)waveProperties["entityGroups"])
            {
                this.entityGroups.Add(EntityGroupBuilder.CreateEntityGroup((Dictionary<string, object>)entityGroupProperties));
            }
        }

        public void CreateWave(List<Sprite> sprites)
        {
            foreach (EntityGroup entityGroup in this.entityGroups)
            {
                entityGroup.CreateEntities(sprites);
            }
        }
    }
}
