namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BulletHell.Game_Utilities;
    using BulletHell.Sprites;
    using BulletHell.Sprites.Commands;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using BulletHell.Sprites.The_Player;
    using BulletHell.Utilities;
    using BulletHell.Waves;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class GameState : State
    {
        private static Player player;
        private List<Sprite> enemies;
        private List<Sprite> projectiles;
        private List<Sprite> attacks;
        private List<Wave> waves;
        private List<SpawnableSprite> enemiesToSpawn;
        private double timeUntilNextWave = 0;
        private SpriteFont font;
        private bool finalBossDefeated = false;
        private List<ICommand> commandQueue = null;

        public GameState()
        : base()
        {
        }

        public static Vector2 GetPlayerPosition()
        {
            return player.GetCenterOfSprite();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.spriteBatch.Begin();

            player.Draw(this.spriteBatch);

            if (player.Invincible)
            {
                this.DrawBoxAroundSprite(player, Color.Crimson);
            }

            if (player.SlowMode)
            {
                this.DrawBoxAroundSprite(player, Color.White);
                player.SlowMode = false;
            }

            foreach (var projectile in this.projectiles)
            {
                projectile.Draw(this.spriteBatch);

                this.DrawBoxAroundSprite(projectile, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            foreach (var enemy in this.enemies.ToArray())
            {
                enemy.Draw(this.spriteBatch);

                this.DrawBoxAroundSprite(enemy, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", player.HP), new Vector2(10, 10), Color.Black);

            this.spriteBatch.End();
        }

        public override void LoadContent()
        {
            this.InitializeLists();
            this.LoadPlayer();
            this.LoadWaves();
            this.CreateStats();
        }

        private void InitializeLists()
        {
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            this.enemies = new List<Sprite>();

            this.projectiles = new List<Sprite>();

            this.attacks = new List<Sprite>();

            this.enemiesToSpawn = new List<SpawnableSprite>();

            this.commandQueue = new List<ICommand>();
        }

        public override void Update(GameTime gameTime)
        {
            this.CheckAndDeployWave(gameTime);

            this.CreateCommands(gameTime); // Create fresh command queue
            this.ExecuteCommands(); // Update sprites, check for collisions, clear queue

            if (player.HP == 0 || this.finalBossDefeated)
            {
                this.EndGamePrompt();
            }
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void PostUpdate(GameTime gameTime)
        {
            this.RemoveSprites(gameTime);
        }

        private void CreateCommands(GameTime gameTime)
        {
            // Create player update command
            this.commandQueue.Add(new UpdateCommand(player, gameTime, this.attacks));

            // Create enemy update commands
            this.enemies.ForEach((e) => { this.commandQueue.Add(new UpdateCommand(e, gameTime, this.attacks)); }); // attacks used here as container where enemy's Attack() adds sprites

            // Create attack update commands
            this.attacks.ForEach((a) => { this.commandQueue.Add(new UpdateCommand(a, gameTime, this.projectiles)); }); // attacks add projectiles

            // Create projectile update commands
            this.projectiles.ForEach((p) => { this.commandQueue.Add(new UpdateCommand(p, gameTime, this.projectiles)); }); // Note: Projectile's Update does nothing with sprite list

            // Create player collision check command, using both enemies and projectiles to check against
            this.commandQueue.Add(new CollisionCheckCommand(player, this.enemies.Concat(this.projectiles).ToList())); // Did player hit any enemies or projectiles

            // Create enemy collision checks (purpose is to see if player projectiles hit any)
            this.enemies.ForEach((e) => { this.commandQueue.Add(new CollisionCheckCommand(e, this.projectiles)); }); // Did player projectiles hit any enemies
        }

        private void ExecuteCommands()
        {
            if (this.commandQueue != null)
            {
                this.commandQueue.ForEach((command) => { command.Execute(); });
                this.commandQueue.Clear();
            }
        }

        private void RemoveSprites(GameTime gameTime)
        {
            if (player.IsRemoved)
            {
                player.HP--;
                this.projectiles.Clear(); // Remove all projectiles
                player.Respawn(gameTime);
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
                        Enemy enemy = (Enemy)this.enemies[i];
                        if (enemy.DropLoot)
                        {
                            this.projectiles.Add(enemy.GetLoot()); // powerUp has a movement pattern, its update will just move it down
                        }

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

            for (int i = this.attacks.Count - 1; i >= 0; i--)
            {
                if (this.attacks[i].IsRemoved)
                {
                    this.attacks.RemoveAt(i);
                }
            }
        }

        /*private void OnSpriteRemoval(object sender, GameComponentCollectionEventArgs e)
        {
            if (sender == this.player)
            {
                this.lives--;
                this.projectiles.Clear(); // Remove all projectiles
                this.player.Respawn(gameTime); // TODO: set up sender so that gameTime comes in args ?
            }
            else if (sender is Enemy enemy)
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
            else if (sender is Projectile projectile)
            {
                this.projectiles.RemoveAt(i);
            }
        }*/

        private void LoadWaves()
        {
            this.waves = GameLoader.LoadWaves();
        }

        private void LoadPlayer()
        {
            player = GameLoader.LoadPlayer();
        }

        private void CheckAndDeployWave(GameTime gameTime)
        {
            this.timeUntilNextWave -= gameTime.ElapsedGameTime.TotalSeconds;
            List<SpawnableSprite> spritesToSpawn = new List<SpawnableSprite>();
            if (this.timeUntilNextWave <= 0 && this.waves.Count > 0)
            {
                this.timeUntilNextWave = this.waves[0].WaveDuration;
                spritesToSpawn = this.waves[0].CreateWave();
                this.waves.RemoveRange(0, 1);
            }

            spritesToSpawn.ForEach(item => this.enemiesToSpawn.Add(item));

            spritesToSpawn.ForEach(item =>
            {
                item.TimeToSpawn += this.SpawnEnemies;
                item.GetTimer().Start();
            });
        }

        private void SpawnEnemies(object source, EventArgs e)
        {
            SpawnableSprite enemyToSpawn = (SpawnableSprite)source;
            this.enemiesToSpawn.Remove(enemyToSpawn);
            this.enemies.Add(enemyToSpawn.GetSprite());
        }

        private void DrawBoxAroundSprite(Sprite sprite, Color color)
        {
            Texture2D lineTexture = new Texture2D(GraphicManagers.GraphicsDevice, 1, 1);
            lineTexture.SetData(new Color[] { Color.White }); // fill the texture with white

            Vector2 topLeft = new Vector2(sprite.Rectangle.Left, sprite.Rectangle.Top);
            Vector2 bottomLeft = new Vector2(sprite.Rectangle.Left, sprite.Rectangle.Bottom);
            Vector2 topRight = new Vector2(sprite.Rectangle.Right, sprite.Rectangle.Top);
            Vector2 bottomRight = new Vector2(sprite.Rectangle.Right, sprite.Rectangle.Bottom);
            this.DrawLine(lineTexture, topLeft, topRight, color); // top edge
            this.DrawLine(lineTexture, topLeft, bottomLeft, color); // left edge
            this.DrawLine(lineTexture, bottomLeft, bottomRight, color); // bottom edge
            this.DrawLine(lineTexture, topRight, bottomRight, color); // right edge
        }

        // Source: https://gamedev.stackexchange.com/a/44016
        private void DrawLine(Texture2D lineTexture, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X); // calculate angle to rotate line (for left/right sides)

            this.spriteBatch.Draw(
                lineTexture,
                new Rectangle( // rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), // spriteBatch will strech the texture to fill this rectangle
                    1), // width of line, change this to make thicker line
                null, // source rectangle N/A
                color, // color of line
                angle, // angle of line (calulated above)
                Vector2.Zero, // point in line about which to rotate
                SpriteEffects.None,
                0);
        }

        private void EndGamePrompt()
        {
            if (player.HP == 0)
            {
                StateManager.ChangeState(new GameOverLose());
            }
            else if (this.finalBossDefeated == true)
            {
                StateManager.ChangeState(new EndingState());
            }
        }

        private void CreateStats()
        {
            this.font = TextureFactory.GetSpriteFont("Fonts/Font");
        }
    }
}
