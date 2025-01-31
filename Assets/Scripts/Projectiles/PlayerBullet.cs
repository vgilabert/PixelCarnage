using Enemy;
using Shared;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Projectiles
{
    public class PlayerBullet : Projectile
    {
        protected override void Move()
        {
            transform.position += (Vector3) Direction.normalized * (BulletSpeed * Time.deltaTime);
            transform.up = Direction;
        }
        
        protected override void CheckCollision()
        {
            var hit = Physics2D.Raycast(transform.position, Direction, 0.2f);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out EnemyBase target))
                {
                    if (!TargetsHit.Add(target))
                    {
                        return;
                    }

                    OnHit(target);
                }
            }
        }

        public override void OnHit(Damageable target)
        {
            float finalDamage = ProcessCriticalChance();
            target.TakeHit(finalDamage, new HitData(Direction, Force));
            base.OnHit(target);
        }
        
        private float ProcessCriticalChance()
        {
            if (Random.value <= CriticalChance/100)
            {
                Debug.Log("Critical Hit!");
                AudioManager.Instance.PlaySound(SoundType.CriticalHit);
                return Damage *= CriticalDamage/100;
            }
            return Damage;
        }
    }
}