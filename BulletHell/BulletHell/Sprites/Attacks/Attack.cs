namespace BulletHell.Sprites
{
    using System;
    using System.Timers;
    using BulletHell.Sprites.Entities;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;

    public abstract class Attack : Sprite
    {
        public Projectile ProjectileToLaunch;
        public Entity Attacker;
        public Timer CooldownToAttack;
        public Timer CooldownToCreateProjectile;

        public Attack(Projectile projectile, MovementPattern movement, Timer cooldownToAttack, Timer cooldownToCreateProjectile)
            : base(null, Color.Transparent, movement)
        {
            this.ProjectileToLaunch = projectile;

            this.CooldownToAttack = cooldownToAttack;
            this.CooldownToAttack.Elapsed += this.ExecuteAttack;

            this.CooldownToCreateProjectile = cooldownToCreateProjectile;
        }

        public event EventHandler ExecuteAttackEventHandler;

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
                Entity newAttacker = (Entity)this.Attacker.Clone();
                newAttack.Attacker = newAttacker;
            }

            newAttack.CooldownToCreateProjectile.Elapsed += this.CreateProjectile;
            newAttack.ExecuteAttackEventHandler += this.Attacker.ExecuteAttack;

            this.CooldownToCreateProjectile.Start();

            return newAttack;
        }

        protected virtual void CreateProjectile(object source, ElapsedEventArgs args)
        {
        }

        protected virtual void ExecuteAttack(object source, ElapsedEventArgs args)
        {
            this.ExecuteAttackEventHandler.Invoke(this, null);
        }
    }
}
