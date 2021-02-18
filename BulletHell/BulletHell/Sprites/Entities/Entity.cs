using BulletHell.Sprites.Movement_Patterns;
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
            //healthPoints = (int)entityProperties["healthPoints"];
            projectile = ProjectileFactory.createProjectile((Dictionary<string, object>)entityProperties["projectile"]);
        }

        protected void Attack(List<Sprite> sprites)
        {
            Projectile newProjectile = projectile.Clone() as Projectile;
            newProjectile.movement = projectile.movement.Clone() as MovementPattern;
            newProjectile.movement.position = this.movement.position;
            newProjectile.lifeSpan = 2f;
            newProjectile.parent = this;

            sprites.Add(newProjectile);
        }
    }
}
