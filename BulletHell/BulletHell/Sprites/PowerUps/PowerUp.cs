namespace BulletHell.Sprites.PowerUps
{
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class PowerUp : Projectile
    {
        public PowerUp(Texture2D texture, Color color, MovementPattern movement, int dropPercent)
            : base(texture, color, movement, 0) // projectile does 0 damage
        {
            this.DropPercent = dropPercent;
            this.Movement.Parent = this;
        }

        public double DropPercent { get; set; }

        public override Rectangle Rectangle
        {
            get => new Rectangle(
                    new Point((int)this.Movement.Position.X - (this.Texture.Width), (int)this.Movement.Position.Y - (this.Texture.Height)),
                    new Point(this.Texture.Width * 2, this.Texture.Height * 2));
        }

        public int DropPercent { get; set; }

        public override void OnCollision(Sprite sprite)
        {
            // Only care if sprite is player
            if (sprite is Player)
            {
                this.IsRemoved = true;
            }
        }
    }
}
