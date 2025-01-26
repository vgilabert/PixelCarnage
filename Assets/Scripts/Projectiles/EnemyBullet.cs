using Shared;
using StatSystem;
using UnityEngine;

namespace Projectiles
{
    public class EnemyBullet : Projectile
    {
        private Vector2 _direction;
        
        protected override void Initialize()
        {
            _direction = SceneManager.Instance.PlayerPosition - transform.position;
        }

        public override void SetUserStats(StatsData stats)
        {
            Damage = stats[StatType.Attack].Value;
            Speed = 5f;
            LifeTime = 2f;
            Force = 0;
        }

        protected override void Move()
        {
            transform.position += (Vector3)_direction.normalized * (Speed * Time.deltaTime);
        }
        
        protected override void CheckCollision()
        {
            var hit = Physics2D.Raycast(transform.position, _direction, 0.2f);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Player.Player target))
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
        }
    }
}