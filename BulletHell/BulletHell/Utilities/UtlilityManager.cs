namespace BulletHell.Utilities
{
    using Microsoft.Xna.Framework.Content;

    internal class UtlilityManager
    {
        public static void Initialize(ContentManager content)
        {
            TextureFactory.Content = content;
        }
    }
}
