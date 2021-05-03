namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using BulletHell.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal class PlayerInput : MovementPattern
    {
        private Vector2 spawnPosition;
        private bool respawning;

        public PlayerInput(Vector2 spawnPosition, Vector2 startPosition, int speed)
            : base(startPosition, speed)
        {
            this.spawnPosition = spawnPosition;
            this.Respawn();
        }

        public void Respawn()
        {
            this.respawning = true;
            this.CurrentPosition = this.spawnPosition;
            this.CurrentSpeed = this.Speed * 2;
            this.velocity = CalculateVelocity(this.spawnPosition, this.startPosition, this.CurrentSpeed);
        }

        public override void Move()
        {
            if (this.respawning)
            {
                // if start position reached
                if (this.ExceededPosition(this.spawnPosition, this.startPosition, this.Velocity))
                {
                    this.respawning = false; // change bool so entity will move in the pattern
                    this.CurrentSpeed = this.Speed;
                    this.ZeroXVelocity();
                    this.ZeroYVelocity();
                }

                base.Move();
            }
            else
            {
                bool isMoving = false;
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                {
                    this.velocity.X = -this.CurrentSpeed;
                    isMoving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                {
                    this.velocity.X = this.CurrentSpeed;
                    isMoving = true;
                }

                if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    this.velocity.Y = -this.CurrentSpeed;
                    isMoving = true;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                {
                    this.velocity.Y = this.CurrentSpeed;
                    isMoving = true;
                }

                if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
                {
                    this.velocity.X = 0;
                }

                if (this.IsTouchingBottomOfScreen() || this.IsTouchingTopOfScreen())
                {
                    this.velocity.Y = 0;
                }

                if (isMoving)
                {
                    base.Move();
                }

                this.velocity = Vector2.Zero;
            }
        }
    }
}
