using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    class ExampleEnemy : Enemy
    {
        public ExampleGrunt(Texture2D texture) : base(texture)
        {

        }

        public ExampleEnemy(Dictionary<string, object> entityProperties)
        {
        }
    }
}
