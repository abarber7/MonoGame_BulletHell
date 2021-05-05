namespace BulletHell.Ending
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class SplashScreenManager
    {
        private List<SplashScreen> screens;
        private Keys skipButton;
        private Texture2D imageTexture;

        public SplashScreenManager()
            : this(new List<SplashScreen>(), Keys.None)
        {
        }

        public SplashScreenManager(List<SplashScreen> screens, Keys skipButton)
        {
            this.screens = screens;
            this.skipButton = skipButton;
            this.Prepare();
        }

        public SplashScreenManager(float fadeIn, float wait, float fadeOut, Keys skipButton)
        {
            List<Texture2D> images = new List<Texture2D>();
            this.imageTexture = TextureFactory.GetTexture("FinalEndScreen");

            images.Add(this.imageTexture);

            this.screens = new List<SplashScreen>();
            foreach (Texture2D t in images)
            {
                this.screens.Add(new SplashScreen(t, fadeIn, wait, fadeOut));
            }

            this.skipButton = skipButton;
        }

        public bool Running
        {
            get
            {
                foreach (SplashScreen s in this.screens)
                {
                    if (s.CurrentStatus != SplashScreen.Status.NotReady)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void Update(GameTime gt)
        {
            for (int i = 0; i < this.screens.Count(); i++)
            {
                if (this.screens[i].CurrentStatus != SplashScreen.Status.NotReady)
                {
                    this.screens[i].Update(gt);
                    if (Keyboard.GetState().IsKeyDown(Keys.Home))
                    {
                        this.screens[i].End();
                    }

                    break;
                }
            }
        }

        public void Prepare()
        {
            foreach (SplashScreen s in this.screens)
            {
                s.Prepare();
            }
        }

        public void Draw(SpriteBatch sp)
        {
            for (int i = 0; i < this.screens.Count(); i++)
            {
                if (this.screens[i].CurrentStatus != SplashScreen.Status.NotReady)
                {
                    this.screens[i].Draw(sp);
                    break;
                }
            }
        }
    }
}