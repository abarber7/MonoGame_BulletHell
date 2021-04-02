namespace BulletHell
{
    using System.Collections.Generic;
    using BulletHell.Sprites;
    using BulletHell.Sprites.The_Player;
    using Microsoft.Xna.Framework;

    internal class PlayerCommand : ICommand
    {
        private Player player;

        public PlayerCommand(Player player)
        {
            this.player = player;
        }

        public void Execute(GameTime gameTime, List<Sprite> sprites)
        {
            this.player.Update(gameTime, sprites);
            // this.playermanager.collisiondetection
        }
    }
}
