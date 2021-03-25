namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using global::BulletHell.Sprites.Entities.Enemies.Concrete_Enemies;

    internal class EntityFactory
    {
        public static Entity CreateEntity(Dictionary<string, object> entityProperties)
        {
            Entity entity = null;
            switch (entityProperties["entityType"])
            {
                case "player":
                    entity = new Player(entityProperties);
                    break;
                case "exampleEnemy":
                    entity = new ExampleEnemy(entityProperties);
                    break;
                case "simpleGrunt":
                    entity = new SimpleGrunt(entityProperties);
                    break;
                case "complexGrunt":
                    entity = new ComplexGrunt(entityProperties);
                    break;
                case "midBoss":
                    entity = new MidBoss(entityProperties);
                    break;
                case "finalBoss":
                    entity = new FinalBoss(entityProperties);
                    break;
                default:
                    throw new Exception("Invalid Entity");
            }

            return entity;
        }
    }
}
