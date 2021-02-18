namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class MidBoss : Enemy
    {
        public MidBoss(Dictionary<string, object> midBossProperties)
            : base (midBossProperties)
        {
        }

        public void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }
    }
}
