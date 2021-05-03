namespace BulletHell.Sprites
{
    using System;
    using System.Timers;

    internal class SpawnableSprite
    {
        private Sprite spriteToSpawn;
        private Timer timerTillSpawn;

        public SpawnableSprite(Sprite spriteToSpawn, Timer timerTillSpawn)
        {
            this.spriteToSpawn = spriteToSpawn;
            this.timerTillSpawn = timerTillSpawn;
            this.timerTillSpawn.Elapsed += this.TimerElapsed;
            this.timerTillSpawn.AutoReset = false;
        }

        public event EventHandler TimeToSpawn;

        public Sprite GetSprite()
        {
            return this.spriteToSpawn;
        }

        public Timer GetTimer()
        {
            return this.timerTillSpawn;
        }

        public void TimerElapsed(object source, ElapsedEventArgs args)
        {
            this.TimeToSpawn.Invoke(this, null);
        }
    }
}
