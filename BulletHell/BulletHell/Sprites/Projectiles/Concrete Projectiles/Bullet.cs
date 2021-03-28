namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class Bullet : Projectile
    {
        public Bullet(Texture2D texture, Color color, MovementPattern movement)
            : base(texture, color, movement)
        {
        }
    }
}
