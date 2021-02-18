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
            entity = entityProperties["entityType"] switch
            {
                "player" => new Player(entityProperties),
                "exampleEnemy" => new ExampleEnemy(entityProperties),
                "simpleGrunt" => new SimpleGrunt(entityProperties),
                "complexGrunt" => new ComplexGrunt(entityProperties),
                "midBoss" => new MidBoss(entityProperties),
                "finalBoss" => new FinalBoss(entityProperties),
                _ => throw new Exception("Invalid Entity"),
            };
            return entity;
        }
    }
}
