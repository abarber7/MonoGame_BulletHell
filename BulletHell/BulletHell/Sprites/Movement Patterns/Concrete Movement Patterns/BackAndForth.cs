namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Timers;
    using Microsoft.Xna.Framework;

    internal class BackAndForth : MovementPattern
    {
        private Vector2 spawnPosition; // spawn position
        private Vector2 exitPosition; // position where the entity will exit from
        private Vector2 startPosition;
        private Vector2 endPosition;

        private bool reachedStart = false; // bool for if entity reached start position
        private bool exitTime = false; // bool for if it is time to exit

        private System.Timers.Timer timer = new System.Timers.Timer(15000); // timer for exit at 15000 mili seconds

        public BackAndForth(Vector2 spawnPosition, Vector2 startPosition, Vector2 endPosition, int speed)
            : base()
        {
            this.spawnPosition = spawnPosition;
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.Speed = speed;
            this.Position = this.spawnPosition; // set position to spawn positions
            this.Velocity = this.CalculateVelocity(this.spawnPosition, this.startPosition, this.Speed); // set velocity to move towards start position
        }

        public override void Move()
        {
            // timer stuff
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
                    this.Velocity = this.CalculateVelocity(this.startPosition, this.endPosition, this.Speed); // change velocity to match what the patterns velocity should be
                }
            }

            // Otherwise do the expected move pattern
            else
            {
                // If it is not time to exit do the movement pattern
                if (this.exitTime == false)
                {
                    // when exceeding position reverse velocity to go back and forth
                    if (this.ExceededPosition(this.startPosition, this.endPosition, this.Velocity))
                    {
                        this.Velocity = -this.Velocity;
                    }
                }

                // Otherwise exit
                else
                {
                    this.Velocity = this.CalculateVelocity(this.exitPosition, this.spawnPosition, this.Speed); // change velocity to exit out of the screen
                }
            }

            base.Move();

            // this.position = new Vector2(this.Parent.Rectangle.Center.X, this.Parent.Rectangle.Center.Y) + this.Velocity; // Rectangle offset bug hunting (analogous statement)--remove later
        }

        // method to call when timer has Elapsed
        private void ExitScreen(object source, ElapsedEventArgs e)
        {
            this.exitTime = true; // change bool so the entity will exit
            this.exitPosition = this.Position; // save the position at the end of the timer so it can be used for calculating exit velocity
            this.CurrentSpeed = this.Speed * 2; // change to exit speed
        }
    }
}
