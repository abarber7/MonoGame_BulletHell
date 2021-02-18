namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ComplexGrunt : Enemy
    {
        public ComplexGrunt(Dictionary<string, object> complexGruntProperties)
            : base(complexGruntProperties)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
        }
    }
}
