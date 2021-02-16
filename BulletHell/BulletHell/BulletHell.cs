using BulletHell.Sprites;
using BulletHell.Sprites.Entities;
using BulletHell.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BulletHell
{
    public class BulletHell : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<Sprite> _sprites;

        // Initialize screensize and other game properties
        public BulletHell()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            UtlilityManager.Initialize(this.Content);
        }

        // Set any values that weren't set in the constructor for BulletHell
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        // Load in content (sprites, assets, etc.)
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var playerTexture = Content.Load<Texture2D>("Block");

            _sprites = new List<Sprite>();

            List<Dictionary<string, object>> listOfEntitiesToCreate = this.demoEntites();

            foreach (Dictionary<string, object> entity in listOfEntitiesToCreate)
            {
                Sprite sprite = EntityFactory.createEntity(entity);
                _sprites.Add(sprite);
            }
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
            {
                sprite.Update(gameTime, _sprites);
            }

            base.Update(gameTime);
        }


        // This is called when the game should draw itself
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private List<Dictionary<string, object>> demoEntites()
        {
            List<Dictionary<string, object>> listOfEntitiesToCreate = new List<Dictionary<string, object>>();

            Dictionary<string, object> player1 = new Dictionary<string, object>(){
                { "entityType", "player" },
                { "textureName", "Block" },
                { "color", "Blue" },
                { "xPosition", 200 },
                { "yPosition", 200 }
            };

            listOfEntitiesToCreate.Add(player1);

            Dictionary<string, object> player2 = new Dictionary<string, object>(){
                { "entityType", "player" },
                { "textureName", "Block" },
                { "color", "Red" },
                { "xPosition", 100 },
                { "yPosition", 100 }
            };

            listOfEntitiesToCreate.Add(player2);

            return listOfEntitiesToCreate;
        }
    }
}
