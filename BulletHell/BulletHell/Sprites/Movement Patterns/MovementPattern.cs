namespace BulletHell.Sprites.Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal abstract class MovementPattern : ICloneable
    {
        public Sprite Parent;
        public Vector2 Origin;
        public Vector2 Position;
        public Vector2 velocity;
        public float Speed;
        public float Rotation;

        public MovementPattern(Dictionary<string, object> movementPatternProperties)
        {
        }

        public virtual void Move()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool IsTouchingBottomOfScreen()
        {
            int bottom = BulletHell.Graphics.PreferredBackBufferHeight - (this.Parent.Texture.Height / 2);
            if (this.Position.Y + this.velocity.Y > bottom)
            {
                this.Position.Y = bottom;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingTopOfScreen()
        {
            int top = this.Parent.Texture.Height / 2;
            if (this.Position.Y + this.velocity.Y < top)
            {
                this.Position.Y = top;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingRightOfScreen()
        {
            int right = BulletHell.Graphics.PreferredBackBufferWidth - (this.Parent.Texture.Width / 2);
            if (this.Position.X + this.velocity.X > right)
            {
                this.Position.X = right;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingLeftOfScreen()
        {
            int left = this.Parent.Texture.Width / 2;
            if (this.Position.X + this.velocity.X < left)
            {
                this.Position.X = left;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
