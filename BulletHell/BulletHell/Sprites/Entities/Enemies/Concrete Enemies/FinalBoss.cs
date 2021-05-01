namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using System.Timers;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class FinalBoss : Enemy
    {
        private List<Attack> phase2Attacks;
        private int initialHP;
        private Timer swapToPhase2;
        private bool swappedToPhase2 = false;

        public FinalBoss(Texture2D texture, Color color, MovementPattern movement, PowerUp powerUp, int hp, List<Attack> attacks, List<Attack> phase2Attacks)
            : base(texture, color, movement, powerUp, hp, attacks)
        {
            this.phase2Attacks = phase2Attacks;
            this.initialHP = hp;
            this.swapToPhase2 = new Timer(15 * 1000);
            this.swapToPhase2.Stop();
            this.swapToPhase2.AutoReset = false;
            this.swapToPhase2.Elapsed += this.SwitchToPhase2DueToTime;
            this.textureScale = 1;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (!this.Exiting && this.ReachedStart)
            {
                this.swapToPhase2.Start();
            }

            if (this.HP <= this.initialHP / 2.0 && !this.swappedToPhase2)
            {
                this.BeginPhase2Attacks();
            }

            base.Update(gameTime, sprites);
        }

        private void SwitchToPhase2DueToTime(object source, ElapsedEventArgs args)
        {
            this.BeginPhase2Attacks();
        }

        private void BeginPhase2Attacks()
        {
            this.swappedToPhase2 = true;

            this.Attacks.ForEach(item => item.CooldownToAttack.Stop());

            this.phase2Attacks.ForEach(item =>
            {
                item.Attacker = this;
                item.CooldownToAttack.Elapsed += item.ExecuteAttack;
                item.ExecuteAttackEventHandler += this.LaunchAttack;
                item.CooldownToAttack.Start();
            });
        }
    }
}
