using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns
{
    abstract class MovementPattern : ICloneable
    {
        public Vector2 origin;
        public Vector2 position;
        public Vector2 velocity;
        public float speed;
        public float rotation;

        public MovementPattern(Dictionary<string, object> movementPatternProperties)
        {
            this.speed = (float)movementPatternProperties["speed"];
        }

        public MovementPattern(MovementPattern movementPattern)
        {
            this.origin = movementPattern.origin;
            this.position = movementPattern.position;
            this.velocity = movementPattern.velocity;
            this.speed = movementPattern.speed;
            this.rotation = movementPattern.rotation;
        }

        virtual public void Move()
        {

        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
