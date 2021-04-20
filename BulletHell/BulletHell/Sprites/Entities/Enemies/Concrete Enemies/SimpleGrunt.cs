namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class SimpleGrunt : Enemy
    {
        private new double timer = 0;
        private double previousTimer = 0;

        public SimpleGrunt(Texture2D texture, Color color, MovementPattern movement, Attack attack, PowerUp powerUp, int lifeSpan)
            : base(texture, color, movement, attack, powerUp, lifeSpan)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);

            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer > 5)
            {
                this.timer = 0;
                this.Attack(sprites);
            }

            this.previousTimer = gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
