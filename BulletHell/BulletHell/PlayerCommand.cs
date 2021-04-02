namespace BulletHell
{
    using BulletHell.Sprites.The_Player;

    internal class PlayerCommand : ICommand
    {
        private Player player;

        public PlayerCommand(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            //this.player.Update();
        }
    }
}
