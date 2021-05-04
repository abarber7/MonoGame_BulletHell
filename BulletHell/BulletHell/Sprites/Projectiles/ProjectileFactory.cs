namespace BulletHell.Sprites.Projectiles
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles.Concrete_Projectiles;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class ProjectileFactory
    {
        public static Projectile CreateProjectile(Dictionary<string, object> projectileProperties)
        {
            Projectile projectile = null;

            string textureName = (string)projectileProperties["textureName"];
            Texture2D texture = TextureFactory.GetTexture(textureName);

            string colorName = (string)projectileProperties["color"];
            Color color = System.Drawing.Color.FromName(colorName).ToXNA();

            MovementPattern movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)projectileProperties["movementPattern"]);

            int damage = Convert.ToInt32((float)projectileProperties["damage"]);

            switch (projectileProperties["projectileType"])
            {
                case "bullet":
                    projectile = new Bullet(texture, color, movement, damage);
                    break;
                case "bouncingBullet":
                    projectile = new BouncingBullet(texture, color, movement, damage);
                    break;
                case "bounceBullet":
                    int numberOfTimesToBounce = (int)projectileProperties["bounceTimes"];
                    projectile = new BounceBullet(texture, color, movement, damage, numberOfTimesToBounce);
                    break;
                case "pushBullet":
                    projectile = new PushBullet(texture, color, movement, damage);
                    break;
                default:
                    throw new Exception("Invalid Projectile Type");
            }

            return projectile;
        }
    }
}
