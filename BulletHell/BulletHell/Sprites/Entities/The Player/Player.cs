namespace BulletHell.Sprites.The_Player
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.PowerUps.Concrete_PowerUps;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.The_Player;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    internal class Player : Entity
    {
        public bool SlowMode;
        public bool Invincible;
        private double initialSpawnTime;
        private bool spawning;
        private bool resetGameTime = true;
        private int damageLevel;

        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public Player(Texture2D texture, Color color, MovementPattern movement, int hp, Attack attack, float cooldownToAttack)
            : base(texture, color, movement, hp, attack, cooldownToAttack)
        {
            this.spawning = true;
            this.Invincible = true;
            this.damageLevel = 0;
        }

        public int Lives { get; set; }

        // Serves as hitbox; Player hitbox is smaller than enemies'
        public override Rectangle Rectangle
        {
            get
            {
                Vector2 upperLeftCorner = this.Movement.CurrentPosition;
                upperLeftCorner.X -= this.Texture.Width / 2;
                upperLeftCorner.Y -= this.Texture.Height / 2;
                return new Rectangle(
                    upperLeftCorner.ToPoint(),
                    new Point(this.Texture.Width / 4, this.Texture.Height / 4));
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> enemies)
        {
            if (this.resetGameTime)
            {
                this.initialSpawnTime = gameTime.TotalGameTime.TotalSeconds;
                this.resetGameTime = !this.resetGameTime;
            }

            this.previousKey = this.currentKey;
            this.currentKey = Keyboard.GetState();
            this.timer += gameTime.ElapsedGameTime.TotalSeconds;

            this.SetInvincibility(gameTime);

            this.Attack(enemies);

            // check if slow speed
            this.SlowMode = this.IsSlowPressed();

            this.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            this.Movement.ZeroXVelocity();
            this.Movement.ZeroYVelocity();

            if (sprite is PowerUp)
            {
                if (sprite is DamageUp)
                {
                    this.IncreaseDamage();
                }
                else if (sprite is ExtraLife)
                {
                    this.HP += 1;
                }
            }
            else if (this.Invincible == false)
            {
                if (sprite is Projectile projectile && projectile.Parent != this)
                {
                    this.IsRemoved = true;
                }
                else if (sprite is Enemy)
                {
                    this.IsRemoved = true;
                }
            }
        }

        public bool IsSlowPressed()
        {
            if (this.currentKey.IsKeyDown(Input.SlowMode))
            {
                this.Movement.CurrentSpeed = this.Movement.Speed / 2;
                return true;
            }

            return false;
        }

        public void Respawn(GameTime gameTime)
        {
            this.Respawn();
            this.IsRemoved = false;
            this.spawning = true;
            this.Invincible = true;
            this.initialSpawnTime = gameTime.TotalGameTime.TotalSeconds;
        }

        private void SetInvincibility(GameTime gameTime)
        {
            if (this.spawning == true)
            {
                if ((gameTime.TotalGameTime.TotalSeconds - this.initialSpawnTime) >= 2)
                {
                    this.Invincible = false;
                    this.spawning = false;
                }
            }
            else
            {
                if (this.currentKey.IsKeyDown(Keys.OemTilde) && !this.previousKey.IsKeyDown(Keys.OemTilde))
                {
                    this.Invincible = !this.Invincible;
                }
            }
        }

        private void IncreaseDamage()
        {
            this.damageLevel += 1;
            switch (this.damageLevel)
            {
                case 1:
                    this.attack.ProjectileToLaunch.Damage += 1;
                    this.attack.ProjectileToLaunch.Texture = TextureFactory.GetTexture("Bullet2");
                    break;
                case 2:
                    this.attack.ProjectileToLaunch.Damage += 1;
                    this.attack.ProjectileToLaunch.Texture = TextureFactory.GetTexture("Bullet3");
                    break;
                case 3:
                    this.attack.ProjectileToLaunch.Damage += 1;
                    this.attack.ProjectileToLaunch.Texture = TextureFactory.GetTexture("Bullet4");
                    break;
                default:
                    Debug.WriteLine("At max damage level");
                    break;
            }
        }

        private new void Attack(List<Sprite> sprites)
        {
            if (this.timer > this.attackCooldown && this.currentKey.IsKeyDown(Input.Attack) && this.previousKey.IsKeyUp(Input.Attack))
            {
                this.timer = 0;
                base.Attack(sprites);
            }
        }
    }
}
