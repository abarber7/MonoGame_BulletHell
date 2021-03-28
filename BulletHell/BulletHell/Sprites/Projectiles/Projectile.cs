namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Projectile : Sprite, ICloneable
    {
        public Projectile(Texture2D texture, Color color, MovementPattern movement)
            : base(texture, color, movement)
        {
        }

        public Sprite Parent { get; set; }

        // Serves as hitbox (larger than usual to account for speed vs framerate)
        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    (int)this.Movement.Position.X - (this.Texture.Width / 2),
                    (int)this.Movement.Position.Y - (this.Texture.Height / 2),
                    (int)Math.Round(this.Texture.Width * 1.5),
                    (int)Math.Round(this.Texture.Height * 3.5));
            }
        }

        // A translation matrix for collision
        // Source: https://github.com/Oyyou/MonoGame_Tutorials/blob/master/MonoGame_Tutorials/Tutorial019/Sprites/Sprite.cs
        public override Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-new Vector2(this.Rectangle.Width / 2, this.Rectangle.Height / 2), 0)) * // Rectangle width and height standing in for origin
                  Matrix.CreateTranslation(new Vector3(new Vector2(this.Movement.Position.X, this.Movement.Position.Y), 0)); // Position represented by movement pattern position
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprits)
        {
            this.Move();
        }

        public override void OnCollision(Sprite sprite)
        {
            // Ignore collision if sprite is one who fired or if sprite is another projectile (for now)
            if (sprite != this.Parent && !(sprite is Projectile))
            {
                this.IsRemoved = true;
            }
        }

        public virtual void Move()
        {
            if (this.OutOfBounds())
            {
                this.IsRemoved = true;
            }

            this.Movement.Move();
        }

        // Check against other sprites for pixel overlaps (collision)
        // Source: https://github.com/Oyyou/MonoGame_Tutorials/blob/master/MonoGame_Tutorials/Tutorial019/Sprites/Sprite.cs
        public override bool IsTextureIntersecting(Sprite sprite)
        {
            if (sprite == this)
            {
                return false;
            }

            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = this.Transform * Matrix.Invert(sprite.Transform);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            for (int yA = 0; yA < this.Rectangle.Height; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                for (int xA = 0; xA < this.Rectangle.Width; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    if (xB >= 0 && xB < sprite.Rectangle.Width &&
                        yB >= 0 && yB < sprite.Rectangle.Height)
                    {
                        // Get the color of the overlapping pixel from sprite
                        Color colorB = sprite.TextureData[xB + (yB * sprite.Rectangle.Width)];

                        // If sprite pixel isn't completely transparent
                        if (colorB.A != 0)
                        {
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool OutOfBounds()
        {
            if (this.Movement.IsTouchingLeftOfScreen() ||
                this.Movement.IsTouchingRightOfScreen() ||
                this.Movement.IsTouchingBottomOfScreen() ||
                this.Movement.IsTouchingTopOfScreen())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
