namespace BulletHell.Sprites.PowerUps
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps.Concrete_PowerUps;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class PowerUpFactory
    {
        public static PowerUp CreatePowerUp(Dictionary<string, object> powerUpProperties)
        {
            string textureName = (string)powerUpProperties["textureName"];
            Texture2D texture = TextureFactory.GetTexture(textureName);

            string colorName = (string)powerUpProperties["color"];
            Color color = System.Drawing.Color.FromName(colorName).ToXNA();

            MovementPattern movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)powerUpProperties["movementPattern"]);

            float dropPercent = (float)powerUpProperties["dropPercent"];

            PowerUp powerUp = powerUpProperties["powerUpType"] switch
            {
                "damageUp" => new DamageUp(texture, color, movement, dropPercent),
                "extraLife" => new ExtraLife(texture, color, movement, dropPercent),
                _ => throw new Exception("Invalid PowerUp Type"),
            };
            return powerUp;
        }
    }
}
