using Projectiles;
using StatSystem;
using UnityEngine;

namespace Weapons
{
    public class ConicShooter : Weapon
    {
        [SerializeField] private float angle;

        protected override void InstantiateProjectiles(Projectile prefab, Vector2 direction, StatsData stats,
            float projectileSpeed)
        {
            for (int i = 0; i < ProjectileCount; i++)
            {
                Vector2 projectileDirection = CalculateDirection(i, direction);
                Projectile newProjectile = Instantiate(prefab, transform.position, Quaternion.identity);
                newProjectile.SetDirection(projectileDirection);
                newProjectile.Initialize(stats, projectileSpeed);
                newProjectile.ApplyMods(Mods);
            }
        }

        private Vector2 CalculateDirection(int index, Vector2 centralDirection)
        {
            if (ProjectileCount == 1)
            {
                return centralDirection.normalized;
            }

            float halfAngle = angle / 2f;
            float stepAngle = angle / (ProjectileCount - 1);
            float angleOffset = -halfAngle + stepAngle * index;
            float angleRad = angleOffset * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angleRad);
            float sin = Mathf.Sin(angleRad);

            return new Vector2(
                centralDirection.x * cos - centralDirection.y * sin,
                centralDirection.x * sin + centralDirection.y * cos
            ).normalized;
        }

        public override void SetProjectileCount(int count)
        {
            base.SetProjectileCount(count+1);
        }
    }
}