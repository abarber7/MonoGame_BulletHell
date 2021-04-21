namespace BulletHell.Sprites.Entities.Enemies
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Enemy : Entity
    {
        private double timer;

        public Enemy(Texture2D texture, Color color, MovementPattern movement, Projectile projectile, PowerUp powerUp, int lifeSpan, int hp = 10)
            : base(texture, color, movement, projectile)
        {
            this.LifeSpan = lifeSpan;
            this.HealthPoints = hp;
            this.timer = 0;
            this.DropLoot = false;
            this.PowerUp = powerUp;
        }

        public bool DropLoot { get; set; }

        protected double LifeSpan { get; set; }

        protected int HealthPoints { get; set; }

        // public because GameState looks at a Sprite version of the enemy?
        protected PowerUp PowerUp { get; set; }

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
                        Random rnd = new Random();
                        if (rnd.Next(1, 101) <= this.PowerUp.DropPercent)
                        {
                            this.DropLoot = true; // random <= to powerUp field's  dropPercent here, if so dropLoot is set to True, GameState checks and drops if so. i.e. adds this enemies PowerUp powerUp to the enemies sprite list in GameState.. Alex
                        }
                    }
                }
            }
            else if (sprite is Player)
            {
                this.IsRemoved = true;
            }
        }

        public PowerUp GetLoot()
        {
            PowerUp powerUp = this.PowerUp.Clone() as PowerUp;
            powerUp.Movement = this.PowerUp.Movement.Clone() as MovementPattern;
            Vector2 velocity = powerUp.Movement.Velocity;
            velocity.Normalize();
            velocity.X *= powerUp.Movement.Speed;
            velocity.Y *= powerUp.Movement.Speed;
            powerUp.Movement.Velocity = velocity;
            Random random = new Random();
            powerUp.Movement.Position = new Vector2(random.Next(100, 700), 70); // Spawn origin x-coordinate randomized in center portion of screen
            return powerUp;
        }
    }
}
