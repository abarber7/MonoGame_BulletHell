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
using BulletHell.Sprites.Projectiles;
using BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;

namespace BulletHell.States
{
    public class GameState : State
    {
        private SpriteBatch spriteBatch;
        private List<Sprite> sprites;
        private List<Wave> waves;
        private double timeUntilNextWave = 0;
        private SpriteFont font;
        private State _currentState;
        private State _nextState;
        private int lives = 3;
        private bool finalBossDefeated = false;

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
                    if (player.invicible)
                    {
                        this.DrawBoxAroundSprite(player, Color.Crimson);
                    }

                    if (player.slowMode)
                    {
                        this.DrawBoxAroundSprite(player, Color.White);
                        player.slowMode = false;
                    }
                }
            }

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", this.lives), new Vector2(10, 10), Color.Black);


            this.spriteBatch.End();

        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            this.sprites = new List<Sprite>();

            this.CreatePlayer();

            this.CreateWaves();

            this.CreateStats();
        }

        public override void Update(GameTime gameTime)
        {

            this.CheckAndDeployWave(gameTime);

            foreach (var sprite in this.sprites.ToArray())
            {
                sprite.Update(gameTime, this.sprites);
            }

            if (this.lives == 0 || this.finalBossDefeated)
            {
                this.EndGamePrompt();
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

        private void DrawBoxAroundSprite(Sprite sprite, Color color)
        {
            Texture2D hitboxTexture = new Texture2D(_game.GraphicsDevice, sprite.Rectangle.Width, sprite.Rectangle.Height);
            Color[] data = new Color[sprite.Rectangle.Width * sprite.Rectangle.Height];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < sprite.Rectangle.Width)
                {
                    data[i] = color;
                }
                else if (i % sprite.Rectangle.Width == 0)
                {
                    data[i] = color;
                }
                else if (i % sprite.Rectangle.Width == sprite.Rectangle.Width - 1)
                {
                    data[i] = color;
                }
                else if (i > (sprite.Rectangle.Width * sprite.Rectangle.Height) - sprite.Rectangle.Width)
                {
                    data[i] = color;
                }
            }

            hitboxTexture.SetData(data);

            this.spriteBatch.Draw(hitboxTexture, new Vector2(sprite.Movement.Position.X - (hitboxTexture.Width / 2), sprite.Movement.Position.Y - (hitboxTexture.Height / 2)), color);
        }

        private void PostUpdate()
        {
            for (int i = this.sprites.Count - 1; i >= 0; i--)
            {
                if (this.sprites[i].IsRemoved)
                {
                    if (this.sprites[i] is Sprites.Entities.Player)
                    {
                        this.lives--;
                        this.RemoveAllProjectiles();
                        this.CreatePlayer();
                    }
                    else if (this.sprites[i] is FinalBoss)
                    {
                        this.finalBossDefeated = true;
                    }

                    this.sprites.RemoveAt(i);
                }
            }
        }
        private void EndGamePrompt()
        {
            // TODO: Implement with Antonio's menu system.
            if (this.lives == 0)
            {
                _game.ChangeState(new GameOverLose(_game, _graphicsDevice, _content));

            }
            else if (this.finalBossDefeated = true)
            {
                _game.ChangeState(new GameOverWin(_game, _graphicsDevice, _content));

            }
        }

        private void CreateStats()
        {
            this.font = this._content.Load<SpriteFont>("Fonts/Font");
        }


        private void RemoveAllProjectiles()
        {
            for (int i = this.sprites.Count - 1; i >= 0; i--)
            {
                if (this.sprites[i] is Projectile)
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
