namespace BulletHell.States
{
    using System.Collections.Generic;
    using global::BulletHell.Game_Utilities;
    using global::BulletHell.Sprites;
    using global::BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using global::BulletHell.Sprites.Projectiles;
    using global::BulletHell.Sprites.The_Player;
    using global::BulletHell.Waves;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class GameState : State
    {
        private SpriteBatch spriteBatch;
        private List<Sprite> sprites;
        private List<Wave> waves;
        private double timeUntilNextWave = 0;
        private SpriteFont font;
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

                if (sprite is Player player)
                {
                    if (player.Invincible)
                    {
                        this.DrawBoxAroundSprite(player, Color.Crimson);
                    }

                    if (player.SlowMode)
                    {
                        this.DrawBoxAroundSprite(player, Color.White);
                        player.SlowMode = false;
                    }
                }
            }

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", this.lives), new Vector2(10, 10), Color.Black);

            this.spriteBatch.End();
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

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

            this.CustomPostUpdate(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
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
                this.timeUntilNextWave = this.waves[0].WaveDuration;
                this.waves[0].CreateWave(this.sprites);
                this.waves.RemoveRange(0, 1);
            }
        }

        private void DrawBoxAroundSprite(Sprite sprite, Color color)
        {
            Texture2D hitboxTexture = new Texture2D(this.game.GraphicsDevice, sprite.Rectangle.Width, sprite.Rectangle.Height);
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

        private void CustomPostUpdate(GameTime gameTime)
        {
            for (int i = this.sprites.Count - 1; i >= 0; i--)
            {
                if (this.sprites[i].IsRemoved)
                {
                    if (this.sprites[i] is Player)
                    {
                        this.lives--;
                        this.RemoveAllProjectiles();
                        ((Player)this.sprites[i]).Respawn(gameTime);
                    }
                    else if (this.sprites[i] is FinalBoss)
                    {
                        this.finalBossDefeated = true;
                        this.sprites.RemoveAt(i);
                    }
                    else
                    {
                        this.sprites.RemoveAt(i);
                    }
                }
            }
        }

        private void EndGamePrompt()
        {
            // TODO: Implement with Antonio's menu system.
            if (this.lives == 0)
            {
                this.game.ChangeState(new GameOverLose(this.game, this.graphicsDevice, this.content));
            }
            else if (this.finalBossDefeated == true)
            {
                this.game.ChangeState(new GameOverWin(this.game, this.graphicsDevice, this.content));
            }
        }

        private void CreateStats()
        {
            this.font = this.content.Load<SpriteFont>("Fonts/Font");
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
    }
}
