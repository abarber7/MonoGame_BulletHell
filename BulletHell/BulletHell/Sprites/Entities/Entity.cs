namespace BulletHell.Sprites.Entities
{
    using System.Collections.Generic;
    using BulletHell.Sprites.Attacks;
    using BulletHell.Sprites.Movement_Patterns;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Entity : Sprite
    {
        public int HP;
        public List<Attack> Attacks = new List<Attack>();
        public Vector2 SpawnPosition;
        public Vector2 DespawnPosition;
        protected double timer;
        protected float attackCooldown;
        private bool reachedStart = false; // bool for if entity reached start position
        private bool exiting = false; // bool for if it is time to exit
        private bool initializedSpawningPosition = false;
        private bool initializedDespawningPosition = false;
        private bool initializedMovementPosition = false;
        private Vector2 positionWhenDespawningBegins;

        public void Respawn()
        {
            this.reachedStart = false;
            this.initializedSpawningPosition = false;
        }

        public Entity(Texture2D texture, Color color, MovementPattern movement, int hp, List<Attack> attacks, float attackCooldown)
            : base(texture, color, movement)
        {
            this.Attacks = attacks;
            this.HP = hp;
            this.attackCooldown = attackCooldown;
        }

        protected void ExecuteAttack(List<Sprite> sprites)
        {
            if (this.reachedStart && !this.exiting)
            {
                Attack attackClone = AttackFactory.DownCastAttack(this.Attacks[0].Clone());
                attackClone.Movement.CurrentPosition = this.Movement.CurrentPosition;
                attackClone.Attacker = this;

                sprites.Add(attackClone);
            }
        }

        protected virtual void Move()
        {
            // For spawning
            if (this.reachedStart == false && this.exiting == false)
            {
                if (this.initializedSpawningPosition == false)
                {
                    this.initializedSpawningPosition = true;
                    this.Movement.CurrentPosition = this.SpawnPosition;
                    this.Movement.CurrentSpeed = this.Movement.Speed * 2;
                    this.Movement.Velocity = this.Movement.CalculateVelocity(this.SpawnPosition, this.Movement.StartPosition, this.Movement.CurrentSpeed);
                }

                if (this.Movement.ExceededPosition(this.SpawnPosition, this.Movement.StartPosition, this.Movement.Velocity))
                {
                    this.reachedStart = true;
                }
                else
                {
                    this.Movement.CurrentPosition += this.Movement.Velocity;
                }
            }

            // For movement
            else if (this.reachedStart == true && this.exiting == false)
            {
                if (this.initializedMovementPosition == false)
                {
                    this.initializedMovementPosition = true;
                    this.Movement.InitializeMovement();
                }

                if (this.Movement.CompletedMovement == true)
                {
                    this.exiting = true;
                }
                else
                {
                    this.Movement.Move();
                }
            }

            // For despawning
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

                    this.Movement.CurrentSpeed = this.Movement.Speed * 2;
                    this.positionWhenDespawningBegins = this.Movement.CurrentPosition;

                    this.Movement.Velocity = this.Movement.CalculateVelocity(this.Movement.CurrentPosition, this.DespawnPosition, this.Movement.CurrentSpeed);
                }

                if (this.Movement.ExceededPosition(this.positionWhenDespawningBegins, this.DespawnPosition, this.Movement.Velocity))
                {
                    this.isRemoved = true;
                }
                else
                {
                    this.Movement.CurrentPosition += this.Movement.Velocity;
                }
            }
        }

        public override object Clone()
        {
            Entity newEntity = (Entity)this.MemberwiseClone();
            if (this.Movement != null)
            {
                MovementPattern newMovement = (MovementPattern)this.Movement.Clone();
                newEntity.Movement = newMovement;
            }

            List<Attack> newAttacks = new List<Attack>();

            foreach (Attack attack in this.Attacks)
            {
                Attack newAttack = (Attack)attack.Clone();
                newAttacks.Add(newAttack);
            }

            newEntity.Attacks = newAttacks;

            return newEntity;
        }
    }
}
