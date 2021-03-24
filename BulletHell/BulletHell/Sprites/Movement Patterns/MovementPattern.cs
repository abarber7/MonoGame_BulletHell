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
        public int Speed;
        public int Rotation;

        public MovementPattern(Dictionary<string, object> movementPatternProperties)
        {
        }

        public virtual void Move()
        {
            this.Position += this.velocity;
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

        protected Vector2 CalculateVelocity(Vector2 startPosition, Vector2 endPosition, int speed)
        {
            Vector2 velocity = new Vector2();
            velocity.X = (endPosition.X - startPosition.X) == 0 ? 0 : speed / (endPosition.X - startPosition.X);
            velocity.Y = (endPosition.Y - startPosition.Y) == 0 ? 0 : speed / (endPosition.Y - startPosition.Y);

            return velocity;
        }

        protected bool ExceededPosition(Vector2 startPosition, Vector2 endPosition, Vector2 velocity)
        {
            // Up
            if (velocity.Y < 0)
            {
                // Up to start
                if ((this.Position.Y + velocity.Y - startPosition.Y < this.Position.Y - startPosition.Y)
                    && this.Position.Y < startPosition.Y
                    && startPosition.Y - endPosition.Y < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.Position.Y + velocity.Y - endPosition.Y < this.Position.Y - endPosition.Y)
                    && this.Position.Y < endPosition.Y
                    && endPosition.Y - startPosition.Y < 0)
                {
                    return true;
                }
            }

            // Down
            else if (velocity.Y > 0)
            {
                // Down to finish
                if ((endPosition.Y - this.Position.Y + velocity.Y > endPosition.Y - this.Position.Y)
                    && this.Position.Y > endPosition.Y
                    && endPosition.Y - startPosition.Y > 0)
                {
                    return true;
                }

                // Down to Start
                if ((startPosition.Y - this.Position.Y + velocity.Y > startPosition.Y - this.Position.Y)
                    && this.Position.Y > startPosition.Y
                    && endPosition.Y - startPosition.Y < 0)
                {
                    return true;
                }
            }

            // Left
            else if (velocity.X < 0)
            {
                // Up to start
                if ((this.Position.X + this.velocity.X - startPosition.X < this.Position.X - startPosition.X)
                    && this.Position.X < startPosition.X
                    && startPosition.X - endPosition.X < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.Position.X + velocity.X - endPosition.X < this.Position.X - endPosition.X)
                    && this.Position.X < endPosition.X
                    && endPosition.X - startPosition.X < 0)
                {
                    return true;
                }
            }

            // Right
            else if (velocity.X > 0)
            {
                // Right to finish
                if ((endPosition.X - this.Position.X + velocity.X > endPosition.X - this.Position.X)
                    && this.Position.X > endPosition.X
                    && endPosition.X - startPosition.X > 0)
                {
                    return true;
                }

                // Right to Start
                if ((startPosition.X - this.Position.X + velocity.X > startPosition.X - this.Position.X)
                    && this.Position.X > startPosition.X
                    && endPosition.X - startPosition.X < 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
