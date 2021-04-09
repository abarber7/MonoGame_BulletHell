namespace BulletHell.Utilities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class GraphicManagers
    {
        public static GraphicsDeviceManager GraphicsDeviceManager { get; set; }

        public static GraphicsDevice GraphicsDevice { get => GraphicsDeviceManager.GraphicsDevice; }
    }
}
