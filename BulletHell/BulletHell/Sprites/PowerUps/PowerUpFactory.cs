namespace BulletHell.Sprites.PowerUps
{
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps.Concrete_PowerUps;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class PowerUpFactory
    {
        public static PowerUp CreatePowerUp(Dictionary<string, object> powerUpProperties)
        {
            PowerUp powerUp = null;

            string textureName = (string)powerUpProperties["textureName"];
            Texture2D texture = TextureFactory.GetTexture(textureName);

            string colorName = (string)powerUpProperties["color"];
            Color color = System.Drawing.Color.FromName(colorName).ToXNA();

            MovementPattern movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)powerUpProperties["movementPattern"]);
            movement.Origin = new Vector2(texture.Width / 2, texture.Height / 2); // Orgin is based on texture

            double dropPercent = (double)powerUpProperties["dropPercent"];

            switch (powerUpProperties["powerUpType"])
            {
                case "damageUp":
                    powerUp = new DamageUp(texture, color, movement, dropPercent);
                    break;
                case "extraLife":
                    powerUp = new ExtraLife(texture, color, movement, dropPercent);
                    break;
                default:
                    throw new Exception("Invalid PowerUp Type");
            }

            return powerUp;
        }
    }
}
