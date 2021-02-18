using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns
{
    abstract class MovementPattern : ICloneable
    {
        public Sprite parent;
        public Vector2 origin;
        public Vector2 position;
        public Vector2 velocity;
        public float speed;
        public float rotation;

        public MovementPattern(Dictionary<string, object> movementPatternProperties)
        {
            
        }

        virtual public void Move()
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool IsTouchingBottomOfScreen()
        {
            int bottom = BulletHell.getGraphics().PreferredBackBufferHeight - this.parent.texture.Height / 2;
            if (this.position.Y + this.velocity.Y > bottom)
            {
                this.position.Y = bottom;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingTopOfScreen()
        {
            int top = this.parent.texture.Height / 2;
            if (this.position.Y + this.velocity.Y < top)
            {
                this.position.Y = top;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingRightOfScreen()
        {
            int right = BulletHell.getGraphics().PreferredBackBufferWidth - this.parent.texture.Width / 2;
            if (this.position.X + this.velocity.X > right)
            {
                this.position.X = right;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsTouchingLeftOfScreen()
        {
            int left = this.parent.texture.Width / 2;
            if (this.position.X + this.velocity.X < left)
            {
                this.position.X = left;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
