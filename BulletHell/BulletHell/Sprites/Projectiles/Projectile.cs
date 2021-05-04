namespace BulletHell.Sprites.Projectiles
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Projectile : Sprite
    {
        private float damage;

        public Projectile(Texture2D texture, Color color, MovementPattern movement, float damage)
            : base(texture, color, movement)
        {
            this.Damage = damage;
        }

        public Sprite Parent { get; set; }

        public float Damage { get => this.damage * (this.Parent as Entity).DamageModifier; set => this.damage = value; }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            this.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            // Ignore collision if sprite is one who fired or if sprite is another projectile (for now)
            if (sprite != this.Parent && !(sprite is Projectile))
            {
                // Hit case 1: sprite is Player, and this projectile is from an Enemy
                // Hit case 2: sprite is an Enemy, and this projectile is from Player
                if ((sprite is Player && this.Parent is Enemy) ||
                    (sprite is Enemy && this.Parent is Player))
                {
                    this.IsRemoved = true;
                }
            }
        }

        public virtual void Move()
        {
            if (this.OutOfBounds())
            {
                this.IsRemoved = true;
            }

            this.Movement.Move();
        }

        public bool OutOfBounds()
        {
            if (this.Movement.IsTouchingLeftOfScreen() ||
                this.Movement.IsTouchingRightOfScreen() ||
                this.Movement.IsTouchingBottomOfScreen() ||
                this.Movement.IsTouchingTopOfScreen())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object Clone()
        {
            Projectile newProjectile = this.MemberwiseClone() as Projectile;
            newProjectile.Parent = this;

            if (this.Movement != null)
            {
                MovementPattern newMovement = this.Movement.Clone() as MovementPattern;
                newProjectile.Movement = newMovement;
            }

            return newProjectile;
        }

        public void SetTextureScaleBasedOnDamageLevel()
        {
            float damageLevel = (this.Parent as Attack).Attacker.DamageLevel;

            switch (damageLevel)
            {
                case 1:
                    this.textureScale *= 1.2F;
                    this.Color = Color.Orange;
                    break;
                case 2:
                    this.textureScale *= 1.2F;
                    this.Color = Color.Yellow;
                    break;
                case 3:
                    this.textureScale *= 1.2F;
                    this.Color = Color.White;
                    break;
                default:
                    break;
            }
        }
    }
}
