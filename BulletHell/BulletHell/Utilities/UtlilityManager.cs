namespace BulletHell.Utilities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    internal class UtlilityManager
    {
        public static void Initialize(ContentManager content, GraphicsDeviceManager graphicsDeviceManager)
        {
            TextureFactory.Content = content;
            GraphicManagers.GraphicsDeviceManager = graphicsDeviceManager;
        }
    }
}
