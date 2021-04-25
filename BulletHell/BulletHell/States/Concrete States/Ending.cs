namespace BulletHell.States
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BulletHell.Ending;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class EndingState : State
    {
        public SplashScreenManager Ssm;

        public EndingState()
            : base()
        {
        }

        public object GraphicsDevice { get; private set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GraphicManagers.GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            this.Ssm.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Draw(GameTime gameTime)
        {
        }

        public override void LoadContent()
        {
            // SPLASH SCREEN
            this.Ssm = new SplashScreenManager(3, 5 , 3, Keys.Space);
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
                this.Ssm.Update(gameTime);
                if (!this.Ssm.Running)
                {
                    StateManager.ChangeState(new GameOverWin());
                }
        }
    }
}
