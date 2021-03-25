namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using global::BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class SimpleGrunt : Enemy
    {
        private int previousTime = 0;
        private float timer2;

        public SimpleGrunt(Texture2D texture, Color color, MovementPattern movement, Projectile projectile, int lifeSpan)
            : base(texture, color, movement, projectile, lifeSpan)
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
