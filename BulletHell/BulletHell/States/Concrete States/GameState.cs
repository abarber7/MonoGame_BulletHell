namespace BulletHell.States
{
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
            return player.Rectangle.Center.ToVector2();
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

            foreach (var p in this.projectiles)
            {
                p.Draw(this.spriteBatch);

                this.DrawBoxAroundSprite(p, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            foreach (var e in this.enemies)
            {
                e.Draw(this.spriteBatch);

                // this.DrawBoxAroundSprite(e, Color.Chartreuse); // rectangle/hitbox visual TESTING
            }

            this.spriteBatch.DrawString(this.font, string.Format("Lives: {0}", player.Lives), new Vector2(10, 10), Color.Black);

            this.spriteBatch.End();
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            this.enemies = new List<Sprite>();

            this.projectiles = new List<Sprite>();

            this.attacks = new List<Sprite>();

            this.commandQueue = new List<ICommand>();

            player = GameLoader.LoadPlayer();

            this.CreateWaves();

            this.CreateStats();
        }

        public override void Update(GameTime gameTime)
        {
            this.CheckAndDeployWave(gameTime);

            this.CreateCommands(gameTime); // Create fresh command queue
            this.ExecuteCommands(); // Update sprites, check for collisions, clear queue

            if (player.Lives == 0 || this.finalBossDefeated)
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
            this.commandQueue.Add(new UpdateCommand(player, gameTime, this.projectiles));

            // Create enemy update commands
            this.enemies.ForEach((e) => { this.commandQueue.Add(new UpdateCommand(e, gameTime, this.attacks)); }); // projectiles used here as container where Attack() adds sprites

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
                this.commandQueue.ForEach((c) => { c.Execute(); });
                this.commandQueue.Clear();
            }
        }

        private void RemoveSprites(GameTime gameTime)
        {
            if (player.IsRemoved)
            {
                player.Lives--;
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
                        Enemy e = (Enemy)this.enemies[i];
                        if (e.DropLoot)
                        {
                            this.projectiles.Add(e.GetLoot()); // powerUp has a movement pattern, its update will just move it down
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
            if (player.Lives == 0)
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
