using BulletHell.Sprites.Movement_Patterns;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Projectiles
{
    abstract class Projectile : Sprite, ICloneable
    {
        public Sprite parent;

        public Projectile(Dictionary<string, object> projectileProperties) : base(projectileProperties)
        {
            
        }

        public override void Update(GameTime gameTime, List<Sprite> sprits)
        {
            this.Move();
        }

        public void Move()
        {
            if (this.outOfBounds())
            {
                this.isRemoved = true;
            }
            this.movement.Move();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool outOfBounds()
        {
            if (this.movement.IsTouchingLeftOfScreen() || this.movement.IsTouchingRightOfScreen() || this.movement.IsTouchingBottomOfScreen() || this.movement.IsTouchingTopOfScreen())
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
