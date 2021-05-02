namespace BulletHell.Sprites.Movement_Patterns
{
    using System;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;

    public abstract class MovementPattern : ICloneable
    {
        public bool CompletedMovement = false;
        public Vector2 CurrentPosition;
        protected Vector2 velocity;
        protected Vector2 startPosition;

        public MovementPattern(Vector2 startPosition, float speed)
        {
            this.startPosition = startPosition;
            this.Speed = speed;
        }

        public Sprite Parent { get; set; }

        public Vector2 Velocity { get => this.velocity; set => this.velocity = value; }

        public Vector2 StartPosition { get => this.startPosition; }

        public float Speed { get; set; }

        public float CurrentSpeed { get; set; }

        // Specifies the rotation axis of sprite, relative to the drawing
        // bounds and based on sprite texture - only needed when using Draw method
        public Vector2 Origin { get; set; }

        // For rotating sprites when drawing; should be done in radians
        public int Rotation { get; set; }

        public static Vector2 CalculateVelocity(Vector2 startPosition, Vector2 endPosition, float speed)
        {
            Vector2 velocity;
            velocity.X = endPosition.X - startPosition.X;
            velocity.Y = endPosition.Y - startPosition.Y;
            velocity.Normalize();
            velocity.X *= speed;
            velocity.Y *= speed;

            return velocity;
        }

        public virtual void InitializeMovement()
        {
        }

        public virtual void Move()
        {
            this.CurrentPosition += this.Velocity;
        }

        public object Clone() => this.MemberwiseClone();

        public bool IsTouchingBottomOfScreen()
        {
            float bottom = GraphicManagers.GraphicsDeviceManager.PreferredBackBufferHeight - (this.Parent.Rectangle.Height / 2F);
            if (this.Parent.Rectangle.Bottom + this.Velocity.Y > bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingTopOfScreen()
        {
            float top = this.Parent.Rectangle.Height / 2F;
            if (this.Parent.Rectangle.Top + this.Velocity.Y < top)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingRightOfScreen()
        {
            int right = GraphicManagers.GraphicsDeviceManager.PreferredBackBufferWidth - (this.Parent.TextureWidth / 2);
            if (this.CurrentPosition.X + this.Velocity.X > right)
            {
                Vector2 position = this.CurrentPosition;
                position.X = right;
                this.CurrentPosition = position;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingLeftOfScreen()
        {
            int left = this.Parent.TextureWidth / 2;
            if (this.CurrentPosition.X + this.Velocity.X < left)
            {
                Vector2 position = this.CurrentPosition;
                position.X = left;
                this.CurrentPosition = position;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ZeroXVelocity() => this.velocity.X = 0;

        public void ZeroYVelocity() => this.velocity.Y = 0;

        public void InvertXVelocity() => this.velocity.X = -this.velocity.X;

        public void InvertYVelocity() => this.velocity.Y = -this.velocity.Y;

        public bool ExceededPosition(Vector2 startPosition, Vector2 endPosition, Vector2 velocity)
        {
            // Up
            if (velocity.Y < 0)
            {
                // Up to start
                if ((this.CurrentPosition.Y + velocity.Y - startPosition.Y < this.CurrentPosition.Y - startPosition.Y)
                    && this.CurrentPosition.Y < startPosition.Y
                    && startPosition.Y - endPosition.Y < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.CurrentPosition.Y + velocity.Y - endPosition.Y < this.CurrentPosition.Y - endPosition.Y)
                    && this.CurrentPosition.Y < endPosition.Y
                    && endPosition.Y - startPosition.Y < 0)
                {
                    return true;
                }
            }

            // Down
            else if (velocity.Y > 0)
            {
                // Down to finish
                if ((endPosition.Y - this.CurrentPosition.Y + velocity.Y > endPosition.Y - this.CurrentPosition.Y)
                    && this.CurrentPosition.Y > endPosition.Y
                    && endPosition.Y - startPosition.Y > 0)
                {
                    return true;
                }

                // Down to Start
                if ((startPosition.Y - this.CurrentPosition.Y + velocity.Y > startPosition.Y - this.CurrentPosition.Y)
                    && this.CurrentPosition.Y > startPosition.Y
                    && endPosition.Y - startPosition.Y < 0)
                {
                    return true;
                }
            }

            // Left
            else if (velocity.X < 0)
            {
                // Up to start
                if ((this.CurrentPosition.X + this.Velocity.X - startPosition.X < this.CurrentPosition.X - startPosition.X)
                    && this.CurrentPosition.X < startPosition.X
                    && startPosition.X - endPosition.X < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.CurrentPosition.X + this.Velocity.X - endPosition.X < this.CurrentPosition.X - endPosition.X)
                    && this.CurrentPosition.X < endPosition.X
                    && endPosition.X - startPosition.X < 0)
                {
                    return true;
                }
            }

            // Right
            else if (velocity.X > 0)
            {
                // Right to finish
                if ((endPosition.X - this.CurrentPosition.X + velocity.X > endPosition.X - this.CurrentPosition.X)
                    && this.CurrentPosition.X > endPosition.X
                    && endPosition.X - startPosition.X > 0)
                {
                    return true;
                }

                // Right to Start
                if ((startPosition.X - this.CurrentPosition.X + velocity.X > startPosition.X - this.CurrentPosition.X)
                    && this.CurrentPosition.X > startPosition.X
                    && endPosition.X - startPosition.X < 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
