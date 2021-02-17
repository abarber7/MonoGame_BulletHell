using BulletHell.Sprites.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Sprites.Entities
{
    internal abstract class Entity : Sprite
    {
        //private int healthPoints;
        public Projectile projectile;
        public ushort attackSpeed = 1;

        protected Entity(Dictionary<string, object> entityProperties) : base(entityProperties)
        {
            int xPosition = (int)entityProperties["xPosition"];
            int yPosition = (int)entityProperties["yPosition"];
            position = new Vector2(xPosition, yPosition);
            //healthPoints = (int)entityProperties["healthPoints"];
            projectile = ProjectileFactory.createProjectile((Dictionary<string, object>)entityProperties["projectile"]);
        }

        protected void Attack(List<Sprite> sprites)
        {
            var newProjectile = projectile.Clone() as Projectile;
            newProjectile.velocity = new Vector2(0, -1);
            newProjectile.position = this.position;
            newProjectile.speed = this.speed;
            newProjectile.lifeSpan = 2f;
            newProjectile.parent = this;

            sprites.Add(newProjectile);
        }
    }
}
