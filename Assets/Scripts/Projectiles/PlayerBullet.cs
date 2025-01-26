using System.Linq;
using Enemy;
using Shared;
using StatSystem;
using UnityEngine;

namespace Projectiles
{
    public class PlayerBullet : Projectile
    {
        private Damageable _target;
        private Vector3 _currentTargetPosition;
        private Vector2 _direction;
        
        // Upgrades
        private int _ricochetLevel = 2;
        private int _ricochetCount;
        private int _piercingLevel = 1;
        private int _piercingCount; 
        
        
        protected override void Initialize()
        {
            FindNewTarget();
        }
        
        private void FindNewTarget()
        {
            _target = TargetFinder.FindClosestTarget(transform.position, ref _currentTargetPosition,
                TargetsHit);
            if (_target == null)
            {
                Destroy(gameObject);
            }
            _direction = _currentTargetPosition - transform.position;
            if (_direction == Vector2.zero)
            {
                Destroy(gameObject);
            }
        }
        
        public override void SetUserStats(StatsData stats)
        {
            Damage = stats[StatType.Attack].Value;
            Speed = 20f;
            LifeTime = 2f;
            Force = 0;
        }

        protected override void Move()
        {
            transform.position += (Vector3) _direction.normalized * (Speed * Time.deltaTime);
            transform.up = _direction;
        }
        
        protected override void CheckCollision()
        {
            var hit = Physics2D.Raycast(transform.position, _direction, 0.2f);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out EnemyBase target))
                {
                    if (!TargetsHit.Add(target))
                    {
                        return;
                    }

                    ProcessHit(target);
                }
            }
        }

        protected override void ProcessHit(Damageable target)
        {
            target.TakeHit(Damage, new HitData(_direction, Force));
            bool shouldRicochet = (_ricochetLevel > 0 && _ricochetCount < _ricochetLevel);
            bool shouldPierce = (_piercingLevel > 0 && _piercingCount < _piercingLevel);
            if (shouldRicochet)
            {
                _ricochetCount++;
                FindNewTarget();
            }
            else if (shouldPierce)
            {
                _piercingCount++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}