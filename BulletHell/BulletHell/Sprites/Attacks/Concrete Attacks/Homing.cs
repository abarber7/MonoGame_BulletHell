namespace BulletHell.Sprites.Attacks.Concrete_Attacks
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.Sprites.Projectiles;
    using BulletHell.States;
    using Microsoft.Xna.Framework;

    internal class Homing : Attack
    {
        public Homing(Projectile projectile)
            : base(projectile)
        {
        }

        public override void DoAttack(List<Sprite> sprites, Sprite parent)
        {
            Projectile newProjectile = this.projectile.Clone() as Projectile;
            newProjectile.Movement = this.projectile.Movement.Clone() as MovementPattern;
            newProjectile.Movement.Parent = newProjectile;

            Vector2 targetPosition = GameState.GetPlayerPosition();
            Vector2 velocity = parent.Movement.CalculateVelocity(parent.Rectangle.Center.ToVector2(), targetPosition, newProjectile.Movement.Speed);

            newProjectile.Movement.Velocity = velocity;
            newProjectile.Movement.Position = new Vector2(parent.Rectangle.Center.X, parent.Rectangle.Center.Y);
            newProjectile.Parent = parent;
            sprites.Add(newProjectile);
        }
    }
}
