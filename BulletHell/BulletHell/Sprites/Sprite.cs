namespace BulletHell.Sprites
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Sprite
    {
        private readonly Color[] textureData;
        private bool isRemoved = false;
        private Color color = Color.White;

        public Sprite(Texture2D texture, Color color, MovementPattern movement)
        {
            this.Texture = texture;
            this.Color = color;
            this.Movement = movement;
            this.textureData = new Color[texture.Width * texture.Height]; // array size is pixel amount in the texture
            this.Texture.GetData(this.textureData);
        }

        public Texture2D Texture { get; set; }

        public MovementPattern Movement { get; set; }

        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        public bool IsRemoved
        {
            get
            {
                return this.isRemoved;
            }

            set
            {
                this.isRemoved = value;
            }
        }

        // A translation matrix for collision
        // Source: https://github.com/Oyyou/MonoGame_Tutorials/blob/master/MonoGame_Tutorials/Tutorial019/Sprites/Sprite.cs
        public Matrix Transform
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-new Vector2(this.Texture.Width / 2, this.Texture.Height / 2), 0)) * // Texture width and height standing in for origin
                  Matrix.CreateTranslation(new Vector3(new Vector2(this.Movement.Position.X, this.Movement.Position.Y), 0)); // Position represented by movement pattern position
            }
        }

        // Serves as hitbox
        public virtual Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    new Point((int)this.Movement.Position.X, (int)this.Movement.Position.Y),
                    new Point(this.Texture.Width, this.Texture.Height));
            }
        }

        // Check against other sprites for pixel overlaps (collision)
        // Source: https://github.com/Oyyou/MonoGame_Tutorials/blob/master/MonoGame_Tutorials/Tutorial019/Sprites/Sprite.cs
        public bool IsTextureIntersecting(Sprite sprite)
        {
            if (sprite == this)
            {
                return false;
            }

            // A: this, B: colliding sprite
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
                    int xB = (int)System.Math.Round(posInB.X);
                    int yB = (int)System.Math.Round(posInB.Y);

                    if (xB >= 0 && xB < sprite.Rectangle.Width &&
                        yB >= 0 && yB < sprite.Rectangle.Height)
                    {
                        // Get the colors of the overlapping pixels
                        Color colourA = this.textureData[xA + (yA * this.Rectangle.Width)];
                        Color colourB = sprite.textureData[xB + (yB * sprite.Rectangle.Width)];

                        // If both pixels are not completely transparent
                        if (colourA.A != 0 && colourB.A != 0)
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

        public virtual void Update(GameTime gametime, List<Sprite> sprites)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Movement.Position, null, this.Color, this.Movement.Rotation, this.Movement.Origin, 1, SpriteEffects.None, 0);
        }

        public virtual void OnCollision(Sprite sprite)
        {
        }

        public bool IsHitboxIntersecting(Sprite sprite)
        {
            // To check if either rectangle is actually a line
            // For example :  l1 ={-1,0}  r1={1,1}  l2={0,-1} r2={0,1}
            if (this.Rectangle.Left == this.Rectangle.Right ||
                this.Rectangle.Top == sprite.Rectangle.Bottom ||
                sprite.Rectangle.Left == sprite.Rectangle.Right ||
                sprite.Rectangle.Top == sprite.Rectangle.Bottom)
            {
                // the line cannot have positive overlap
                return false;
            }

            // If one rectangle is on left side of other
            if (this.Rectangle.Left >= sprite.Rectangle.Right || sprite.Rectangle.Left >= this.Rectangle.Right)
            {
                return false;
            }

            // If one rectangle is above other
            if (this.Rectangle.Top >= sprite.Rectangle.Bottom || sprite.Rectangle.Top >= this.Rectangle.Bottom)
            {
                return false;
            }

            return true;
        }

        protected bool IsTouchingLeftSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Right + this.Movement.Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRightSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Left + this.Movement.Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTopSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.Movement.Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottomSideOfSprite(Sprite sprite)
        {
            return this.Rectangle.Top + this.Movement.Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool HasCollidedWithASprite(Sprite sprite)
        {
            if ((this.Movement.Velocity.X > 0 && this.IsTouchingLeftSideOfSprite(sprite)) || (this.Movement.Velocity.X < 0 && this.IsTouchingRightSideOfSprite(sprite)) || (this.Movement.Velocity.Y > 0 && this.IsTouchingTopSideOfSprite(sprite)) || (this.Movement.Velocity.Y < 0 && this.IsTouchingBottomSideOfSprite(sprite)))
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