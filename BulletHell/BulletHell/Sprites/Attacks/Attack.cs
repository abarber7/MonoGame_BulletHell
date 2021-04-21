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
        public Projectile projectile;
        protected double timer = 0;
        protected double projectileSpawnCooldown;

        public Attack(Projectile projectile, MovementPattern movement, double projectileSpawnCooldown)
            : base(null, Color.Transparent, movement)
        {
            this.projectile = projectile;
            this.projectileSpawnCooldown = projectileSpawnCooldown;
        }

        protected virtual void CreateProjectile(List<Sprite> sprites)
        {
        }
    }
}
