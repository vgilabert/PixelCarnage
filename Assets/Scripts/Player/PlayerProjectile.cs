using System;
using System.Collections.Generic;
using Enemy;
using Shared;
using StatSystem;
using UnityEngine;

namespace Player
{
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField] private float collisionRadius = 0.5f;
        protected float Speed;
        protected float Force;
        protected float LifeTime;
    
        private List<EnemyBase> _enemiesHit = new();
        private int _playerDamage;
        
        // Velocity variables
        protected Vector2 Direction;
        private Vector2 _lastPosition;

        public virtual void Initialize(float speed)
        {
            Speed = speed;
            if (LifeTime > 0)
            {
                Destroy(gameObject, LifeTime);
            }
        }

        protected virtual void Update()
        {
            Move();
            CalculateDirection();
        }

        protected virtual void Move() { }
        
        private void CalculateDirection()
        {
            Direction = new Vector2(transform.position.x - _lastPosition.x, transform.position.y - _lastPosition.y)
                .normalized;
            _lastPosition = transform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                enemy.TakeHit(SceneManager.Instance.PlayerReference.Stats[StatType.Attack].Value, new HitData(Direction, Force));
                _enemiesHit.Add(enemy);
            }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, collisionRadius);
        }
    }
}