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

            this.CreatePlayer();

            List<Dictionary<string, object>> listOfEntitiesToCreate = this.DemoEntites();

            foreach (Dictionary<string, object> entity in listOfEntitiesToCreate)
            {
                Sprite sprite = EntityFactory.CreateEntity(entity);
                this.sprites.Add(sprite);
            }
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in this.sprites.ToArray())
            {
                sprite.Update(gameTime, this.sprites);
            }

            this.PostUpdate();

            base.Update(gameTime);
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
                    { "speed", 4f },
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
                        { "speed", 8f },
                        }
                    },
                    }
                },
            };
            return playerProperties;
        }

        private void CreatePlayer()
        {
            Dictionary<string, object> playerProperties = this.PlayerProperties();
            Sprite sprite = EntityFactory.CreateEntity(playerProperties);
            this.sprites.Add(sprite);
        }

        private List<Dictionary<string, object>> DemoEntites()
        {
            List<Dictionary<string, object>> listOfEntitiesToCreate = new List<Dictionary<string, object>>();

            Dictionary<string, object> player = new Dictionary<string, object>()
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
                    { "speed", 4f },
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
                        { "speed", 8f },
                    }
                    },
                }
                },
            };

            Dictionary<string, object> enemy = new Dictionary<string, object>()
            {
                { "entityType", "exampleEnemy" },
                { "textureName", "Block" },
                { "color", "Red" },
                {
                    "movementPattern", new Dictionary<string, object>()
                    {
                    { "movementPatternType", "Static" },
                    { "xPosition", 100 },
                    { "yPosition", 100 },
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
                        { "speed", 4f },
                        }
                    },
                    }
                },
            };

            listOfEntitiesToCreate.Add(player);
            listOfEntitiesToCreate.Add(enemy);

            return listOfEntitiesToCreate;
        }

        private List<Wave> DemoWaves()
        {
            List<Wave> waves = new List<Wave>();

            Dictionary<string, object> wave1Properties = new Dictionary<string, object>()
            {
                { "waveNumber", 1 },
                { "waveDuration", 1000 },
                {
                    "EntityGroups", new List<Dictionary<string, object>>()
                    {
                    new Dictionary<string, object>()
                    {
                        { "EntityAmount", 1 },
                        {
                            "EntityProperties", new Dictionary<string, object>()
                            {
                            { "entityType", "ExampleEnemy" },
                            { "textureName", "Block" },
                            { "color", "Red" },
                            {
                                "movementPattern", new Dictionary<string, object>()
                                {
                                { "movementPatternType", "backAndForth" },
                                { "xStartPosition", 100 },
                                { "yStartPosition", 100 },
                                { "xEndPosition", 200 },
                                { "yEndPosition", 100 },
                                { "speed", 2f },
                                }
                            },
                            {
                                "projectile", new Dictionary<string, object>()
                            {
                                { "projectileType", "bullet" },
                                { "textureName", "Bullet" },
                                {
                                    "movementPattern", new Dictionary<string, object>()
                                    {
                                    { "movementPatternType", "linear" },
                                    { "xVelocity", 0 },
                                    { "yVelocity", 1 },
                                    { "speed", 4f },
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

            Wave wave1 = new Wave(wave1Properties);

            waves.Add(wave1);

            return waves;
        }
    }
}
