namespace BulletHell
{
    using System.Collections.Generic;
    using global::BulletHell.Sprites;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BulletHell : Game
    {
        private SpriteBatch spriteBatch;
        private List<Sprite> sprites;
        private List<Wave> waves;
        private double timeUntilNextWave = 0;

        // Initialize screensize and other game properties
        public BulletHell()
        {
            Graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            UtlilityManager.Initialize(this.Content);
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

            this.waves = new List<Wave>();

            this.CreatePlayer();

            // For individual entities
             this.CreateEnemies();

            // For waves
            // this.CreateWaves();

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
            }

            this.spriteBatch.End();

            base.Draw(gameTime);
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
            Dictionary<string, object> playerProperties = this.PlayerProperties();
            Sprite sprite = EntityFactory.CreateEntity(playerProperties);
            this.sprites.Add(sprite);
        }

        private Dictionary<string, object> PlayerProperties()
        {
            Dictionary<string, object> playerProperties = new Dictionary<string, object>()
            {
                { "entityType", "player" },
                { "textureName", "Block" },
                { "color", "Blue" },
                {
                    "movementPattern", new Dictionary<string, object>()
                    {
                    { "movementPatternType", "playerInput" },
                    { "xPosition", 200 },
                    { "yPosition", 200 },
                    { "speed", 4 },
                    }
                },
                {
                    "projectile", new Dictionary<string, object>()
                    {
                    { "projectileType", "bullet" },
                    { "textureName", "Bullet" },
                    { "color", "Blue" },
                    {
                        "movementPattern", new Dictionary<string, object>()
                        {
                        { "movementPatternType", "linear" },
                        { "xVelocity", 0 },
                        { "yVelocity", -1 },
                        { "speed", 8 },
                        }
                    },
                    }
                },
            };
            return playerProperties;
        }

        private void CreateEnemies()
        {
            List<Dictionary<string, object>> listOfEnemiesToProperties = this.EnemyProperties();
            foreach (Dictionary<string, object> entityProperties in listOfEnemiesToProperties)
            {
                Sprite sprite = EntityFactory.CreateEntity(entityProperties);
                this.sprites.Add(sprite);
            }
        }

        private List<Dictionary<string, object>> EnemyProperties()
        {
            List<Dictionary<string, object>> listOfEnemiesToProperties = new List<Dictionary<string, object>>();

            Dictionary<string, object> enemy = new Dictionary<string, object>()
            {
                { "entityType", "finalBoss" },
                { "textureName", "Block" },
                { "color", "Red" },
                { "lifeSpan", 30 },
                {
                    "movementPattern", new Dictionary<string, object>()
                    {
                    { "movementPatternType", "pattern" },
                    {
                        "points", new List<List<int>>()
                        {
                            new List<int>() { 200, 200 },
                            new List<int>() { 300, 300 },
                            new List<int>() { 200, 300 },
                            new List<int>() { 300, 200 },
                        }
                    },
                    { "speed", 250 },
                    }
                },
                {
                    "projectile", new Dictionary<string, object>()
                    {
                    { "projectileType", "bounceBullet" },
                    { "textureName", "Bullet" },
                    { "color", "Red" },
                    { "bounceTimes", 1 },
                    {
                        "movementPattern", new Dictionary<string, object>()
                        {
                        { "movementPatternType", "bounce" },
                        { "xVelocity", 0 },
                        { "yVelocity", 0 },
                        { "xPosition", 0 },
                        { "yPosition", 0 },
                        { "speed", 3 },
                        }
                    },
                    }
                },
            };

            listOfEnemiesToProperties.Add(enemy);
            return listOfEnemiesToProperties;
        }

        private void CreateWaves()
        {
            List<Dictionary<string, object>> listOfWaveProperties = this.WaveProperties();

            foreach (Dictionary<string, object> waveProperties in listOfWaveProperties)
            {
                Wave wave = new Wave(waveProperties);
                this.waves.Add(wave);
            }
        }

        private List<Dictionary<string, object>> WaveProperties()
        {
            List<Dictionary<string, object>> listOfWaveProperties = new List<Dictionary<string, object>>();

            Dictionary<string, object> wave1Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 1 },
                { "waveDuration", 5 },
                {
                    "entityGroups", new List<Dictionary<string, object>>()
                    {
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "exampleEnemy" },
                            { "textureName", "Block" },
                            { "color", "Red" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 150 },
                                { "yStartPosition", 230 },
                                { "xEndPosition", 250 },
                                { "yEndPosition", 300 },
                                { "speed", 100 },
                                }
                            },
                            {
                                "projectile", new Dictionary<string, object>()
                            {
                                { "projectileType", "bullet" },
                                { "textureName", "Bullet" },
                                { "color", "Red" },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "linear" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 1 },
                                    { "speed", 4 },
                                    }
                                },
                            }
                            },
                            }
                        },
                    },
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "exampleEnemy" },
                            { "textureName", "Block" },
                            { "color", "Red" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 150 },
                                { "yStartPosition", 430 },
                                { "xEndPosition", 250 },
                                { "yEndPosition", 300 },
                                { "speed", 100 },
                                }
                            },
                            {
                                "projectile", new Dictionary<string, object>()
                            {
                                { "projectileType", "bullet" },
                                { "textureName", "Bullet" },
                                { "color", "Red" },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "linear" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 1 },
                                    { "speed", 4 },
                                    }
                                },
                            }
                            },
                            }
                        },
                    },
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "exampleEnemy" },
                            { "textureName", "Block" },
                            { "color", "Red" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 150 },
                                { "yStartPosition", 100 },
                                { "xEndPosition", 400 },
                                { "yEndPosition", 100 },
                                { "speed", 100 },
                                }
                            },
                            {
                                "projectile", new Dictionary<string, object>()
                            {
                                { "projectileType", "bullet" },
                                { "textureName", "Bullet" },
                                { "color", "Red" },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "linear" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 1 },
                                    { "speed", 4 },
                                    }
                                },
                            }
                            },
                            }
                        },
                    },
                    }
                },
            };

            listOfWaveProperties.Add(wave1Properties);

            Dictionary<string, object> wave2Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 2 },
                { "waveDuration", 5 },
                {
                    "entityGroups", new List<Dictionary<string, object>>()
                    {
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "exampleEnemy" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 5 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "Static" },
                                { "xPosition", 300 },
                                { "yPosition", 100 },
                                }
                            },
                            {
                                "projectile", new Dictionary<string, object>()
                            {
                                { "projectileType", "bullet" },
                                { "textureName", "Bullet" },
                                { "color", "Green" },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "linear" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 1 },
                                    { "speed", 4 },
                                    }
                                },
                            }
                            },
                            }
                        },
                    },
                    }
                },
            };

            //listOfWaveProperties.Add(wave2Properties);

            return listOfWaveProperties;
        }
    }
}
