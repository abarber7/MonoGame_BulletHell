namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Timers;
    using Microsoft.Xna.Framework;

    internal class RunAndGun : MovementPattern
    {
        private Vector2 spawnPosition; // spawn position
        private Vector2 exitPosition; // position where the entity will exit from
        private Vector2 startPosition;
        private Vector2 stopPosition;

        private bool reachedStart = false; // bool for if entity reached start position
        private bool exitTime = false; // bool for if it is time to exit

        private Timer timer;

        public RunAndGun(Vector2 spawnPosition, Vector2 startPosition, Vector2 stopPosition, Vector2 exitPosition, int speed, Timer timer)
            : base()
        {
            this.spawnPosition = spawnPosition;
            this.startPosition = startPosition;
            this.stopPosition = stopPosition;
            this.exitPosition = exitPosition;
            this.Speed = speed;
            this.timer = timer;

            this.CurrentSpeed = this.Speed * 2;
            this.Position = this.spawnPosition; // set position to spawn positions
            this.Velocity = this.CalculateVelocity(this.spawnPosition, this.startPosition, this.Speed); // set velocity to move towards start position
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
                if (this.ExceededPosition(this.spawnPosition, this.startPosition, this.Velocity))
                {
                    this.reachedStart = true; // change bool so entity will move in the pattern
                    this.CurrentSpeed = this.Speed;
                    this.Velocity = this.CalculateVelocity(this.startPosition, this.stopPosition, this.Speed); // change velocity to match what the patterns velocity should be
                }
            }

            // Otherwise do the expected move pattern
            else
            {
                // If it is not time to exit do the movement pattern
                if (this.exitTime == false)
                {
                    // when stop position is reached stop moving and start timer
                    if (this.ExceededPosition(this.startPosition, this.stopPosition, this.Velocity))
                    {
                        this.Velocity = Vector2.Zero;
                    }
                }

                // Otherwise exit
                else
                {
                    this.Velocity = this.CalculateVelocity(this.stopPosition, this.exitPosition, this.Speed); // change velocity to exit out of the screen
                }
            }

            base.Move();
        }

        // method to call when timer has Elapsed
        private void ExitScreen(object source, ElapsedEventArgs e)
        {
            this.exitTime = true; // change bool so the entity will exit
            this.CurrentSpeed = this.Speed * 2; // change to exit speed
        }
    }
}
