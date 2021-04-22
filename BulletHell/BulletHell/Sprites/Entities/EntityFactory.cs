namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BulletHell.Sprites.Attacks;
    using BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.Sprites.The_Player;
    using BulletHell.Utilities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class EntityFactory
    {
        public static Entity CreateEntity(Dictionary<string, object> entityProperties)
        {
            Entity entity = null;
            string textureName = (string)entityProperties["textureName"];
            Texture2D texture = TextureFactory.GetTexture(textureName);

            string colorName = (string)entityProperties["color"];
            Color color = System.Drawing.Color.FromName(colorName).ToXNA();

            MovementPattern movement;
            if (entityProperties.ContainsKey("movementPattern"))
            {
                movement = MovementPatternFactory.CreateMovementPattern((Dictionary<string, object>)entityProperties["movementPattern"]);
                movement.Origin = new Vector2(texture.Width / 2, texture.Height / 2); // Orgin is based on texture
            }
            else
            {
                movement = null;
            }

            string enemyType = (string)entityProperties["entityType"];
            string entityClassification = (string)entityProperties["entityType"] != "player" ? "enemy" : "player";

            int hp = Convert.ToInt32((float)entityProperties["hp"]);
            Attack attack = AttackFactory.CreateAttack((Dictionary<string, object>)entityProperties["attack"]);
            float attackCooldown = (float)entityProperties["attackCooldown"];

            switch (entityClassification)
            {
                case "player":
                    entity = new Player(texture, color, movement, hp, attack, attackCooldown);
                    break;
                case "enemy":
                    int lifeSpan = Convert.ToInt32((float)entityProperties["lifeSpan"]);
                    PowerUp powerUp = PowerUpFactory.CreatePowerUp((Dictionary<string, object>)entityProperties["powerUp"]);

                    switch (enemyType)
                    {
                        case "simpleGrunt":
                            entity = new SimpleGrunt(texture, color, movement, powerUp, lifeSpan, hp, attack, attackCooldown);
                            break;
                        case "complexGrunt":
                            entity = new ComplexGrunt(texture, color, movement, powerUp, lifeSpan, hp, attack, attackCooldown);
                            break;
                        case "midBoss":
                            entity = new MidBoss(texture, color, movement, powerUp, lifeSpan, hp, attack, attackCooldown);
                            break;
                        case "finalBoss":
                            entity = new FinalBoss(texture, color, movement, powerUp, lifeSpan, hp, attack, attackCooldown);
                            break;
                    }

                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            if (entity.Movement != null)
            {
                entity.Movement.Parent = entity;
            }

            return entity;
        }
    }
}
