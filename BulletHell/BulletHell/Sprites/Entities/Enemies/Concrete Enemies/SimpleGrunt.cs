namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class SimpleGrunt : Enemy
    {
        private int previousTime = 0;
        private float timer2;

        public SimpleGrunt(Dictionary<string, object> simpleGruntProperties)
            : base(simpleGruntProperties)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
           /* if (this.previousTime != (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.Attack(sprites);
            }
           */

            this.timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer2 > 1f)
            {
                this.timer2 = 0;
                this.Attack(sprites);
            }

            ///this.previousTime = (int)gameTime.TotalGameTime.TotalSeconds;
            base.Update(gameTime, sprites);
        }
    }
}
