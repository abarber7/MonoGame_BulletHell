namespace BulletHell.Ending
{
    using System;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public class SplashScreen
    {
        private Status currentStatus;
        private Texture2D image;
        private Color color;
        private byte fade;
        private float timer;
        private float fadeInTime;
        private float waitTime;
        private float fadeOutTime;
        private Song song;

        public SplashScreen(Texture2D image, float fadeInTime, float waitTime, float fadeOutTime)
        {
            float min = 0.1f;
            this.image = image;
            this.fadeInTime = Math.Max(fadeInTime, min);
            this.waitTime = Math.Max(waitTime, min);
            this.fadeOutTime = Math.Max(fadeOutTime, min);
            this.Prepare();
            this.song = TextureFactory.Content.Load<Song>("Songs/ending song");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(this.song);
        }

        public enum Status
        {
            Ready,
            FadingIn,
            Waiting,
            FadingOut,
            NotReady,
        }

        public Status CurrentStatus
        {
            get { return this.currentStatus; }
        }

        private float StartToWaitTime
        {
            get { return this.fadeInTime; }
        }

        private float StartToFadeOutTime
        {
            get { return this.fadeInTime + this.waitTime; }
        }

        private float StartToEndTime
        {
            get { return this.fadeInTime + this.waitTime + this.fadeOutTime; }
        }

        public void Prepare()
        {
            this.fade = 0;
            this.timer = 0;
            this.color = new Color(this.fade, this.fade, this.fade);
            this.currentStatus = Status.Ready;
        }

        public void Update(GameTime gt)
        {
            // CALCULATE ALPHA & status
            if (this.timer < this.StartToWaitTime)
            {
                this.fade = (byte)((byte.MaxValue * this.timer) / this.StartToWaitTime);
                if (this.currentStatus != Status.FadingIn)
                {
                    this.currentStatus = Status.FadingIn;
                }
            }
            else if (this.timer < this.StartToFadeOutTime)
            {
                if (this.color.A < byte.MaxValue)
                {
                    this.color.A = byte.MaxValue;
                }

                if (this.currentStatus != Status.Waiting)
                {
                    this.currentStatus = Status.Waiting;
                }
            }
            else if (this.timer < this.StartToEndTime)
            {
                this.fade = (byte)(byte.MaxValue - ((byte.MaxValue * (this.timer - this.StartToFadeOutTime)) / this.fadeOutTime));
                if (this.currentStatus != Status.FadingOut)
                {
                    this.currentStatus = Status.FadingOut;
                }
            }
            else
            {
                this.fade = byte.MinValue;
                if (this.currentStatus != Status.NotReady)
                {
                    this.currentStatus = Status.NotReady;
                }
            }

            // UPDATE COLOR AND TIME
            this.color = new Color(this.fade, this.fade, this.fade);
            this.timer += (float)gt.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch sp)
        {
            sp.Draw(this.image, default(Vector2), this.color);
        }

        public void End()
        {
            this.currentStatus = Status.NotReady;
        }
    }
}