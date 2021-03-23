using BulletHell.Game_Utilities;
using BulletHell.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using global::BulletHell.Utilities;
using Microsoft.Xna.Framework.Input;

namespace BulletHell.States
{
    public class GameState : State
    {
        private SpriteBatch spriteBatch;
        private List<Sprite> sprites;
        private List<Wave> waves;
        private double timeUntilNextWave = 0;
        private State _currentState;

        private State _nextState;

        public GameState(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
        : base(game, graphicsDevice, content)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.spriteBatch.Begin();
            foreach (var sprite in this.sprites)
            {
                sprite.Draw(this.spriteBatch);

                if (sprite is Sprites.Entities.Player)
                {
                    Sprites.Entities.Player player = (Sprites.Entities.Player)sprite;
                    if (player.slowMode)
                    {
                        this.DrawBoxAroundSprite(player);
                        player.slowMode = false;
                    }
                }
            }
            this.spriteBatch.End();

        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            this.sprites = new List<Sprite>();

            this.CreatePlayer();

            this.CreateWaves();
        }

        public override void Update(GameTime gameTime)
        {

            this.CheckAndDeployWave(gameTime);

            foreach (var sprite in this.sprites.ToArray())
            {
                sprite.Update(gameTime, this.sprites);
            }

            this.PostUpdate();
        }

        private void CreatePlayer()
        {
            this.sprites.Add(GameLoader.LoadPlayer());
        }

        private void CreateWaves()
        {
            this.waves = GameLoader.LoadWaves();
        }

        private void CheckAndDeployWave(GameTime gameTime)
        {
            this.timeUntilNextWave -= gameTime.ElapsedGameTime.TotalSeconds;
            if (this.timeUntilNextWave <= 0 && this.waves.Count > 0)
            {
                this.timeUntilNextWave = this.waves[0].waveDuration;
                this.waves[0].CreateWave(this.sprites);
                this.waves.RemoveRange(0, 1);
            }
        }

        private void DrawBoxAroundSprite(Sprite sprite)
        {
            Texture2D hitboxTexture = new Texture2D(_game.GraphicsDevice, sprite.Rectangle.Width, sprite.Rectangle.Height);
            Color[] data = new Color[sprite.Rectangle.Width * sprite.Rectangle.Height];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < sprite.Rectangle.Width)
                {
                    data[i] = Color.White;
                }
                else if (i % sprite.Rectangle.Width == 0)
                {
                    data[i] = Color.White;
                }
                else if (i % sprite.Rectangle.Width == sprite.Rectangle.Width - 1)
                {
                    data[i] = Color.White;
                }
                else if (i > (sprite.Rectangle.Width * sprite.Rectangle.Height) - sprite.Rectangle.Width)
                {
                    data[i] = Color.White;
                }
            }

            hitboxTexture.SetData(data);

            this.spriteBatch.Draw(hitboxTexture, new Vector2(sprite.Movement.Position.X - (hitboxTexture.Width / 2), sprite.Movement.Position.Y - (hitboxTexture.Height / 2)), Color.White);
        }

        private void PostUpdate()
        {
            for (int i = this.sprites.Count - 1; i >= 0; i--)
            {
                if (this.sprites[i].IsRemoved)
                {
                    this.sprites.RemoveAt(i);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }
    }
}
