using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Utilities
{
    class UtlilityManager
    {
        public static void Initialize(ContentManager content)
        {
            TextureFactory.Content = content;
        }
    }
}
