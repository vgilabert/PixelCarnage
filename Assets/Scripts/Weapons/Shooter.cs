using Projectiles;
using UnityEngine;

namespace Weapons
{
    public class Shooter : Weapon
    {
        protected override void Attack()
        {
            base.Attack();
            Projectile projectile = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity)
                .GetComponent<Projectile>();
            projectile.SetUserStats(UserStats);
        }
    }
}