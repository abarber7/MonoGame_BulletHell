namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class FinalBoss : Enemy
    {
        public FinalBoss(Dictionary<string, object> finalBossProperties)
            : base(finalBossProperties)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }
    }
}
