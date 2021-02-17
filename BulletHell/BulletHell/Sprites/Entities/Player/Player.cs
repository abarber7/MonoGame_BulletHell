using BulletHell.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using BulletHell.Utilities;

namespace BulletHell.Sprites.Entities
{
    internal class Player : Entity
    {
        public Input input;
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public Player(Dictionary<string, object> entityProperties) : base(entityProperties)
        {
            input = new Input()
            {
                Left = Keys.A,
                Right = Keys.D,
                Up = Keys.W,
                Down = Keys.S,
            };
            string colorName = (string)entityProperties["color"];
            color = System.Drawing.Color.FromName(colorName).ToXNA();
            speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            this.Attack(sprites);

            this.Move(sprites);
        }

        private void Move(List<Sprite> sprites)
        {
            if (Keyboard.GetState().IsKeyDown(input.Left))
            {
                direction.X = -speed;
            }
            else if (Keyboard.GetState().IsKeyDown(input.Right))
            {
                direction.X = speed;
            }

            if (Keyboard.GetState().IsKeyDown(input.Up))
            {
                direction.Y = -speed;
            }
            else if (Keyboard.GetState().IsKeyDown(input.Down))
            {
                direction.Y = speed;
            }
            
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }

                if ((this.direction.X > 0 && this.IsTouchingLeft(sprite)) || (this.direction.X < 0 && this.IsTouchingRight(sprite)))
                {
                    this.direction.X = 0;
                }
                if ((this.direction.Y > 0 && this.IsTouchingTop(sprite)) || (this.direction.Y < 0 && this.IsTouchingBottom(sprite)))
                {
                    this.direction.Y = 0;
                }
            }
            
            position += this.direction;
            velocity = direction;
            this.direction = Vector2.Zero;
        }

        private void Attack(List<Sprite> sprites)
        {
            if (currentKey.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space))
            {
                base.Attack(sprites);
            }
        }
    }
}
