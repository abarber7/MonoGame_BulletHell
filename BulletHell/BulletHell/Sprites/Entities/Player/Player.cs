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
            string colorName = (string)entityProperties["color"];
            color = System.Drawing.Color.FromName(colorName).ToXNA();
            this.movement.speed = 5f;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            this.Attack(sprites);

            this.movement.Move();
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
