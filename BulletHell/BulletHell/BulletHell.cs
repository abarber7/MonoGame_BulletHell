namespace BulletHell
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using global::BulletHell.Sprites;
    using global::BulletHell.Sprites.Entities;
    using global::BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BulletHell : Game
    {
        private Sprites.Entities.Player player;
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

            // toggle
            if (this.player.slowMode == true)
            {
                Texture2D x = this.Content.Load<Texture2D>("Bullet");
                this.spriteBatch.Draw(x, this.player.Rectangle, Color.White);
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
            this.player = (Sprites.Entities.Player)EntityFactory.CreateEntity(playerProperties);
            this.sprites.Add(this.player);
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
                    { "xPosition", Graphics.PreferredBackBufferWidth / 2 },
                    { "yPosition", Graphics.PreferredBackBufferHeight },
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
            Random r = new Random();
            int randX1 = r.Next(100, 400);
            int randY1 = r.Next(50, 200);
            int randX2 = r.Next(100, 400);
            int randY2 = r.Next(50, 200);

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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 0 },
                                { "yStartPosition", 50 },
                                { "xEndPosition", 800 },
                                { "yEndPosition", 50 },
                                { "speed", 800 },
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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 800 },
                                { "yStartPosition", 50 },
                                { "xEndPosition", 0 },
                                { "yEndPosition", 50 },
                                { "speed", 800 },
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
            randX1 = r.Next(100, 400);
            randY1 = r.Next(50, 200);
            randX2 = r.Next(100, 400);
            randY2 = r.Next(50, 200);

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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 0 },
                                { "yStartPosition", 50 },
                                { "xEndPosition", 800 },
                                { "yEndPosition", 50 },
                                { "speed", 800 },
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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 800 },
                                { "yStartPosition", 50 },
                                { "xEndPosition", 0 },
                                { "yEndPosition", 50 },
                                { "speed", 800 },
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

            listOfWaveProperties.Add(wave2Properties);

            Dictionary<string, object> wave3Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 3 },
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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 0 },
                                { "yStartPosition", 50 },
                                { "xEndPosition", 800 },
                                { "yEndPosition", 50 },
                                { "speed", 800 },
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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 800 },
                                { "yStartPosition", 50 },
                                { "xEndPosition", 0 },
                                { "yEndPosition", 50 },
                                { "speed", 800 },
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

            listOfWaveProperties.Add(wave3Properties);

            return listOfWaveProperties;
        }
    }
}
