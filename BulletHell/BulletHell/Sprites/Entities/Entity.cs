namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Entity : Sprite
    {
        public int HP;
        protected double timer;
        protected double attackCooldown;
        protected Attack attack;

        protected Entity(Texture2D texture, Color color, MovementPattern movement, Attack attack, int hp, double attackCooldown)
            : base(texture, color, movement)
        {
            this.attack = attack;
            this.HP = hp;
            this.attackCooldown = attackCooldown;
        }

        protected void Attack(List<Sprite> sprites)
        {
             if (this.Movement.reachedStart && !this.Movement.exitTime)
            {
                Attack attackClone = (Attack)this.attack.Clone();
                attackClone.Attacker = this;
                attackClone.Movement.Position = this.GetCenterOfSprite();

                sprites.Add(attackClone);
            }
        }
    }
}
