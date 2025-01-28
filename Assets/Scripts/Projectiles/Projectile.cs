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
        // Projectile stats
        [SerializeField] private float speed = 10f;
        protected float Speed => speed;
        
        [SerializeField] private float lifeTime = 2f;
        protected float LifeTime => lifeTime;
        
        // Stats from user
        protected float Damage;
        protected float Force;
        
        private readonly HashSet<Damageable> _targetsHit = new ();
        public HashSet<Damageable> TargetsHit => _targetsHit;
        
        private Vector3 _lastPosition;
        private float _lifeTimer;

        protected abstract void Initialize();
        
        public virtual void SetUserStats(StatsData stats)
        {
            Damage = stats[StatType.Attack].Value;
            Force = 1;
        }
        
        public virtual void SetUserStats(EnemyStats stats)
        {
            Damage = stats.Damage;
            Force = 0;
        }

        protected abstract void Move();

        protected virtual void Start()
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
            _lifeTimer += Time.deltaTime;
            if (_lifeTimer >= LifeTime)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void CheckCollision()
        {
            
        }

        public abstract void ProcessHit(Damageable target);
        
        public void Die() => Destroy(gameObject);
    }
}