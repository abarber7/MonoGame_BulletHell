namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 startPosition;
        private Vector2 endPosition;

        public BackAndForth(Dictionary<string, object> backAndForthProperties)
            : base(backAndForthProperties)
        {
            this.Speed = Convert.ToSingle((int)backAndForthProperties["speed"]);
            this.startPosition.X = Convert.ToSingle((int)backAndForthProperties["xStartPosition"]);
            this.startPosition.Y = Convert.ToSingle((int)backAndForthProperties["yStartPosition"]);
            this.endPosition.X = Convert.ToSingle((int)backAndForthProperties["xEndPosition"]);
            this.endPosition.Y = Convert.ToSingle((int)backAndForthProperties["yEndPosition"]);
            this.Position = this.startPosition;
            this.velocity.X = (this.endPosition.X - this.startPosition.X) == 0 ? 0 : this.Speed / (this.endPosition.X - this.startPosition.X);
            this.velocity.Y = (this.endPosition.Y - this.startPosition.Y) == 0 ? 0 : this.Speed / (this.endPosition.Y - this.startPosition.Y);
        }

        public override void Move()
        {
            if (this.ExceededPosition())
            {
                this.velocity = -this.velocity;
            }

            this.Position += this.velocity;
        }

        private bool ExceededPosition()
        {
            // Up
            if (this.velocity.Y < 0)
            {
                // Up to start
                if ((this.Position.Y + this.velocity.Y - this.startPosition.Y < this.Position.Y - this.startPosition.Y)
                    && this.Position.Y < this.startPosition.Y
                    && this.startPosition.Y - this.endPosition.Y < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.Position.Y + this.velocity.Y - this.endPosition.Y < this.Position.Y - this.endPosition.Y)
                    && this.Position.Y < this.endPosition.Y
                    && this.endPosition.Y - this.startPosition.Y < 0)
                {
                    return true;
                }
            }

            // Down
            else if (this.velocity.Y > 0)
            {
                // Down to finish
                if ((this.endPosition.Y - this.Position.Y + this.velocity.Y > this.endPosition.Y - this.Position.Y)
                    && this.Position.Y > this.endPosition.Y
                    && this.endPosition.Y - this.startPosition.Y > 0)
                {
                    return true;
                }

                // Down to Start
                if ((this.startPosition.Y - this.Position.Y + this.velocity.Y > this.startPosition.Y - this.Position.Y)
                    && this.Position.Y > this.startPosition.Y
                    && this.endPosition.Y - this.startPosition.Y < 0)
                {
                    return true;
                }
            }

            // Left
            else if (this.velocity.X < 0)
            {
                // Up to start
                if ((this.Position.X + this.velocity.X - this.startPosition.X < this.Position.X - this.startPosition.X)
                    && this.Position.X < this.startPosition.X
                    && this.startPosition.X - this.endPosition.X < 0)
                {
                    return true;
                }

                // Up to end
                if ((this.Position.X + this.velocity.X - this.endPosition.X < this.Position.X - this.endPosition.X)
                    && this.Position.X < this.endPosition.X
                    && this.endPosition.X - this.startPosition.X < 0)
                {
                    return true;
                }
            }

            // Right
            else if (this.velocity.X > 0)
            {
                // Right to finish
                if ((this.endPosition.X - this.Position.X + this.velocity.X > this.endPosition.X - this.Position.X)
                    && this.Position.X > this.endPosition.X
                    && this.endPosition.X - this.startPosition.X > 0)
                {
                    return true;
                }

                // Right to Start
                if ((this.startPosition.X - this.Position.X + this.velocity.X > this.startPosition.X - this.Position.X)
                    && this.Position.X > this.startPosition.X
                    && this.endPosition.X - this.startPosition.X < 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
