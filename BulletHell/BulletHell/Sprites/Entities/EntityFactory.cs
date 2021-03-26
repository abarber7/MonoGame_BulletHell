namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using global::BulletHell.Sprites.Movement_Patterns;
    using global::BulletHell.Sprites.Projectiles;
    using global::BulletHell.Sprites.The_Player;
    using global::BulletHell.Utilities;
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

            Projectile projectile = ProjectileFactory.CreateProjectile((Dictionary<string, object>)entityProperties["projectile"]);

            string enemyType = (string)entityProperties["entityType"];
            string entityClassification = (string)entityProperties["entityType"] != "player" ? "enemy" : "player";

            switch (entityClassification)
            {
                case "player":
                    entity = new Player(texture, color, movement, projectile);
                    break;
                case "enemy":
                    int lifeSpan = (int)entityProperties["lifeSpan"];

                    switch (enemyType)
                    {
                        case "exampleEnemy":
                            entity = new ExampleEnemy(texture, color, movement, projectile, lifeSpan);
                            break;
                        case "simpleGrunt":
                            entity = new SimpleGrunt(texture, color, movement, projectile, lifeSpan);
                            break;
                        case "complexGrunt":
                            entity = new ComplexGrunt(texture, color, movement, projectile, lifeSpan);
                            break;
                        case "midBoss":
                            entity = new MidBoss(texture, color, movement, projectile, lifeSpan);
                            break;
                        case "finalBoss":
                            entity = new FinalBoss(texture, color, movement, projectile, lifeSpan);
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

            entity.Projectile.Movement.Parent = entity;

            return entity;
        }
    }
}
