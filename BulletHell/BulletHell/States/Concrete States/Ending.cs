namespace BulletHell.States
{
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
            this.Ssm = new SplashScreenManager(5, 10, 5, Keys.S);
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
