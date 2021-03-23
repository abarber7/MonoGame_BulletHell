namespace BulletHell
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using global::BulletHell.Game_Utilities;
    using global::BulletHell.Sprites;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using global::BulletHell.Sprites.Projectiles;
    using global::BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BulletHell : Game
    {
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private List<Sprite> sprites;
        private List<Wave> waves;
        private double timeUntilNextWave = 0;
        private int lives = 3;
        private bool finalBossDefeated = false;

        // Initialize screensize and other game properties
        public BulletHell()
        {
            Graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            UtlilityManager.Initialize(this.Content);
            GameLoader.LoadGameDictionary("Test");
        }

        public static GraphicsDeviceManager Graphics { get; set; }

        // Set any values that weren't set in the constructor for BulletHell
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        // Load in content (sprites, assets, etc.)
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.sprites = new List<Sprite>();

            this.CreatePlayer();

            this.CreateStats();

            this.CreateWaves();
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            this.CheckAndDeployWave(gameTime);

            foreach (var sprite in this.sprites.ToArray())
            {
                sprite.Update(gameTime, this.sprites);
            }

            this.PostUpdate();

            if (this.lives == 0 || this.finalBossDefeated)
            {
                this.EndGamePrompt();
            }

            base.Update(gameTime);
        }

        // This is called when the game should draw itself
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

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

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", this.lives), new Vector2(10, 10), Color.Black);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBoxAroundSprite(Sprite sprite)
        {
            Texture2D hitboxTexture = new Texture2D(Graphics.GraphicsDevice, sprite.Rectangle.Width, sprite.Rectangle.Height);
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
                    // Some namespace issue, thinks Bullethell.Player is a namespace
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

            }
            else
            {

            }
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

        private void CreatePlayer()
        {
            this.sprites.Add(GameLoader.LoadPlayer());
        }

        private void CreateStats()
        {
            this.font = this.Content.Load<SpriteFont>("Font");
        }

        private void CreateWaves()
        {
            this.waves = GameLoader.LoadWaves();
        }
    }
}
