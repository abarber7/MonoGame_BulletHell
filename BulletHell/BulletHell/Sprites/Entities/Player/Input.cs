namespace BulletHell.Player
{
    using Microsoft.Xna.Framework.Input;

    // Allows for rebinding.
    internal class Input
    {
        public static object Keys { get; internal set; }
        public Keys Up { get; set; }

        public Keys Down { get; set; }

        public Keys Left { get; set; }

        public Keys Right { get; set; }

        public Keys Attack { get; set; }
    }
}
