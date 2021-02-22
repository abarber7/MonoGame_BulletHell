namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using System.Timers;

    internal class RunAndGun : MovementPattern
    {
        private bool run = true; // bool to switch between moving or stopping
        private System.Timers.Timer timer = new System.Timers.Timer(1000); // timer for 1000 mili seconds

        public RunAndGun(Dictionary<string, object> runAndGunProperties)
            : base(runAndGunProperties)
        {
            // copied from linear movement pattern
            this.Speed = (int)runAndGunProperties["speed"];
            this.velocity.X = Convert.ToSingle((int)runAndGunProperties["xVelocity"]);
            this.velocity.Y = Convert.ToSingle((int)runAndGunProperties["yVelocity"]);
        }

        public override void Move()
        {
            // when timer is ellapsed swap betwwen moving or stopping
            this.timer.Elapsed += this.moveOrStop;
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
            base.Move();
        }

        private void moveOrStop (Object source, ElapsedEventArgs e)
        {
            // move
            if (this.run == true)
            {
                this.velocity.X = 1;
                this.velocity.Y = 0;
                this.run = false;
            }

            // stop
            else
            {
                this.velocity = Vector2.Zero;
                this.run = true;
            }
        }
    }
}
