using BulletHell.Sprites.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities.Enemies
{
    abstract class Enemy : Entity
    {
        

        public Enemy(Dictionary<string, object> enemyProperties) : base(enemyProperties)
        {

        }

        
    }
}
