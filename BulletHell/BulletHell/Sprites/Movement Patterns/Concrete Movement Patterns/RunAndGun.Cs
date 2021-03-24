namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using System.Timers;

    internal class RunAndGun : MovementPattern
    {
        private bool run = true; // bool to switch between moving or stopping
        private Vector2 spawnPosition; // spawn position

        private Vector2 exitPosition; // position where the entity will exit from
        private int exitSpeed; // speed when spawning

        private Vector2 startPosition;
        private Vector2 stopPosition;
        private int normalSpeed;

        private bool reachedStart = false; // bool for if entity reached start position
        private bool exitTime = false; // bool for if it is time to exit

        private System.Timers.Timer timer;

        public RunAndGun(Dictionary<string, object> runAndGunProperties)
            : base(runAndGunProperties)
        {
            // spawning
            this.spawnPosition.X = Convert.ToSingle((int)runAndGunProperties["xspawnPosition"]);
            this.spawnPosition.Y = Convert.ToSingle((int)runAndGunProperties["yspawnPosition"]);
            this.Speed = (int)runAndGunProperties["spawnSpeed"];

            // movement pattern
            this.startPosition.X = Convert.ToSingle((int)runAndGunProperties["xStartPosition"]);
            this.startPosition.Y = Convert.ToSingle((int)runAndGunProperties["yStartPosition"]);
            this.stopPosition.X = Convert.ToSingle((int)runAndGunProperties["xStopPosition"]);
            this.stopPosition.Y = Convert.ToSingle((int)runAndGunProperties["yStopPosition"]);
            this.normalSpeed = (int)runAndGunProperties["normalSpeed"];

            // exit
            this.exitPosition.X = Convert.ToSingle((int)runAndGunProperties["xExitPosition"]);
            this.exitPosition.Y = Convert.ToSingle((int)runAndGunProperties["yExitPosition"]);
            this.exitSpeed = (int)runAndGunProperties["exitSpeed"];

            // timer
            this.timer = new System.Timers.Timer((int)runAndGunProperties["timer"]);

            this.Position = this.spawnPosition; // set position to spawn positions
            this.velocity = this.CalculateVelocity(this.spawnPosition, this.startPosition, this.Speed); // set velocity to move towards start position
        }

        public override void Move()
        {
            // when timer is ellapsed swap betwwen moving or stopping
            this.timer.Elapsed += this.ExitScreen;
            this.timer.AutoReset = true;
            this.timer.Enabled = true;

            // If the entity has not reached the start then continue moving to start position
            if (this.reachedStart == false)
            {
                // if start position reached
                if (this.ExceededPosition(this.spawnPosition, this.startPosition, this.velocity))
                {
                    this.reachedStart = true; // change bool so entity will move in the pattern
                    this.Speed = this.normalSpeed;
                    this.velocity = this.CalculateVelocity(this.startPosition, this.stopPosition, this.Speed); // change velocity to match what the patterns velocity should be
                }
            }

            // Otherwise do the expected move pattern
            else
            {
                // If it is not time to exit do the movement pattern
                if (this.exitTime == false)
                {
                    // when stop position is reached stop moving and start timer
                    if (this.ExceededPosition(this.startPosition, this.stopPosition, this.velocity))
                    {
                        this.velocity = Vector2.Zero;

                    }
                }

                // Otherwise exit
                else
                {
                    this.velocity = this.CalculateVelocity(this.stopPosition, this.exitPosition, this.Speed); // change velocity to exit out of the screen
                }
            }
            base.Move();
        }

        // method to call when timer has Elapsed
        private void ExitScreen(object source, ElapsedEventArgs e)
        {

            this.exitTime = true; // change bool so the entity will exit
            this.Speed = this.exitSpeed; // change to exit speed
        }
    }
}
