namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class ExampleEnemy : Enemy
    {
        private int previousTime = 0;

        public ExampleEnemy(Dictionary<string, object> exampleGruntProperties)
            : base(exampleGruntProperties)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (this.previousTime != (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.Attack(sprites);
            }

            this.previousTime = (int)gameTime.TotalGameTime.TotalSeconds;

            this.Collision(sprites);

            // this.Move(sprites);
        }
    }
}
