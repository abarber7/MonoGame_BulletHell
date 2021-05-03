namespace BulletHell.Sprites.Projectiles.Concrete_Projectiles
{
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class Bullet : Projectile
    {
        public Bullet(Texture2D texture, Color color, MovementPattern movement, int damage)
            : base(texture, color, movement, damage)
        {
            this.textureScale = 0.5F;
        }
    }
}
