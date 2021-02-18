namespace BulletHell.Sprites.Entities.Enemies
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Projectiles;

    internal abstract class Enemy : Entity
    {
        public Enemy(Dictionary<string, object> enemyProperties)
            : base(enemyProperties)
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

                if (sprite is Projectile projectile)
                {
                    if (projectile.Parent is Player && (this.IsTouchingLeftSideOfSprite(sprite) || this.IsTouchingRightSideOfSprite(sprite) || this.IsTouchingTopSideOfSprite(sprite) || this.IsTouchingBottomSideOfSprite(sprite)))
                    {
                        this.IsRemoved = true;
                        sprite.IsRemoved = true;
                    }
                }
            }
        }
    }
}
