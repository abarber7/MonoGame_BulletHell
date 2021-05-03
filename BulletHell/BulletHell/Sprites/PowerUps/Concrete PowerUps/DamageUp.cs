namespace BulletHell.Sprites.PowerUps.Concrete_PowerUps
{
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class DamageUp : PowerUp
    {
        public DamageUp(Texture2D texture, Color color, MovementPattern movement, float dropPercent)
            : base(texture, color, movement, dropPercent)
        {
        }
    }
}
