namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using BulletHell.Game_Utilities;
    using BulletHell.Sprites;
    using BulletHell.Sprites.Commands;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using BulletHell.Sprites.Projectiles.Concrete_Projectiles;
    using BulletHell.Sprites.The_Player;
    using BulletHell.States.Concrete_States.GameState;
    using BulletHell.Utilities;
    using BulletHell.Waves;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public class GameState : State
    {
        public static List<Sprite> Attacks = new List<Sprite>();
        public static Player Player;
        public static List<Sprite> Enemies = new List<Sprite>();
        public static List<Sprite> Projectiles = new List<Sprite>();
        private List<Wave> waves = new List<Wave>();
        private List<SpawnableSprite> enemiesToSpawn = new List<SpawnableSprite>();
        private readonly object enemiesToSpawnLock = new object();
        private double timeUntilNextWave = 0;
        private SpriteFont font;
        private bool finalBossDefeated = false;
        private List<ICommand> commandQueue = null;
        private List<Song> song = new List<Song>();
        private Song song1 = TextureFactory.Content.Load<Song>("Songs/battle song");
        private Song song2 = TextureFactory.Content.Load<Song>("Songs/battle song + anxiety");
        private ScrollingBackground background1;
        private ScrollingBackground background2;

        public GameState()
        : base()
        {
        }

        public static Vector2 GetPlayerPosition()
        {
            return Player.GetCenterOfSprite();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.spriteBatch.Begin();

            this.background1.Draw(this.spriteBatch);
            this.background2.Draw(this.spriteBatch);

            Player.Draw(this.spriteBatch);

            if (Player.Invincible)
            {
                this.DrawBoxAroundSprite(Player, Color.Crimson);
            }

            if (Player.SlowMode)
            {
                this.DrawBoxAroundSprite(Player, Color.White);
                Player.SlowMode = false;
            }

            foreach (var projectile in Projectiles.FindAll(item => item != null))
            {
                projectile.Draw(this.spriteBatch);

                // this.DrawBoxAroundSprite(projectile, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            foreach (var enemy in Enemies.ToArray())
            {
                enemy.Draw(this.spriteBatch);

                // this.DrawBoxAroundSprite(enemy, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", Player.HP), new Vector2(20, 15), Color.DarkRed, 0, new Vector2(10, 10), 1.5f, SpriteEffects.None, 0);
            this.spriteBatch.DrawString(this.font, string.Format("{0:C}", Player.GetPoints()), new Vector2(20, 35), Color.PaleGreen, 0, new Vector2(10, 10), 1.5f, SpriteEffects.None, 0);

            this.spriteBatch.End();
        }

        public override void LoadContent()
        {
            this.InitializeLists();
            this.LoadPlayer();
            this.LoadWaves();
            this.CreateStats();

            this.song.Add(this.song1);
            this.song.Add(this.song2);
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(this.song[0]);
            MediaPlayer.MediaStateChanged += this.MediaPlayer_MediaStateChanged;

            this.background1 = new ScrollingBackground(new Rectangle(0, -720, 480, 720));
            this.background2 = new ScrollingBackground(new Rectangle(0, 0, 480, 720));
        }

        public void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Play(this.song[1]);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.background1.Rectangle.Y + this.background1.Texture.Height >= 720)
            {
                this.background1.Rectangle.Y = -720;
            }

            if (this.background2.Rectangle.Y + this.background2.Texture.Height >= 720)
            {
                this.background2.Rectangle.Y = this.background1.Rectangle.Y + this.background1.Texture.Height;
            }

            this.background1.Update();
            this.background2.Update();

            this.CheckAndDeployWave(gameTime);

            this.CreateCommands(gameTime); // Create fresh command queue
            this.ExecuteCommands(); // Update sprites, check for collisions, clear queue

            if (Player.HP == 1 && MediaPlayer.Queue.ActiveSong != this.song[1])
            {
                MediaPlayer.Play(this.song[1]);
            }
            else if (Player.HP > 1 && MediaPlayer.Queue.ActiveSong == this.song[1])
            {
                MediaPlayer.Play(this.song[0]); // no more panic song
            }

            if (Player.HP == 0 || this.finalBossDefeated)
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

        private void InitializeLists()
        {
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            Enemies = new List<Sprite>();

            Projectiles = new List<Sprite>();

            Attacks = new List<Sprite>();

            this.enemiesToSpawn = new List<SpawnableSprite>();

            this.commandQueue = new List<ICommand>();
        }

        private void CreateCommands(GameTime gameTime)
        {
            // Create player update command
            this.commandQueue.Add(new UpdateCommand(Player, gameTime, GameState.Attacks));

            // Create enemy update commands
            Enemies.ToArray().ToList().ForEach((e) => { this.commandQueue.Add(new UpdateCommand(e, gameTime, Attacks)); }); // attacks used here as container where enemy's Attack() adds sprites

            // Create attack update commands
            Attacks.ToArray().ToList().ForEach((a) => { this.commandQueue.Add(new UpdateCommand(a, gameTime, Projectiles)); }); // attacks add projectiles

            // Create projectile update commands
            Projectiles.ToArray().ToList().ForEach((p) => { this.commandQueue.Add(new UpdateCommand(p, gameTime, Projectiles)); }); // Note: Projectile's Update does nothing with sprite list

            List<Sprite> enemiesAndProjectileList = Enemies.Concat(Projectiles).ToList();

            // Create player collision check command, using both enemies and projectiles to check against
            this.commandQueue.Add(new CollisionCheckCommand(Player, enemiesAndProjectileList)); // Did player hit any enemies or projectiles

            // Create enemy collision checks (purpose is to see if player projectiles hit any)
            Enemies.ToArray().ToList().ForEach((enemy) => { this.commandQueue.Add(new CollisionCheckCommand(enemy, Projectiles.ToArray().ToList())); }); // Did player projectiles hit any enemies

            // Create projectile collision checks (purpose for the moment is check projectile on projectile collision for PushBullet
            List<Sprite> projectiles = Projectiles.ToArray().ToList();

            projectiles.ForEach((projectile) =>
            {
                // Optimize by only setting up checks when PushBullet is involved
                if (projectile is PushBullet)
                {
                    this.commandQueue.Add(new CollisionCheckCommand(projectile, projectiles));
                }
            });
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
            if (Player.IsRemoved)
            {
                Player.HP--;
                Projectiles.Clear(); // Remove all projectiles
                Player.Respawn(gameTime);
            }

            for (int i = Enemies.Count - 1; i >= 0; i--)
            {
                if (Enemies[i].IsRemoved)
                {
                    Enemy enemy = (Enemy)Enemies[i];
                    Player.IncreasePoints(enemy.GetPoints());

                    if (Enemies[i] is FinalBoss)
                    {
                        this.finalBossDefeated = true;
                        Enemies.RemoveAt(i);
                    }
                    else
                    {
                        if (enemy.DropLoot)
                        {
                            Projectiles.Add(enemy.GetLoot()); // powerUp has a movement pattern, its update will just move it down
                        }

                        Enemies.RemoveAt(i);
                    }
                }
            }

            for (int i = Projectiles.Count - 1; i >= 0; i--)
            {
                if (Projectiles[i].IsRemoved)
                {
                    Projectiles.RemoveAt(i);
                }
            }

            for (int i = Attacks.Count - 1; i >= 0; i--)
            {
                if (Attacks[i].IsRemoved)
                {
                    Attacks.RemoveAt(i);
                }
            }
        }

        private void LoadWaves()
        {
            this.waves = GameLoader.LoadWaves();
        }

        private void LoadPlayer()
        {
            Player = GameLoader.LoadPlayer();
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
            lock (this.enemiesToSpawnLock)
            {
                SpawnableSprite enemyToSpawn = (SpawnableSprite)source;
                this.enemiesToSpawn.Remove(enemyToSpawn);
                Enemies.Add(enemyToSpawn.GetSprite());
            }
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
            if (Player.HP == 0)
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
