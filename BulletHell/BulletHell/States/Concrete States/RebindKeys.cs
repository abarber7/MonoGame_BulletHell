namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Controls;
    using BulletHell.States.Emitters;
    using BulletHell.The_Player;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class RebindKeys : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private Texture2D configureControlsTexture;
        private Texture2D configureControlsTexture2;
        private KeyboardState preivousState;
        private bool rebinding = false;
        private string functionToRebind;
        private Button buttonToRebind;

        private Button upButton;
        private Button downButton;
        private Button leftButton;
        private Button rightButton;
        private Button attackButton;

        public RebindKeys()
          : base()
        {
            var buttonTexture = TextureFactory.Content.Load<Texture2D>("Controls/Button");
            var buttonFont = TextureFactory.Content.Load<SpriteFont>("Fonts/Font");
            this.configureControlsTexture = TextureFactory.Content.Load<Texture2D>("Titles/CONFIGURE");
            this.configureControlsTexture2 = TextureFactory.Content.Load<Texture2D>("Titles/CONTROLS");

            this.upButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(82, 357),
                Text = "Up | " + Input.Up.ToString(),
            };

            this.upButton.Click += this.UpButton_Click;

            this.downButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(82, 444),
                Text = "Down | " + Input.Down.ToString(),
            };

            this.downButton.Click += this.DownButton_Click;

            this.leftButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(2, 400),
                Text = "Left | " + Input.Left.ToString(),
            };

            this.leftButton.Click += this.LeftButton_Click;

            this.rightButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(165, 400),
                Text = "Right | " + Input.Right.ToString(),
            };

            this.rightButton.Click += this.RightButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(161, 600),
                Text = "Return to Main Menu",
            };

            returnButton.Click += this.ReturnButton_Click;

            this.attackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(318, 500),
                Text = "Attack | " + Input.Attack.ToString(),
            };

            this.attackButton.Click += this.AttackButton_Click;

            this.components = new List<Component>()
              {
                this.upButton,
                this.downButton,
                this.leftButton,
                this.rightButton,
                this.attackButton,
                returnButton,
              };
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GraphicManagers.GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();
            spriteBatch.Draw(this.configureControlsTexture, new Vector2(20, 50), Color.Black);
            spriteBatch.Draw(this.configureControlsTexture2, new Vector2(40, 120), Color.Black);

            foreach (var component in this.components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            this.snowEmitter.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in this.components)
            {
                if (this.rebinding)
                {
                    this.GetNewKey();
                }

                component.Update(gameTime);
            }

            this.snowEmitter.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicManagers.GraphicsDevice);

            this.snowEmitter = new SnowEmitter(new SpriteLike(TextureFactory.GetTexture("Particles/Snow")));
        }

        public override void Draw(GameTime gameTime)
        {
        }

        private void GetNewKey()
        {
            KeyboardState newState = Keyboard.GetState();
            if (this.preivousState.GetPressedKeyCount() == 0 && newState.GetPressedKeyCount() == 1)
            {
                Keys newKey = newState.GetPressedKeys()[0];

                // Check if key is already bound
                if (Input.CheckIfAlreadyBinded(newKey) == false)
                {
                    Input.SetKey(this.functionToRebind, newKey);
                    this.buttonToRebind.Text = this.functionToRebind + " | " + newKey.ToString();

                    this.rebinding = false;
                    this.functionToRebind = string.Empty;
                    this.buttonToRebind = null;
                }
            }

            this.preivousState = newState;
        }

        private void UpButton_Click(object sender, EventArgs e)
        {
            if (!this.rebinding)
            {
                this.rebinding = true;
                this.functionToRebind = "Up";
                this.buttonToRebind = sender as Button;
                this.buttonToRebind.Text = this.functionToRebind + " | Rebinding";
            }
        }

        private void DownButton_Click(object sender, EventArgs e)
        {
            if (!this.rebinding)
            {
                this.rebinding = true;
                this.functionToRebind = "Down";
                this.buttonToRebind = sender as Button;
                this.buttonToRebind.Text = this.functionToRebind + " | Rebinding";
            }
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            if (!this.rebinding)
            {
                this.rebinding = true;
                this.functionToRebind = "Left";
                this.buttonToRebind = sender as Button;
                this.buttonToRebind.Text = this.functionToRebind + " | Rebinding";
            }
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            if (!this.rebinding)
            {
                this.rebinding = true;
                this.functionToRebind = "Right";
                this.buttonToRebind = sender as Button;
                this.buttonToRebind.Text = this.functionToRebind + " | Rebinding";
            }
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            if (!this.rebinding)
            {
                this.rebinding = true;
                this.functionToRebind = "Attack";
                this.buttonToRebind = sender as Button;
                this.buttonToRebind.Text = this.functionToRebind + " | Rebinding";
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            StateManager.ChangeState(new Options());
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            StateManager.ExitEvent(null, e);
        }
    }
}
