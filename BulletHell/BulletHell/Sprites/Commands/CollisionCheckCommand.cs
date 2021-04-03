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

        public void Execute()
        {
            for (int i = this.sprites.Count - 1; i >= 0; i--)
            {
                // Check for hitbox collision
                if (this.focusSprite.Rectangle.Intersects(this.sprites[i].Rectangle))
                {
                    this.focusSprite.OnCollision(this.sprites[i]);
                    this.sprites[i].OnCollision(this.focusSprite);
                }
            }
        }
    }
}
