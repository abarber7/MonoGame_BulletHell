namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ExampleEnemy : Enemy
    {
        private int previousTime = 0;

        public ExampleEnemy(Texture2D texture, Color color, MovementPattern movement, Projectile projectile, PowerUp powerUp, int lifeSpan)
            : base(texture, color, movement, projectile, powerUp, lifeSpan)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
            if (this.previousTime != (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.Attack(sprites);
            }

            this.previousTime = (int)gameTime.TotalGameTime.TotalSeconds;
        }
    }
}
