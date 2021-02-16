using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Utilities
{
    class TextureFactory
    {
        static public ContentManager Content { get; set; }

        public static Texture2D getTexture(string textureName)
        {
            var texture = content.Load<Texture2D>(textureName);

            // Need to throw error if texture was not found.

            return texture;
        }
    }
}
