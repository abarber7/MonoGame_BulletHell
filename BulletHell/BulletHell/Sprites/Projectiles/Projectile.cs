using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Projectiles
{
    class Projectile : Sprite, ICloneable
    {
        public Sprite parent;

        public Projectile(Dictionary<string, object> projectileProperties) : base(projectileProperties)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprits)
        {
            if (parent.isRemoved)
            {
                this.isRemoved = true;
            }
            timeAlive += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeAlive >= lifeSpan)
                this.isRemoved = true;

            position += velocity * 4;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
