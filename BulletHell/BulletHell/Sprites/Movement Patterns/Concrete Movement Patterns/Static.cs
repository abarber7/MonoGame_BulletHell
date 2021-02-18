using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    class Static : MovementPattern
    {
        public Static(Dictionary<string, object> staticProperties) : base(staticProperties)
        {
            position.X = Convert.ToSingle((Int32)staticProperties["xPosition"]);
            position.Y = Convert.ToSingle((Int32)staticProperties["yPosition"]);
        }
    }
}
