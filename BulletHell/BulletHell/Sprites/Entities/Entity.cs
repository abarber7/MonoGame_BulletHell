namespace BulletHell.Sprites.Entities
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Entity : Sprite
    {
        public int HP;
        public Vector2 SpawnPosition;
        public Vector2 DespawnPosition;
        protected double timer;
        protected float attackCooldown;
        protected Attack attack;
        private bool reachedStart = false; // bool for if entity reached start position
        private bool exiting = false; // bool for if it is time to exit
        private bool initializedSpawningPosition = false;
        private bool initializedDespawningPosition = false;
        private bool initializedMovementPosition = false;
        private Vector2 positionWhenDespawningBegins;

        protected Entity(Texture2D texture, Color color, MovementPattern movement, int hp, Attack attack, float attackCooldown)
            : base(texture, color, movement)
        {
            this.attack = attack;
            this.HP = hp;
            this.attackCooldown = attackCooldown;
        }

        protected void Attack(List<Sprite> sprites)
        {
             if (this.reachedStart && !this.exiting)
            {
                Attack attackClone = (Attack)this.attack.Clone();
                attackClone.Attacker = this;
                attackClone.Movement.CurrentPosition = this.GetCenterOfSprite();

                sprites.Add(attackClone);
            }
        }

        protected virtual void Move()
        {
            if (this.reachedStart == false && this.exiting == false)
            {
                if (this.initializedSpawningPosition == false)
                {
                    this.initializedSpawningPosition = true;
                    this.Movement.CurrentPosition = this.SpawnPosition;
                    this.Movement.CurrentSpeed = 5;
                    this.Movement.Velocity = this.Movement.CalculateVelocity(this.SpawnPosition, this.Movement.StartPosition, this.Movement.CurrentSpeed);
                }

                if (this.Movement.ExceededPosition(this.SpawnPosition, this.Movement.StartPosition, this.Movement.Velocity))
                {
                    this.reachedStart = true;
                }

                this.Movement.CurrentPosition += this.Movement.Velocity;
            }
            else if (this.reachedStart == true && this.exiting == false)
            {
                if (this.initializedMovementPosition == false)
                {
                    this.initializedMovementPosition = true;
                    this.Movement.CurrentPosition = this.Movement.StartPosition;
                    this.Movement.CurrentSpeed = this.Movement.Speed;
                    this.Movement.Velocity = this.Movement.CalculateVelocity(this.SpawnPosition, this.Movement.StartPosition, this.Movement.CurrentSpeed);
                }
            }
            else if (this.reachedStart == true && this.exiting == true)
            {
                if (this.initializedDespawningPosition == false)
                {
                    this.initializedDespawningPosition = true;

                    // In the case that a despawn position is not provided it goes to the spawn position
                    if (this.DespawnPosition.Equals(default(Vector2)))
                    {
                        this.DespawnPosition = this.SpawnPosition;
                    }

                    this.Movement.CurrentSpeed = 5;

                    this.Movement.Velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, this.DespawnPosition, this.Movement.CurrentSpeed);
                }

                if (this.Movement.ExceededPosition(this.positionWhenDespawningBegins, this.DespawnPosition, this.Movement.Velocity))
                {
                    this.isRemoved = true;
                }

                this.Movement.CurrentPosition += this.Movement.Velocity;
            }
        }
    }
}
