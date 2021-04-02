namespace BulletHell
{
    using System.Collections.Generic;
    using BulletHell.Sprites;
    using Microsoft.Xna.Framework;

    internal interface ICommand
    {
        public void Execute(GameTime gameTime, List<Sprite> sprites);
    }
}
