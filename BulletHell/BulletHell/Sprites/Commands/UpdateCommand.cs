namespace BulletHell.Sprites.Commands
{
    using System.Collections.Generic;
    using BulletHell.Sprites;
    using Microsoft.Xna.Framework;

    // Update Command for the purpose of being able to execute from a queue of commands.
    internal class UpdateCommand : ICommand
    {
        private readonly Sprite focusSprite;
        private readonly GameTime gameTime;
        private readonly List<Sprite> sprites;

        public UpdateCommand(Sprite focusSprite, GameTime time, List<Sprite> sprites)
        {
            this.focusSprite = focusSprite;
            this.gameTime = time;
            this.sprites = sprites;
        }

        public void Execute() => this.focusSprite?.Update(this.gameTime, this.sprites);
    }
}
