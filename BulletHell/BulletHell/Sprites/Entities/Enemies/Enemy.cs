namespace BulletHell.Sprites.Entities.Enemies
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    internal abstract class Enemy : Entity
    {
        public Enemy(Dictionary<string, object> enemyProperties)
            : base(enemyProperties)
        {
            this.LifeSpan = (int)enemyProperties["lifeSpan"];
            this.Timer = 0;
        }

        protected int LifeSpan { get; set; }

        protected int Timer { get; set; }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.Timer += (int)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.Timer >= this.LifeSpan)
            {
                this.IsRemoved = true;
            }
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
                    if (projectile.Parent is Player
                        && (this.IsTouchingLeftSideOfSprite(sprite) || this.IsTouchingRightSideOfSprite(sprite) || this.IsTouchingTopSideOfSprite(sprite) || this.IsTouchingBottomSideOfSprite(sprite)))
                    {
                        this.IsRemoved = true;
                        sprite.IsRemoved = true;
                    }
                }
            }
        }
    }
}
