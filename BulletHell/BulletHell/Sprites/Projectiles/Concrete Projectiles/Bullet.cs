namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class Bullet : Projectile
    {
        public Bullet(Texture2D texture, Color color, MovementPattern movement)
            : base(texture, color, movement)
        {
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            base.Update(gameTime, sprites);
        }
    }
}
