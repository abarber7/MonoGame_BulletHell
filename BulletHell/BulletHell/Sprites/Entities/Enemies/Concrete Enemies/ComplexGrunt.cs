namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ComplexGrunt : Enemy
    {
        public ComplexGrunt(Texture2D texture, Color color, MovementPattern movement, PowerUp powerUp, int lifeSpan, int hp, Attack attack, float attackCooldown)
            : base(texture, color, movement, powerUp, lifeSpan, hp, attack, attackCooldown)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);

            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer > this.attackCooldown)
            {
                this.timer = 0;
                this.Attack(sprites);
            }
        }
    }
}
