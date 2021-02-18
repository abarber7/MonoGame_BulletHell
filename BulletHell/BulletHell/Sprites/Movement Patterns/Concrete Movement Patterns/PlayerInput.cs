using BulletHell.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    class PlayerInput : MovementPattern
    {
        public Input input;

        public PlayerInput(Dictionary<string, object> playerInputProperties) : base(playerInputProperties)
        {
            this.input = new Input()
            {
                Left = Keys.A,
                Right = Keys.D,
                Up = Keys.W,
                Down = Keys.S,
                Attack = Keys.Space
            };

            speed = (float)playerInputProperties["speed"];
            position.X = Convert.ToSingle((Int32)playerInputProperties["xPosition"]);
            position.Y = Convert.ToSingle((Int32)playerInputProperties["yPosition"]);
        }

        public override void Move()
        {
            if (Keyboard.GetState().IsKeyDown(input.Left))
            {
                this.velocity.X = -this.speed;
            }
            else if (Keyboard.GetState().IsKeyDown(input.Right))
            {
                this.velocity.X = this.speed;
            }

            if (Keyboard.GetState().IsKeyDown(input.Up))
            {
                this.velocity.Y = -this.speed;
            }
            else if (Keyboard.GetState().IsKeyDown(input.Down))
            {
                this.velocity.Y = this.speed;
            }

            if (this.IsTouchingLeftOfScreen() || this.IsTouchingRightOfScreen())
            {
                this.velocity.X = 0;
            }
            if (this.IsTouchingBottomOfScreen() || this.IsTouchingTopOfScreen())
            {
                this.velocity.Y = 0;
            }

            this.position += this.velocity;
            this.velocity = Vector2.Zero;
        }
    }
}
