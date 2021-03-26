namespace BulletHell.Sprites.Movement_Patterns.Concrete_Movement_Patterns
{
    using System;
    using System.Collections.Generic;

    internal class Static : MovementPattern
    {
        public Static(Dictionary<string, object> staticProperties)
            : base()
        {
            this.position.X = Convert.ToSingle((int)staticProperties["xPosition"]);
            this.position.Y = Convert.ToSingle((int)staticProperties["yPosition"]);
        }
    }
}
