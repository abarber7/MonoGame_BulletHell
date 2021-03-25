﻿namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
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
            this.Speed = (int)playerInputProperties["speed"];
            this.Position.X = Convert.ToSingle((int)playerInputProperties["xPosition"]);
            this.Position.Y = Convert.ToSingle((int)playerInputProperties["yPosition"]);
        }

        public override void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                this.velocity.X = -this.Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                this.velocity.X = this.Speed;
            }

            if (Keyboard.GetState().IsKeyDown(Input.Up))
            {
                this.velocity.Y = -this.Speed;
            }
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
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

            base.Move();
            this.velocity = Vector2.Zero;
        }
    }
}
