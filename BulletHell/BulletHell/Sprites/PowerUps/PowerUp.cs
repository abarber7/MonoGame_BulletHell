namespace BulletHell.Sprites.PowerUps
{
    using System;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class PowerUp : Projectile
    {
        public PowerUp(Texture2D texture, Color color, MovementPattern movement, float dropPercent)
            : base(texture, color, movement, 0) // projectile does 0 damage
        {
            this.DropPercent = dropPercent;
            this.Movement.Parent = this;
        }

        public float DropPercent { get; set; }

        public override Rectangle Rectangle
        {
            get => new Rectangle(
                    new Point(Convert.ToInt32(this.Movement.CurrentPosition.X - (1.5 * this.TextureWidth)), Convert.ToInt32(this.Movement.CurrentPosition.Y - (1.5 * this.TextureHeight))),
                    new Point(this.TextureWidth * 2, this.TextureHeight * 2));
        }

        public override void OnCollision(Sprite sprite)
        {
            // Only care if sprite is player
            if (sprite is Player)
            {
                this.IsRemoved = true;
            }
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
