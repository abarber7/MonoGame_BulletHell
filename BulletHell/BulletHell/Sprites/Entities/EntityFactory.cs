namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using BulletHell.Sprites.Attacks;
    using BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.PowerUps;
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

            Dictionary<string, object> movementPatternProperties = null;
            MovementPattern movement = null;
            if (entityProperties.ContainsKey("movementPattern"))
            {
                movementPatternProperties = (Dictionary<string, object>)entityProperties["movementPattern"];
                movement = MovementPatternFactory.CreateMovementPattern(movementPatternProperties);
            }

            string enemyType = (string)entityProperties["entityType"];
            string entityClassification = (string)entityProperties["entityType"] != "player" ? "enemy" : "player";

            int hp = Convert.ToInt32((float)entityProperties["hp"]);

            List<Attack> attacks = null;
            if (entityProperties["attacks"] is List<object>)
            {
                 attacks = AttackFactory.CreateAttacks((List<object>)entityProperties["attacks"]);
            }
            else if (entityProperties["attacks"] is Dictionary<string, object>)
            {
                attacks = AttackFactory.CreateAttacks((Dictionary<string, object>)entityProperties["attacks"]);
            }

            switch (entityClassification)
            {
                case "player":
                    entity = new Player(texture, color, movement, hp, attacks);
                    entity.SpawnPosition = new Vector2((float)movementPatternProperties["spawnXPosition"], (float)movementPatternProperties["spawnYPosition"]);
                    break;
                case "enemy":
                    PowerUp powerUp = PowerUpFactory.CreatePowerUp((Dictionary<string, object>)entityProperties["powerUp"]);

                    switch (enemyType)
                    {
                        case "simpleGrunt":
                            entity = new SimpleGrunt(texture, color, movement, powerUp, hp, attacks);
                            break;
                        case "complexGrunt":
                            entity = new ComplexGrunt(texture, color, movement, powerUp, hp, attacks);
                            break;
                        case "midBoss":
                            entity = new MidBoss(texture, color, movement, powerUp, hp, attacks);
                            break;
                        case "finalBoss":
                            List<Attack> phase2Attacks = null;
                            if (entityProperties["phase2Attacks"] is List<Dictionary<string, object>>)
                            {
                                phase2Attacks = AttackFactory.CreateAttacks((List<object>)entityProperties["phase2Attacks"]);
                            }
                            else if (entityProperties["phase2Attacks"] is Dictionary<string, object>)
                            {
                                phase2Attacks = AttackFactory.CreateAttacks((Dictionary<string, object>)entityProperties["phase2Attacks"]);
                            }

                            entity = new FinalBoss(texture, color, movement, powerUp, hp, attacks, phase2Attacks);
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
