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
        private Texture2D imageTexture2;
        private Texture2D imageTexture3;
        private Texture2D imageTexture4;


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
            this.imageTexture = TextureFactory.GetTexture("EndingScreen/EndingScreen1");
            this.imageTexture2 = TextureFactory.GetTexture("EndingScreen/EndingScreen2");
            this.imageTexture3 = TextureFactory.GetTexture("EndingScreen//EndingScreen3");
            this.imageTexture4 = TextureFactory.GetTexture("EndingScreen//EndingScreen4");

            images.Add(this.imageTexture);
            images.Add(this.imageTexture2);
            images.Add(this.imageTexture3);
            images.Add(this.imageTexture4);

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
                   /* if (KState.Clicked(this.skipButton))
                    {
                        this.screens[i].End();
                    }
                   */
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