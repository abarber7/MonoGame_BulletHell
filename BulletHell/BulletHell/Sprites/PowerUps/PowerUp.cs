using BulletHell.Sprites.Movement_Patterns;
using BulletHell.Sprites.The_Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.PowerUps
{
    internal abstract class PowerUp : Sprite, ICloneable
    {
        protected PowerUp(Texture2D texture, Color color, MovementPattern movement, int dropPercent)
            : base(texture, color, movement)
        {
            this.DropPercent = dropPercent;
            this.Movement.Parent = this;
        }

        public int DropPercent { get; set; }

        public override Rectangle Rectangle
        {
            get => new Rectangle(
                    new Point((int)this.Movement.Position.X - this.Texture.Width, (int)this.Movement.Position.Y - (int)Math.Round(this.Texture.Height * 2.5)),
                    new Point((int)Math.Round(this.Texture.Width * 2.5), (int)Math.Round(this.Texture.Height * 3.5)));
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            // Only care if sprite is player
            if (sprite is Player)
            {
                this.IsRemoved = true;
            }
        }

        public virtual void Move()
        {
            if (this.OutOfBounds())
            {
                this.IsRemoved = true;
            }

            this.Movement.Move();
        }

        public object Clone() => this.MemberwiseClone();

        public bool OutOfBounds()
        {
            if (this.Movement.IsTouchingLeftOfScreen() ||
                this.Movement.IsTouchingRightOfScreen() ||
                this.Movement.IsTouchingBottomOfScreen() ||
                this.Movement.IsTouchingTopOfScreen())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
