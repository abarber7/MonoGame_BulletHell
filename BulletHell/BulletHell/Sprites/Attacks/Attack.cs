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
        public int NumberOfTimesToAttack = 1;
        public Projectile ProjectileToLaunch;
        public Entity Attacker;
        public Timer CooldownToAttack;
        public Timer CooldownToCreateProjectile;
        protected int numberOfTimesAttacksHaveExecuted = 0;

        public Attack(Projectile projectile, MovementPattern movement, Timer cooldownToAttack, Timer cooldownToCreateProjectile)
            : base(null, Color.Transparent, movement)
        {
            this.ProjectileToLaunch = projectile;

            this.CooldownToAttack = cooldownToAttack;

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

            newAttack.Attacker = this.Attacker;

            newAttack.numberOfTimesAttacksHaveExecuted = 0;

            return newAttack;
        }

        public virtual void CreateProjectile(object source, ElapsedEventArgs args)
        {
        }

        public virtual void ExecuteAttack(object source, ElapsedEventArgs args)
        {
            this.CooldownToCreateProjectile.Elapsed += this.CreateProjectile;
            this.CooldownToCreateProjectile.Start();
            this.numberOfTimesAttacksHaveExecuted++;
            this.ExecuteAttackEventHandler.Invoke(this, null);
        }
    }
}
