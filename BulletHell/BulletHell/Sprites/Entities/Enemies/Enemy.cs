namespace BulletHell.Sprites.Entities.Enemies
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Enemy : Entity
    {
        private double timer;

        public Enemy(Texture2D texture, Color color, MovementPattern movement, Projectile projectile, int lifeSpan, int hp = 10)
            : base(texture, color, movement, projectile)
        {
            this.LifeSpan = lifeSpan;
            this.HealthPoints = hp;
            this.timer = 0;
        }

        protected double LifeSpan { get; set; }

        protected int HealthPoints { get; set; }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            if (this.timer >= this.LifeSpan)
            {
                this.IsRemoved = true;
            }

            this.Movement.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            if (sprite is Projectile projectile)
            {
                // Ignore projectiles from fellow enemies/self
                if (projectile.Parent is Player)
                {
                    this.HealthPoints -= projectile.Damage;
                    if (this.HealthPoints <= 0)
                    {
                        this.IsRemoved = true;
                    }
                }
            }
            else if (sprite is Player)
            {
                this.IsRemoved = true;
            }
        }
    }
}
