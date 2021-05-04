namespace BulletHell.Sprites.The_Player
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.PowerUps.Concrete_PowerUps;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Player : Entity
    {
        public bool SlowMode;
        public bool Invincible;
        private double initialSpawnTime;
        private bool spawning;
        private bool resetGameTime = true;

        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public Player(Texture2D texture, Color color, MovementPattern movement, int hp, List<Attack> attacks)
            : base(texture, color, movement, hp, attacks)
        {
            this.textureScale = 1.5F;
            this.spawning = true;
            this.Invincible = true;
            this.DamageLevel = 0;
            this.points = 0;

            foreach (Attack attack in attacks)
            {
                attack.CooldownToAttack.Elapsed += attack.ExecuteAttack;
                attack.ExecuteAttackEventHandler += this.LaunchAttack;
                attack.Attacker = this;
                attack.ProjectileToLaunch.Parent = attack;
            }
        }

        public int Lives { get; set; }

        // Serves as hitbox; Player hitbox is smaller than enemies'
        public override Rectangle Rectangle
        {
            get
            {
                float size = 11 * this.textureScale;
                Vector2 upperLeftCorner = this.Movement.CurrentPosition;
                upperLeftCorner.X -= size / 2F; // from middle of sprite, offset for box width
                upperLeftCorner.Y -= this.TextureHeight / 2; // get Y to top of sprite
                upperLeftCorner.Y += (56 * this.textureScale) - (size / 2F); // offset for Bo center and size of box

                return new Rectangle(upperLeftCorner.ToPoint(), new Point((int)size, (int)size));
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

            this.SetInvincibility(gameTime);

            // check if slow speed
            this.SlowMode = this.IsSlowPressed();

            this.Move();
        }

        public override void LaunchAttack(object source, EventArgs args)
        {
            if (this.currentKey.IsKeyDown(Input.Attack))
            {
                base.LaunchAttack(source, args);
            }
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
            else
            {
                this.Movement.CurrentSpeed = this.Movement.Speed;
                return false;
            }
        }

        public void Respawn(GameTime gameTime)
        {
            this.Respawn();
            this.IsRemoved = false;
            this.spawning = true;
            this.Invincible = true;
            this.ReachedStart = false;
            this.initialSpawnTime = gameTime.TotalGameTime.TotalSeconds;
            this.Attacks.ForEach(item =>
            {
                item.CooldownToAttack.Stop();
            });
            this.initializedSpawningPosition = false;
            this.initializedDespawningPosition = false;
            this.initializedMovementPosition = false;
        }

        public void IncreasePoints(double pointValue)
        {
            this.points += pointValue;
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
                if (this.currentKey.IsKeyDown(Input.CheatingMode) && !this.previousKey.IsKeyDown(Input.CheatingMode))
                {
                    this.Invincible = !this.Invincible;
                }
            }
        }

        private void IncreaseDamage()
        {
            this.DamageLevel += 1;
            switch (this.DamageLevel)
            {
                case 1:
                    this.DamageModifier += 0.2F;
                    break;
                case 2:
                    this.DamageModifier += 0.2F;
                    break;
                case 3:
                    this.DamageModifier += 0.2F;
                    break;
                default:
                    break;
            }

            this.Attacks.ForEach(item => item.ProjectileToLaunch.SetTextureScaleBasedOnDamageLevel());
        }
    }
}
