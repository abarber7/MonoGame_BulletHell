using BulletHell.Player;
using BulletHell.Sprites.Movement_Patterns;
using BulletHell.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites
{
    abstract class Sprite
    {
        public Texture2D texture;
        public bool isRemoved = false;
        public MovementPattern movement;
        public Color color = Color.White;
        protected float timeAlive;
        public float lifeSpan;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)movement.position.X, (int)movement.position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite(Dictionary<string, object> spriteProperties)
        {
            string textureName = (string)spriteProperties["textureName"];
            texture = TextureFactory.getTexture(textureName);

            movement = MovementPatternFactory.createMovementPattern((Dictionary<string, object>)spriteProperties["movementPattern"]);
            movement.origin = new Vector2(texture.Width / 2, texture.Height / 2); //orgin is based on texture
        }

        public virtual void Update(GameTime gametime, List<Sprite> sprits)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, movement.position, null, color, movement.rotation, movement.origin, 1, SpriteEffects.None, 0);
        }

        #region Collision
        protected bool IsTouchingLeft(Sprite sprite)
        {
            return this.Rectangle.Right + this.movement.velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite)
        {
            return this.Rectangle.Left + this.movement.velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite)
        {
            return this.Rectangle.Bottom + this.movement.velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite)
        {
            return this.Rectangle.Top + this.movement.velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
        #endregion
    }
}