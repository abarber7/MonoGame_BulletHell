namespace BulletHell.States.Emitters
{
    using System;
    using Microsoft.Xna.Framework;

    public class SnowEmitter : Emitter
    {
        public static Random Random = new Random();

        public SnowEmitter(SpriteLike particle)
          : base(particle)
        {
        }

        protected override void ApplyGlobalVelocity()
        {
            var xSway = (float)Random.Next(-2, 2);
            foreach (var particle in this.particles)
            {
                particle.Velocity.X = (xSway * particle.Scale) / 50;
            }
        }

        protected override SpriteLike GenerateParticle()
        {
            var sprite = this.particlePrefab.Clone() as SpriteLike;

            var xPosition = Random.Next(0, GUI.ScreenWidth);
            var ySpeed = Random.Next(10, 100) / 100f;

            sprite.Position = new Vector2(xPosition, -sprite.Rectangle.Height);
            sprite.Opacity = (float)Random.NextDouble();
            sprite.Rotation = MathHelper.ToRadians(Random.Next(0, 360));
            sprite.Scale = (float)Random.NextDouble() + Random.Next(0, 3);
            sprite.Velocity = new Vector2(0, ySpeed);

            return sprite;
        }
    }
}
