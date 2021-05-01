namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class FinalBoss : Enemy
    {
        private List<Attack> phase2Attacks;
        private int initialHP;

        public FinalBoss(Texture2D texture, Color color, MovementPattern movement, PowerUp powerUp, int hp, List<Attack> attacks)
            : base(texture, color, movement, powerUp, hp, attacks)
        {
            this.initialHP = hp;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (this.HP <= this.initialHP)
            {
                this.phase2Attacks.ForEach(item =>
                {
                    // TODO: Intitialize attacks and their events
                });
            }

            base.Update(gameTime, sprites);
        }

        private void BeginPhase2Attacks()
        {

        }
    }
}
