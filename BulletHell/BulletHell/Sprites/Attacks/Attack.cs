namespace BulletHell.Sprites
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Attack : Sprite
    {
        public Projectile Projectile;

        protected Attack(Projectile projectile, MovementPattern movement)
            : base(null, Color.Transparent, movement)
        {
            this.Projectile = projectile;
        }

        public virtual void CreateProjectile(List<Sprite> sprites)
        {
        }
    }
}
