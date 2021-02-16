using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities
{
    internal abstract class Entity : Sprite
    {
        //private int healthPoints;

        protected Entity(Dictionary<string, object> entityProperties) : base(entityProperties)
        {
            //healthPoints = (int)entityProperties["healthPoints"];
        }

        protected void Move()
        {

        }
    }
}
