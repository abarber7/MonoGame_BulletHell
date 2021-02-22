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
            // this.CreateEnemies();

            // For waves
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
            this.sprites.Add(EntityFactory.CreateEntity(playerProperties));
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
                        { "speed", 15 },
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
                { "entityType", "complexGrunt" },
                { "textureName", "Block" },
                { "color", "Red" },
                { "lifeSpan", 30 },
                {
                    "movementPattern", new Dictionary<string, object>()
                    {
                        { "movementPatternType", "semicircle" },
                        { "xStartPosition", 50 },
                        { "yStartPosition", 30 },
                        { "xEndPosition", 100 },
                        { "yEndPosition", 30 },
                        { "half1Or2", false },
                        { "speed", 10 },
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
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 25 },
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
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 25 },
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

            listOfWaveProperties.Add(wave2Properties);

            Dictionary<string, object> wave3Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 3 },
                { "waveDuration", 20 },
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
                            { "lifeSpan", 20 },
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
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "simpleGrunt" },
                            { "textureName", "Block" },
                            { "color", "Green" },
                            { "lifeSpan", 20 },
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

            listOfWaveProperties.Add(wave3Properties);

            Dictionary<string, object> wave4Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 2 },
                { "waveDuration", 30 },
                {
                    "entityGroups", new List<Dictionary<string, object>>()
                    {
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "midBoss" },
                            { "textureName", "Block" },
                            { "color", "White" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "bounce" },
                                { "xVelocity", -1 },
                                { "yVelocity", -1 },
                                { "xPosition", 0 },
                                { "yPosition", 0 },
                                { "speed", 5 },
                                }
                            },
                            {
                                "projectile", new Dictionary<string, object>()
                                {
                                { "projectileType", "bounceBullet" },
                                { "textureName", "Bullet" },
                                { "color", "White" },
                                { "bounceTimes", 1 },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "bounce" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 0 },
                                    { "xPosition", 0 },
                                    { "yPosition", 0 },
                                    { "speed", 10 },
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

            listOfWaveProperties.Add(wave4Properties);

            Dictionary<string, object> wave5Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 2 },
                { "waveDuration", 30 },
                {
                    "entityGroups", new List<Dictionary<string, object>>()
                    {
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                                { "entityType", "complexGrunt" },
                                { "textureName", "Block" },
                                { "color", "Red" },
                                { "lifeSpan", 30 },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                        { "movementPatternType", "semicircle" },
                                        { "xStartPosition", 50 },
                                        { "yStartPosition", 30 },
                                        { "xEndPosition", 100 },
                                        { "yEndPosition", 30 },
                                        { "half1Or2", false },
                                        { "speed", 10 },
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
                            }
                        },
                    },
                    }
                },
            };

            listOfWaveProperties.Add(wave5Properties);

            Dictionary<string, object> wave6Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 6 },
                { "waveDuration", 30 },
                {
                    "entityGroups", new List<Dictionary<string, object>>()
                    {
                    new Dictionary<string, object>()
                    {
                        { "entityAmount", 1 },
                        {
                            "entityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "finalBoss" },
                            { "textureName", "Block" },
                            { "color", "Black" },
                            { "lifeSpan", 30 },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "pattern" },
                                {
                                    "points", new List<List<int>>()
                                    {
                                        new List<int>() { 600, 100 },
                                        new List<int>() { 600, 200 },
                                        new List<int>() { 100, 200 },
                                        new List<int>() { 100, 100 },
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
                                { "color", "Black" },
                                { "bounceTimes", 2 },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "bounce" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 0 },
                                    { "xPosition", 0 },
                                    { "yPosition", 0 },
                                    { "speed", 2 },
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

            listOfWaveProperties.Add(wave6Properties);

            return listOfWaveProperties;
        }
    }
}
