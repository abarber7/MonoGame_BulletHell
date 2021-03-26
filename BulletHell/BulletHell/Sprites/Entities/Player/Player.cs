namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using global::BulletHell.Sprites.Entities.Enemies;
    using global::BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal class Player : Entity
    {
        public bool slowMode;
        public bool invicible;
        private bool spawn;
        private double initialSpawnTime;
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public Player(Dictionary<string, object> entityProperties)
            : base(entityProperties)
        {
            this.Movement.Speed = 5;
            this.spawn = true;
            this.invicible = true;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (this.spawn == true)
            {
                this.initialSpawnTime = gameTime.TotalGameTime.TotalSeconds;
                this.spawn = false;
            }

            this.previousKey = this.currentKey;
            this.currentKey = Keyboard.GetState();

            this.SetInvincibility(gameTime);

            this.Attack(sprites);
            this.Collision(sprites);

            int previousSpeed = this.Movement.Speed;

            // check if slow speed
            this.slowMode = this.IsSlowPressed();

            this.Move();
            this.Movement.Speed = previousSpeed;
        }

        public bool IsSlowPressed()
        {
            if (this.currentKey.IsKeyDown(Keys.LeftShift))
            {
                this.Movement.Speed /= 2;
                return true;
            }

            return false;
        }

        public void SetInvincibility(GameTime gameTime)
        {
            if (this.currentKey.IsKeyDown(Keys.OemTilde) && !this.previousKey.IsKeyDown(Keys.OemTilde))
            {
                if (this.invicible)
                {
                    this.invicible = false;
                }
                else
                {
                    this.invicible = true;
                }
            }

            if ((gameTime.TotalGameTime.TotalSeconds - this.initialSpawnTime) >= 2)
            {
                this.invicible = false;
            }
        }

        private new void Attack(List<Sprite> sprites)
        {
            if (this.currentKey.IsKeyDown(Keys.Space) && this.previousKey.IsKeyUp(Keys.Space))
            {
                base.Attack(sprites);
            }
        }

        private void Move()
        {
            this.Movement.Move();
        }

        private void Collision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }

                if ((this.Movement.velocity.X > 0 && this.IsTouchingLeftSideOfSprite(sprite)) || (this.Movement.velocity.X < 0 && this.IsTouchingRightSideOfSprite(sprite)))
                {
                    this.Movement.velocity.X = 0;
                }

                if ((this.Movement.velocity.Y > 0 && this.IsTouchingTopSideOfSprite(sprite)) || (this.Movement.velocity.Y < 0 && this.IsTouchingBottomSideOfSprite(sprite)))
                {
                    this.Movement.velocity.Y = 0;
                }

                if (sprite is Projectile projectile)
                {
                    if (projectile.Parent is Enemy && (this.IsTouchingLeftSideOfSprite(sprite) || this.IsTouchingRightSideOfSprite(sprite) || this.IsTouchingTopSideOfSprite(sprite) || this.IsTouchingBottomSideOfSprite(sprite)))
                    {
                        if (this.invicible == false)
                        {
                            this.IsRemoved = true;
                        }

                        sprite.IsRemoved = true;
                    }
                }
            }
        }
    }
}
