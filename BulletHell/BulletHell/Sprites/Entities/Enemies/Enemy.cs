namespace BulletHell.Sprites.Entities.Enemies
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using global::BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Enemy : Entity
    {
        private double timer;

        public Enemy(Texture2D texture, Color color, MovementPattern movement, Projectile projectile, int lifeSpan)
            : base(texture, color, movement, projectile)
        {
            this.LifeSpan = lifeSpan;
            this.timer = 0;
        }

        protected double LifeSpan { get; set; }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer >= this.LifeSpan)
            {
                this.IsRemoved = true;
            }

            this.Collision(sprites);

            this.Movement.Move();
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
