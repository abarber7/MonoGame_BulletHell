using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    class BackAndForth : MovementPattern
    {
        private Vector2 startPosition;
        private Vector2 endPosition;

        public BackAndForth(Dictionary<string, object> backAndForthProperties) : base(backAndForthProperties)
        {
            speed = (float)backAndForthProperties["speed"];
            startPosition.X = (float)backAndForthProperties["xStartPosition"];
            startPosition.Y = (float)backAndForthProperties["yStartPosition"];
            endPosition.X = (float)backAndForthProperties["xEndPosition"];
            endPosition.Y = (float)backAndForthProperties["yEndPosition"];
            velocity.X = speed / (endPosition.X - startPosition.X);
            velocity.Y = speed / (endPosition.Y - startPosition.Y);
        }

        public override void Move()
        {
            
        }
    }
}
