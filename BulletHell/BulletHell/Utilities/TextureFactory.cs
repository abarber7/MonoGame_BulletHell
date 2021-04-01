namespace BulletHell.Utilities
{
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    internal class TextureFactory
    {
        public static ContentManager Content { get; set; }

        public static Texture2D GetTexture(string textureName)
        {
            var texture = Content.Load<Texture2D>(textureName);

            // TODO: Need to throw error if texture was not found.
            return texture;
        }

        public static SpriteFont GetSpriteFont(string spriteFontName)
        {
            var spriteFont = Content.Load<SpriteFont>(spriteFontName);

            return spriteFont;
        }
    }
}
