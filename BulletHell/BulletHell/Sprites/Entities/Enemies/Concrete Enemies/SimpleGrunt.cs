using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities.Enemies.Concrete_Enemies
{
    class SimpleGrunt : Enemy
    {
        public SimpleGrunt(Texture2D texture) : base(texture)
        {

        }

        public SimpleGrunt(Dictionary<string, object> entityProperties)
        {
        }
    }
}
