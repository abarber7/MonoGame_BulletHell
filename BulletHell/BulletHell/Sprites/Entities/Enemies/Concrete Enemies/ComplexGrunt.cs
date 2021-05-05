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
        public ComplexGrunt(Texture2D texture, Color color, MovementPattern movement, PowerUp powerUp, int hp, List<Attack> attack)
            : base(texture, color, movement, powerUp, hp, attack)
        {
            this.points = 0.5;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
        }
    }
}
