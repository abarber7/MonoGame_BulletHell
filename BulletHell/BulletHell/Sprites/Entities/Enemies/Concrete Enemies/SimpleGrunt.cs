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
        public SimpleGrunt(Texture2D texture, Color color, MovementPattern movement, PowerUp powerUp, int lifeSpan, int hp, Attack attack, float cooldownToAttack)
            : base(texture, color, movement, powerUp, lifeSpan, hp, attack, cooldownToAttack)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);

            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer > this.attackCooldown)
            {
                this.timer = 0;
                this.ExecuteAttack(sprites);
            }
        }
    }
}
