using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities.Enemies
{
    abstract class Enemy : Entity
    {
        public Enemy(Texture2D texture) : base(texture)
        {

        }
    }
}
