namespace BulletHell.Utilities
{
    using System.Drawing;

    public static class Extensions
    {
        // Source: https://community.monogame.net/t/custom-color-class-not-being-converted-correctly/10921
        public static Microsoft.Xna.Framework.Color ToXNA(this Color drawingColor)
        {
            return new Microsoft.Xna.Framework.Color(drawingColor.R, drawingColor.G, drawingColor.B, drawingColor.A);
        }

        public static Color ToDrawing(this Microsoft.Xna.Framework.Color drawingColor)
        {
            return System.Drawing.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
        }
    }
}
