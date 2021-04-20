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
        public Attack attack;

        protected Entity(Texture2D texture, Color color, MovementPattern movement, Attack attack)
            : base(texture, color, movement)
        {
            this.attack = attack;
        }

        protected void Attack(List<Sprite> sprites)
        {
            Attack attackClone = (Attack)this.attack.Clone();
            attackClone.Movement.Position = this.Movement.Position;

            sprites.Add(attackClone);
        }
    }
}
