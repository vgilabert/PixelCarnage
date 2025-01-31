using Shared;
using UnityEngine;

namespace Projectiles
{
    public class EnemyBullet : Projectile
    {

        protected override void Move()
        {
            transform.position += (Vector3)Direction.normalized * (BulletSpeed * Time.deltaTime);
        }
        
        protected override void CheckCollision()
        {
            var hit = Physics2D.Raycast(transform.position, Direction, 0.2f);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out Player.Player target))
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
            target.TakeHit(Damage, new HitData(Direction, Force));
            base.OnHit(target);
        }
    }
}