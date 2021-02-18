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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<Sprite> sprites;

        // Initialize screensize and other game properties
        public BulletHell()
        {
            graphics = new GraphicsDeviceManager(this);
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites = new List<Sprite>();

            List<Dictionary<string, object>> listOfEntitiesToCreate = this.demoEntites();

            foreach (Dictionary<string, object> entity in listOfEntitiesToCreate)
            {
                Sprite sprite = EntityFactory.createEntity(entity);
                sprites.Add(sprite);
            }
        }

        // Update is called 60 times per second (60 FPS). Put all game logic here.
        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in sprites.ToArray())
            {
                sprite.Update(gameTime, sprites);
            }

            this.PostUpdate();

            base.Update(gameTime);
        }


        private void PostUpdate()
        {
            for (int i = sprites.Count - 1; i > 0; i--)
            {
                if(sprites[i].isRemoved)
                {
                    sprites.RemoveAt(i);
                }
            }
        }

        // This is called when the game should draw itself
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private List<Dictionary<string, object>> demoEntites()
        {
            List<Dictionary<string, object>> listOfEntitiesToCreate = new List<Dictionary<string, object>>();

            Dictionary<string, object> player = new Dictionary<string, object>(){
                { "entityType", "player" },
                { "textureName", "Block" },
                { "color", "Blue" },
                { "movementPattern", new Dictionary<string, object>(){
                    {"movementPatternType", "playerInput" },
                    { "xPosition", 200 },
                    { "yPosition", 200 },
                    { "speed", 4f } }
                },
                { "projectile", new Dictionary<string, object>() {
                    { "projectileType", "bullet" },
                    { "textureName", "Bullet" },
                    { "movementPattern", new Dictionary<string, object>() {
                        {"movementPatternType", "linear" },
                        { "xVelocity", 0 },
                        { "yVelocity", -1 },
                        { "speed", 8f } } 
                    } 
                }
                }
            };

            listOfEntitiesToCreate.Add(player);

            return listOfEntitiesToCreate;
        }
    }
}
