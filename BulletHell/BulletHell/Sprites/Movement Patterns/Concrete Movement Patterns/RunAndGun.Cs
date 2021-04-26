namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Timers;
    using Microsoft.Xna.Framework;

    internal class RunAndGun : MovementPattern
    {
        private Vector2 stopPosition;

        private Timer timer;

        public RunAndGun(Vector2 startPosition, Vector2 stopPosition, int speed, Timer timer)
            : base(stopPosition, speed)
        {
            this.stopPosition = stopPosition;
            this.timer = timer;
            this.timer.Stop();
            this.timer.Elapsed += this.MovementComplete;
        }

        public override void InitializeMovement()
        {
            this.CurrentPosition = this.stopPosition;
            this.CurrentSpeed = this.Speed;
            this.Velocity = default(Vector2);
            this.timer.Start();
        }

        public override void Move()
        {
            base.Move();
        }

        private void MovementComplete(object source, ElapsedEventArgs e)
        {
            this.CompletedMovement = true; // change bool so the entity will exit
        }
    }
}
