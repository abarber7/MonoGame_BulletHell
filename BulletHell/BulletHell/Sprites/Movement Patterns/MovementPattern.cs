namespace BulletHell.Sprites.Movement_Patterns
{
    using System;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;

    internal abstract class MovementPattern : ICloneable
    {
        protected Vector2 velocity;
        protected Vector2 position;

        public MovementPattern()
        {
        }

        public Sprite Parent { get; set; }

        public Vector2 Velocity { get => this.velocity; set => this.velocity = value; }

        public Vector2 Position { get => this.position; set => this.position = value; }

        public Vector2 Origin { get; set; }

        public int Speed { get; set; }

        public int CurrentSpeed { get; set; }

        public int Rotation { get; set; }

        public bool reachedStart = false; // bool for if entity reached start position
        public bool exitTime = false; // bool for if it is time to exit

        public virtual void Move()
        {
            this.Position += this.Velocity;
        }

        public object Clone() => this.MemberwiseClone();

        public bool IsTouchingBottomOfScreen()
        {
            int bottom = GraphicManagers.GraphicsDeviceManager.PreferredBackBufferHeight - (this.Parent.Texture.Height / 2);
            if (this.Position.Y + this.Velocity.Y > bottom)
            {
                Vector2 position = this.Position;
                position.Y = bottom;
                this.Position = position;
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
            if (this.Position.Y + this.Velocity.Y < top)
            {
                Vector2 position = this.Position;
                position.Y = top;
                this.Position = position;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingRightOfScreen()
        {
            int right = GraphicManagers.GraphicsDeviceManager.PreferredBackBufferWidth - (this.Parent.Texture.Width / 2);
            if (this.Position.X + this.Velocity.X > right)
            {
                Vector2 position = this.Position;
                position.X = right;
                this.Position = position;
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
            if (this.Position.X + this.Velocity.X < left)
            {
                Vector2 position = this.Position;
                position.X = left;
                this.Position = position;
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

        public Vector2 CalculateVelocity(Vector2 startPosition, Vector2 endPosition, int speed)
        {
            Vector2 velocity;
            velocity.X = endPosition.X - startPosition.X;
            velocity.Y = endPosition.Y - startPosition.Y;
            velocity.Normalize();
            velocity.X *= speed;
            velocity.Y *= speed;

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
                if ((this.Position.X + this.Velocity.X - startPosition.X < this.Position.X - startPosition.X)
                    && this.Position.X < startPosition.X
                    && startPosition.X - endPosition.X < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.Position.X + this.Velocity.X - endPosition.X < this.Position.X - endPosition.X)
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
