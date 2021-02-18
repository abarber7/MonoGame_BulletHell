using BulletHell.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    class ExampleEnemy : Enemy
    {
        int previousTime = 0;

        public ExampleEnemy(Dictionary<string, object> exampleGruntProperties) : base(exampleGruntProperties)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            if (previousTime != (int)gameTime.TotalGameTime.TotalSeconds)
            {
                this.Attack(sprites);
            }

            previousTime = (int)gameTime.TotalGameTime.TotalSeconds;

            base.Collision(sprites);
            //this.Move(sprites);
        }

    }
}
