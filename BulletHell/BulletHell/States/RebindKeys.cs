﻿namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Controls;
    using global::BulletHell.States.Emitters;
    using global::BulletHell.The_Player;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class RebindKeys : State
    {
        private List<Component> components;
        private SnowEmitter snowEmitter;
        private SpriteBatch spriteBatch;
        private Texture2D configureControlsTexture;
        private KeyboardState preivousState;
        private bool rebinding = false;
        private string functionToRebind;
        private Button buttonToRebind;

        private Button upButton;
        private Button downButton;
        private Button leftButton;
        private Button rightButton;
        private Button attackButton;

        public RebindKeys(BulletHell game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = content.Load<Texture2D>("Controls/Button");
            var buttonFont = content.Load<SpriteFont>("Fonts/Font");
            this.configureControlsTexture = content.Load<Texture2D>("Titles/ConfigureControls");

            this.upButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(150, 150),
                Text = "Up | " + Input.Up.ToString(),
            };

            this.upButton.Click += this.UpButton_Click;

            this.downButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(150, 250),
                Text = "Down | " + Input.Down.ToString(),
            };

            this.downButton.Click += this.DownButton_Click;

            this.leftButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(50, 200),
                Text = "Left | " + Input.Left.ToString(),
            };

            this.leftButton.Click += this.LeftButton_Click;

            this.rightButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(250, 200),
                Text = "Right | " + Input.Right.ToString(),
            };

            this.rightButton.Click += this.RightButton_Click;

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Return to Main Menu",
            };

            returnButton.Click += this.ReturnButton_Click;

            this.attackButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 200),
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
            this.game.GraphicsDevice.Clear(Color.DarkGray);

            spriteBatch.Begin();
            spriteBatch.Draw(this.configureControlsTexture, new Vector2(-7, 0), Color.Black);

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
            // remove sprites if they're not needed
        }

        public override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.game.GraphicsDevice);

            this.snowEmitter = new SnowEmitter(new Emitters.SpriteLike(this.content.Load<Texture2D>("Particles/Snow")));
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

                // Check if key is already binded.
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
            this.game.ChangeState(new Options(this.game, this.graphicsDevice, this.content));
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            this.game.Exit();
        }
    }
}
