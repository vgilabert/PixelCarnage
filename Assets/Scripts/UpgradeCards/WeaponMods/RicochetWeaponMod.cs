using Projectiles;
using Shared;
using UnityEngine;
using UpgradeCards.WeaponMods;

namespace WeaponMods
{
    public class RicochetWeaponMod : WeaponModBase
    {
        private int MaxRicochetCount => Level;
        private int ricochetCount;

        public override void ApplyMod(PlayerBullet bullet)
        {
            IsBulletActive = true; // Ensure the mod keeps the bullet alive
        }

        public override void OnHit(Damageable target, PlayerBullet bullet)
        {
            if (ricochetCount < MaxRicochetCount)
            {
                ricochetCount++;
                bullet.FindNewTarget();
            }
            else
            {
                IsBulletActive = false; // Mark bullet as ready to be destroyed
            }
        }
    }
}