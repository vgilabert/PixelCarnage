using Projectiles;
using StatSystem;
using UnityEngine;

namespace Weapons
{
    public class SphericalShooter : Weapon
    {
        protected override void InstantiateProjectiles(Projectile prefab, Vector2 direction, StatsData stats,
            float projectileSpeed)
        {
            for (int i = 0; i < ProjectileCount; i++)
            {
                Projectile newProjectile = Instantiate(prefab, transform.position, Quaternion.identity);
                // Use trigonometric circle to rotate the direction for each projectile
                direction += new Vector2(Mathf.Cos(i * 2 * Mathf.PI / ProjectileCount),
                    Mathf.Sin(i * 2 * Mathf.PI / ProjectileCount));
                newProjectile.SetDirection(direction);
                newProjectile.Initialize(stats, projectileSpeed);
            }
        }
    }
}