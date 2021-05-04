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
        public SimpleGrunt(Texture2D texture, Color color, MovementPattern movement, PowerUp powerUp, int hp, List<Attack> attacks)
            : base(texture, color, movement, powerUp, hp, attacks)
        {
            this.points = 0.1;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
        }
    }
}
