namespace BulletHell.States.Emitters
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Emitter : Component
    {
        /// <summary>
        /// How often a particle is produced
        /// </summary>
        public float GenerateSpeed = 0.005f;

        /// <summary>
        /// How often we apply the "GlobalVelociy" to our particles
        /// </summary>
        public float GlobalVelocitySpeed = 1;

        public int MaxParticles = 1000;

        protected SpriteLike particlePrefab;
        protected List<SpriteLike> particles;

        private float generateTimer;
        private float swayTimer;

        public Emitter(SpriteLike particle)
        {
            this.particlePrefab = particle;

            this.particles = new List<SpriteLike>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var particle in this.particles)
            {
                particle.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.generateTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.swayTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.AddParticle();

            if (this.swayTimer > this.GlobalVelocitySpeed)
            {
                this.swayTimer = 0;

                this.ApplyGlobalVelocity();
            }

            foreach (var particle in this.particles)
            {
                particle.Update(gameTime);
            }

            this.RemovedFinishedParticles();
        }

        protected abstract void ApplyGlobalVelocity();

        protected abstract SpriteLike GenerateParticle();

        private void AddParticle()
        {
            if (this.generateTimer > this.GenerateSpeed)
            {
                this.generateTimer = 0;

                if (this.particles.Count < this.MaxParticles)
                {
                    this.particles.Add(this.GenerateParticle());
                }
            }
        }

        private void RemovedFinishedParticles()
        {
            for (int i = 0; i < this.particles.Count; i++)
            {
                if (this.particles[i].IsRemoved)
                {
                    this.particles.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
