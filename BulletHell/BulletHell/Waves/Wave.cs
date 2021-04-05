namespace BulletHell.Waves
{
    using System.Collections.Generic;
    using BulletHell.Sprites;

    internal class Wave
    {
        public int WaveNumber;
        public int WaveDuration;
        private List<EntityGroup> entityGroups = new List<EntityGroup>();

        public Wave(Dictionary<string, object> waveProperties)
        {
            this.WaveNumber = (int)waveProperties["waveNumber"];
            this.WaveDuration = (int)waveProperties["waveDuration"];

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
