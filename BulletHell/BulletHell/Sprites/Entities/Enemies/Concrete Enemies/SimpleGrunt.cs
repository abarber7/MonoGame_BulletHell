namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class SimpleGrunt : Enemy
    {
        public SimpleGrunt(Dictionary<string, object> simpleGruntProperties)
            : base(simpleGruntProperties)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }
    }
}
