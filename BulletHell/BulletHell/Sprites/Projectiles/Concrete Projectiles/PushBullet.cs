namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class PushBullet : Projectile
    {
        public PushBullet(Texture2D texture, Color color, MovementPattern movement, int damage)
            : base(texture, color, movement, damage)
        {
        }

        public override void OnCollision(Sprite sprite)
        {
            if (sprite is Projectile projectile && !(projectile is PushBullet))
            {
                projectile.Movement.Velocity = this.Movement.Velocity;
            }
            else
            {
                base.OnCollision(sprite);
            }
        }
    }
}
