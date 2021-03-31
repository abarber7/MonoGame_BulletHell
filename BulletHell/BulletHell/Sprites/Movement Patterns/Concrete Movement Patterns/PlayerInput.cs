namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System.Collections.Generic;
    using global::BulletHell.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal class PlayerInput : MovementPattern
    {
        private Vector2 spawnPosition;
        private Vector2 startPosition;
        private bool respawning;

        public PlayerInput(Vector2 spawnPosition, Vector2 startPosition, int speed)
            : base()
        {
            this.spawnPosition = spawnPosition;
            this.startPosition = startPosition;
            this.Speed = speed;
            this.Respawn();
        }

        public void Respawn()
        {
            this.respawning = true;
            this.position = this.spawnPosition;
            this.CurrentSpeed = this.Speed * 2;
            this.velocity = this.CalculateVelocity(this.spawnPosition, this.startPosition, this.CurrentSpeed);
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
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                {
                    this.velocity.X = -this.CurrentSpeed;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                {
                    this.velocity.X = this.CurrentSpeed;
                }

                if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    this.velocity.Y = -this.CurrentSpeed;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Down))
                {
                    this.velocity.Y = this.CurrentSpeed;
                }

                if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
                {
                    this.velocity.X = 0;
                }

                if (this.IsTouchingBottomOfScreen() || this.IsTouchingTopOfScreen())
                {
                    this.velocity.Y = 0;
                }

                base.Move();
                this.velocity = Vector2.Zero;
            }
        }
    }
}
