namespace BulletHell.States
{
    using System.Collections.Generic;
    using System.Linq;
    using BulletHell.Game_Utilities;
    using BulletHell.Sprites;
    using BulletHell.Sprites.Commands;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Sprites.The_Player;
    using BulletHell.Utilities;
    using BulletHell.Waves;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameState : State
    {
        private Player player;
        private List<Sprite> enemies;
        private List<Sprite> projectiles;

        private List<Wave> waves;
        private double timeUntilNextWave = 0;
        private SpriteFont font;
        private int lives = 3;
        private bool finalBossDefeated = false;
        private List<ICommand> commandQueue = null;

        public GameState()
        : base()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.spriteBatch.Begin();

            this.player.Draw(this.spriteBatch);

            if (this.player.Invincible)
            {
                this.DrawBoxAroundSprite(this.player, Color.Crimson);
            }

            if (this.player.SlowMode)
            {
                this.DrawBoxAroundSprite(this.player, Color.White);
                this.player.SlowMode = false;
            }

            foreach (var p in this.projectiles)
            {
                p.Draw(this.spriteBatch);
                this.DrawBoxAroundSprite(p, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            foreach (var e in this.enemies)
            {
                e.Draw(this.spriteBatch);
                this.DrawBoxAroundSprite(e, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", this.lives), new Vector2(10, 10), Color.Black);

            this.spriteBatch.End();
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            this.enemies = new List<Sprite>();

            this.projectiles = new List<Sprite>();

            this.commandQueue = new List<ICommand>();

            this.player = GameLoader.LoadPlayer();

            this.CreateWaves();

            this.CreateStats();
        }

        public override void Update(GameTime gameTime)
        {
            this.CheckAndDeployWave(gameTime);

            this.CreateCommands(gameTime); // Create fresh command queue

           // this.ExecuteCommands(); // Update sprites and check for collisions

            if (this.lives == 0 || this.finalBossDefeated)
            {
                this.EndGamePrompt();
            }
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
            this.ExecuteCommands();
            this.RemoveSprites(gameTime);
        }

        private void CreateCommands(GameTime gameTime)
        {
            // Create player update command
            this.commandQueue.Add(new UpdateCommand(this.player, gameTime, this.enemies));

            // Create enemy update commands
            this.enemies.ForEach((e) => { this.commandQueue.Add(new UpdateCommand(e, gameTime, this.enemies)); }); // Note: Enemy's Update does nothing with sprite list

            // Create projectile update commands
            this.projectiles.ForEach((p) => { this.commandQueue.Add(new UpdateCommand(p, gameTime, this.projectiles)); }); // Note: Projectile's Update does nothing with sprite list

            // Create player collision check command, using both enemies and projectiles to check against
            this.commandQueue.Add(new CollisionCheckCommand(this.player, this.enemies.Concat(this.projectiles).ToList())); // Did player hit any enemies or projectiles

            // Create enemy collision checks (purpose is to see if player projectiles hit any)
            this.enemies.ForEach((e) => { this.commandQueue.Add(new CollisionCheckCommand(e, this.projectiles)); }); // Did player projectiles hit any enemies
        }

        private void ExecuteCommands()
        {
            if (this.commandQueue != null)
            {
                this.commandQueue.ForEach((c) => { c.Execute(); });
            }
        }

        private void RemoveSprites(GameTime gameTime)
        {
            if (this.player.IsRemoved)
            {
                this.lives--;
                this.projectiles.Clear(); // Remove all projectiles
                this.player.Respawn(gameTime);
            }

            for (int i = this.enemies.Count - 1; i >= 0; i--)
            {
                if (this.enemies[i].IsRemoved)
                {
                    if (this.enemies[i] is FinalBoss)
                    {
                        this.finalBossDefeated = true;
                        this.enemies.RemoveAt(i);
                    }
                    else
                    {
                        this.enemies.RemoveAt(i);
                    }
                }
            }

            for (int i = this.projectiles.Count - 1; i >= 0; i--)
            {
                if (this.projectiles[i].IsRemoved)
                {
                    this.projectiles.RemoveAt(i);
                }
            }
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
                this.waves[0].CreateWave(this.enemies);
                this.waves.RemoveRange(0, 1);
            }
        }

        private void DrawBoxAroundSprite(Sprite sprite, Color color)
        {
            Texture2D hitboxTexture = new Texture2D(GraphicManagers.GraphicsDevice, sprite.Rectangle.Width, sprite.Rectangle.Height);
            Color[] data = new Color[sprite.Rectangle.Width * sprite.Rectangle.Height];
            for (int i = 0; i < data.Length; i++)
            {
                if (i < sprite.Rectangle.Width ||
                    i % sprite.Rectangle.Width == 0 ||
                    i % sprite.Rectangle.Width == sprite.Rectangle.Width - 1 ||
                    i > (sprite.Rectangle.Width * sprite.Rectangle.Height) - sprite.Rectangle.Width)
                {
                    data[i] = color;
                }
            }

            hitboxTexture.SetData(data);

            this.spriteBatch.Draw(hitboxTexture, new Vector2(sprite.Rectangle.Left, sprite.Rectangle.Top), color);
        }

        private void EndGamePrompt()
        {
            if (this.lives == 0)
            {
                StateManager.ChangeState(new GameOverLose());
            }
            else if (this.finalBossDefeated == true)
            {
                StateManager.ChangeState(new GameOverWin());
            }
        }

        private void CreateStats()
        {
            this.font = TextureFactory.GetSpriteFont("Fonts/Font");
        }
    }
}
