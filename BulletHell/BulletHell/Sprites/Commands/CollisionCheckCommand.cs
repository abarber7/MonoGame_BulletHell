namespace BulletHell.Sprites.Commands
{
    using System.Collections.Generic;

    internal class CollisionCheckCommand : ICommand
    {
        private readonly Sprite focusSprite;
        private readonly List<Sprite> sprites;

        public CollisionCheckCommand(Sprite focusSprite, List<Sprite> sprites)
        {
            this.focusSprite = focusSprite;
            this.sprites = sprites;
        }

        public void Execute() => this.focusSprite?.CheckForCollision(this.sprites.FindAll(item => item != null));
    }
}
