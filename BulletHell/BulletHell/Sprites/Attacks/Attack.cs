namespace BulletHell.Sprites
{
    using System;
    using System.Diagnostics;
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
        public int NumberOfTimesAttacksHaveExecuted = 0;
        protected bool isClone = false;

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

            newAttack.NumberOfTimesAttacksHaveExecuted = 0;

            newAttack.isClone = true;

            return newAttack;
        }

        public virtual void CreateProjectile(object source, ElapsedEventArgs args)
        {
        }

        public void ExecuteAttack(object source, ElapsedEventArgs args)
        {
            this.PauseTimersWhileDebugging(source as Timer);
            this.ExecuteAttackEventHandler.Invoke(this, null);
        }

        protected void PauseTimersWhileDebugging(Timer source)
        {
#if DEBUG
            source.Stop();
#endif
        }
    }
}
