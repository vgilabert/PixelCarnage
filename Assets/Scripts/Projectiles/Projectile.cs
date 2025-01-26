using System;
using System.Collections.Generic;
using Enemy;
using Shared;
using StatSystem;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        protected readonly HashSet<Damageable> TargetsHit = new ();
        
        protected int Damage;
        protected float Speed;
        protected float LifeTime;
        protected float Force;
        private Vector3 _lastPosition;

        protected abstract void Initialize();
        
        public virtual void SetUserStats(StatsData stats)
        {
            Damage = stats[StatType.Attack].Value;
            Speed = 20f;
            LifeTime = 2f;
            Force = 0;
        }
        
        public virtual void SetUserStats(EnemyStats stats)
        {
            Damage = stats.Damage;
            Speed = 20f;
            LifeTime = 2f;
            Force = 0;
        }

        protected abstract void Move();

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            Move();
            CheckLifeTime();
            CheckCollision();
        }
        
        private void CheckLifeTime()
        {
            if (LifeTime > 0)
            {
                LifeTime -= Time.deltaTime;
                if (LifeTime <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        protected virtual void CheckCollision()
        {
            
        }

        protected abstract void ProcessHit(Damageable target);
    }
}