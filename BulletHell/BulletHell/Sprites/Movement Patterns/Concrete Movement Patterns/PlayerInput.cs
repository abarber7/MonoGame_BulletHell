namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal class PlayerInput : MovementPattern
    {
        private Input input;

        public PlayerInput(Dictionary<string, object> playerInputProperties)
            : base(playerInputProperties)
        {
            this.input = new Input()
            {
                Left = Keys.A,
                Right = Keys.D,
                Up = Keys.W,
                Down = Keys.S,
                Attack = Keys.Space,
            };

            this.Speed = (float)playerInputProperties["speed"];
            this.Position.X = Convert.ToSingle((int)playerInputProperties["xPosition"]);
            this.Position.Y = Convert.ToSingle((int)playerInputProperties["yPosition"]);
        }

        public override void Move()
        {
            if (Keyboard.GetState().IsKeyDown(this.input.Left))
            {
                this.velocity.X = -this.Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(this.input.Right))
            {
                this.velocity.X = this.Speed;
            }

            if (Keyboard.GetState().IsKeyDown(this.input.Up))
            {
                this.velocity.Y = -this.Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(this.input.Down))
            {
                this.velocity.Y = this.Speed;
            }

            if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
            {
                this.velocity.X = 0;
            }

            if (this.IsTouchingBottomOfScreen() || this.IsTouchingTopOfScreen())
            {
                this.velocity.Y = 0;
            }

            this.Position += this.velocity;
            this.velocity = Vector2.Zero;
        }
    }
}
