namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using global::BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ComplexGrunt : Enemy
    {
        private float timer1;
        private float timer2;
        private float timer3;

        public ComplexGrunt(Texture2D texture, Color color, MovementPattern movement, Projectile projectile, int lifeSpan)
            : base(texture, color, movement, projectile, lifeSpan)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.timer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.timer3 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer1 > .8f)
            {
                this.timer1 = 0;
                this.Attack(sprites);
            }

            if (this.timer2 > .9f)
            {
                this.timer1 = 0;
                this.timer2 = 0;
                this.Attack(sprites);
            }

            if (this.timer3 > 1f)
            {
                this.timer1 = 0;
                this.timer2 = 0;
                this.timer3 = 0;
                this.Attack(sprites);
            }

            base.Update(gameTime, sprites);
        }
    }
}
