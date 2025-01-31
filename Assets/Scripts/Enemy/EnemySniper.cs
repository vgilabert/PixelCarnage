using Projectiles;
using UnityEngine;

namespace Enemy
{
    public class EnemySniper : EnemyBase
    {
        [SerializeField] private float bulletSpeed = 15f;
        [SerializeField] private Projectile projectilePrefab;

        protected override void Update()
        {
            base.Update();
            transform.up = PlayerPosition - transform.position;
            
        }
        
        protected override void PerformSpecificAttack()
        {
            EnemyBullet bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
            bullet.Initialize(Stats, bulletSpeed);
            bullet.SetDirection(PlayerPosition - transform.position);
        }
    }
}