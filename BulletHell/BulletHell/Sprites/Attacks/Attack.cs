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
        protected Projectile projectile;
        protected double timer = 0;
        protected double cooldownToCreateProjectile;

        public Attack(Projectile projectile, MovementPattern movement, double cooldownToCreateProjectile)
            : base(null, Color.Transparent, movement)
        {
            this.projectile = projectile;
            this.cooldownToCreateProjectile = cooldownToCreateProjectile;
        }

        protected virtual void CreateProjectile(List<Sprite> sprites)
        {
        }
    }
}
