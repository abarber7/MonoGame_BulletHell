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
        
        private KeyboardState currentKey;
        private KeyboardState previousKey;

        public Player(Dictionary<string, object> entityProperties) : base(entityProperties)
        {
            this.movement.speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            this.Attack(sprites);
            this.Collision(sprites);
            this.Move();
        }

        private void Attack(List<Sprite> sprites)
        {
            if (currentKey.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space))
            {
                base.Attack(sprites);
            }
        }

        private void Move()
        {
            this.movement.Move();
        }

        private void Collision(List<Sprite> sprites)
        {
            foreach (var sprite in sprites)
            {
                if (sprite == this)
                {
                    continue;
                }

                if ((this.movement.velocity.X > 0 && this.IsTouchingLeftSideOfSprite(sprite)) || (this.movement.velocity.X < 0 && this.IsTouchingRightSideOfSprite(sprite)))
                {
                    this.movement.velocity.X = 0;
                }
                if ((this.movement.velocity.Y > 0 && this.IsTouchingTopSideOfSprite(sprite)) || (this.movement.velocity.Y < 0 && this.IsTouchingBottomSideOfSprite(sprite)))
                {
                    this.movement.velocity.Y = 0;
                }
            }
        }
    }
}
