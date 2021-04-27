namespace BulletHell.Sprites
{
    using System.Collections.Generic;
    using System.Timers;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    public abstract class Attack : Sprite
    {
        public Projectile ProjectileToLaunch;
        public Sprite Attacker;
        protected Timer cooldownToCreateProjectile;

        public Attack(Projectile projectile, MovementPattern movement, Timer cooldownToCreateProjectile)
            : base(null, Color.Transparent, movement)
        {
            this.ProjectileToLaunch = projectile;
            this.cooldownToCreateProjectile = cooldownToCreateProjectile;
            this.cooldownToCreateProjectile.Stop();
            this.cooldownToCreateProjectile.AutoReset = true;
            this.cooldownToCreateProjectile.Elapsed += this.CreateProjectile;
        }

        protected virtual void CreateProjectile(object source, ElapsedEventArgs args)
        {
        }

        public override object Clone()
        {
            Attack newAttack = (Attack)this.MemberwiseClone();
            if (this.Movement != null)
            {
                MovementPattern newMovement = (MovementPattern)this.Movement.Clone();
                newAttack.Movement = newMovement;
            }

            Projectile newProjectile = (Projectile)this.ProjectileToLaunch.Clone();
            newAttack.ProjectileToLaunch = newProjectile;
            if (this.Attacker != null)
            {
                Sprite newAttacker = (Sprite)this.Attacker.Clone();
                newAttack.Attacker = newAttacker;
            }

            this.cooldownToCreateProjectile.Start();

            return newAttack;
        }
    }
}
