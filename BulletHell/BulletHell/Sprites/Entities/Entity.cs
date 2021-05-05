namespace BulletHell.Sprites.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using BulletHell.Sprites.Entities.Enemies;
    using BulletHell.Sprites.Movement_Patterns;
    using BulletHell.States;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Entity : Sprite
    {
        public int HP;
        public List<Attack> Attacks = new List<Attack>();
        public Vector2 SpawnPosition;
        public Vector2 DespawnPosition;
        public bool ReachedStart = false; // bool for if entity reached start position
        public bool Exiting = false; // bool for if it is time to exit
        public float DamageModifier = 1.0F;
        public float DamageLevel;
        protected double points;
        protected bool initializedSpawningPosition = false;
        protected bool initializedDespawningPosition = false;
        protected bool initializedMovementPosition = false;
        private Vector2 positionWhenDespawningBegins;

        public Entity(Texture2D texture, Color color, MovementPattern movement, int hp, List<Attack> attacks)
            : base(texture, color, movement)
        {
            this.Attacks = attacks;
            this.HP = hp;
        }

        public virtual void LaunchAttack(object source, EventArgs args)
        {
            if (this.ReachedStart && !this.Exiting && !this.isRemoved)
            {
                Debug.WriteLineIf(this is Enemy, DateTime.Now + ": " + source.GetHashCode() + " is being launched.");
                Attack attackClone = (Attack)((Attack)source).Clone();
                attackClone.CooldownToAttack.Stop();

                attackClone.Movement.CurrentPosition = this.Movement.CurrentPosition;
                attackClone.Attacker = this;

                GameState.Attacks.Add(attackClone);
                attackClone.CooldownToCreateProjectile.Elapsed += attackClone.CreateProjectile;
                attackClone.CooldownToCreateProjectile.Start();
                Debug.WriteLineIf(this is Enemy, DateTime.Now + ": " + attackClone.GetHashCode() + " is starting the Cooldown to Create Projectile Timer for " + attackClone.GetHashCode());
            }
            else if (this.isRemoved)
            {
                (source as Attack).CooldownToAttack.Stop();
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
                newAttack.Attacker = newEntity;

                newAttack.ExecuteAttackEventHandler += newEntity.LaunchAttack;

                newAttacks.Add(newAttack);
            }

            newEntity.Attacks = newAttacks;

            return newEntity;
        }

        public void Respawn()
        {
            this.ReachedStart = false;
            this.initializedSpawningPosition = false;
        }

        public virtual double GetPoints()
        {
            return this.points;
        }

        protected virtual void Move()
        {
            // For spawning
            if (this.ReachedStart == false && this.Exiting == false)
            {
                if (this.initializedSpawningPosition == false)
                {
                    this.initializedSpawningPosition = true;
                    this.Movement.CurrentPosition = this.SpawnPosition;
                    this.Movement.CurrentSpeed = this.Movement.Speed * 2;
                    this.Movement.Velocity = MovementPattern.CalculateVelocity(this.SpawnPosition, this.Movement.StartPosition, this.Movement.CurrentSpeed);
                }

                if (this.Movement.ExceededPosition(this.SpawnPosition, this.Movement.StartPosition, this.Movement.Velocity))
                {
                    this.ReachedStart = true;
                }
                else
                {
                    this.Movement.CurrentPosition += this.Movement.Velocity;
                }
            }

            // For movement
            else if (this.ReachedStart == true && this.Exiting == false)
            {
                if (this.initializedMovementPosition == false)
                {
                    this.initializedMovementPosition = true;
                    this.Attacks.ForEach(item =>
                    {
                        item.Attacker = this;
                        item.CooldownToAttack.Start();
                        Debug.WriteLineIf(this is Enemy, DateTime.Now + ": " + item.Attacker.GetHashCode() + " is starting the Cooldown to Attack Timer for " + item.GetHashCode());
                    });

                    this.Movement.InitializeMovement();
                }

                if (this.Movement.CompletedMovement == true)
                {
                    this.Exiting = true;
                }
                else
                {
                    this.Movement.Move();
                }
            }

            // For despawning
            else if (this.ReachedStart == true && this.Exiting == true)
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

                    this.Movement.Velocity = MovementPattern.CalculateVelocity(this.Movement.CurrentPosition, this.DespawnPosition, this.Movement.CurrentSpeed);

                    this.Attacks.ForEach(item =>
                    {
                        item.CooldownToAttack.Stop();
                        Debug.WriteLineIf(this is Enemy, DateTime.Now + ": " + item.Attacker.GetHashCode() + " is stoping the Cooldown to Attack Timer for " + item.GetHashCode());
                    });
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
    }
}
