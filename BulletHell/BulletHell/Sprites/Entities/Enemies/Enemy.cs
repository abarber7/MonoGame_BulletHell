using BulletHell.Sprites.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities.Enemies
{
    abstract class Enemy : Entity
    {
        

        public Enemy(Dictionary<string, object> enemyProperties) : base(enemyProperties)
        {

        }

        protected void Collision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }

                if(sprite is Projectile projectile)
                {
                    if (projectile.parent is Player && (this.IsTouchingLeftSideOfSprite(sprite) || this.IsTouchingRightSideOfSprite(sprite) || this.IsTouchingTopSideOfSprite(sprite) || this.IsTouchingBottomSideOfSprite(sprite)))
                    {
                        this.isRemoved = true;
                        sprite.isRemoved = true;
                    }
                }    
            }
        }
    }
}
