namespace BulletHell.States
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    internal class StateManager
    {
        public static EventHandler ExitEvent;

        private static State currentState;
        private static State nextState;

        public static State CurrentState { get => currentState; set => currentState = value;  }

        public static void ChangeState(State state)
        {
            nextState = state;
        }

        public static void Update(GameTime gameTime)
        {
            SwitchToMenu();
            SwitchToNextState();
            CurrentState.Update(gameTime);
            CurrentState.PostUpdate(gameTime);
        }

        private static void SwitchToMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                currentState = new MenuState();
                nextState = null;
                currentState.LoadContent();
            }
        }

        private static void SwitchToNextState()
        {
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
                currentState.LoadContent();
            }
        }
    }
}
