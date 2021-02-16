using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BulletHell.Utilities
{
    public static class Extensions
    {
        public static Microsoft.Xna.Framework.Color ToXNA(this Color drawingColor)
        {
            return new Microsoft.Xna.Framework.Color(drawingColor.R, drawingColor.G, drawingColor.B, drawingColor.A);
        }

        public static System.Drawing.Color ToDrawing(this Microsoft.Xna.Framework.Color drawingColor)
        {
            return System.Drawing.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }
    }
}
