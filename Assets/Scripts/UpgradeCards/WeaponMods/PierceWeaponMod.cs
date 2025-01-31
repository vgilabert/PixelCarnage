using Projectiles;
using Shared;

namespace UpgradeCards.WeaponMods
{
    public class PierceWeaponMod : WeaponModBase
    {
        private int MaxRicochetCount => Level;
        private int ricochetCount;

        public override void ApplyMod(Projectile projectile)
        {
            IsBulletActive = true; // Ensure the mod keeps the bullet alive
        }

        public override void OnHit(Damageable target, Projectile projectile)
        {
            if (ricochetCount < MaxRicochetCount)
            {
                ricochetCount++;
            }
            else
            {
                IsBulletActive = false; // Mark bullet as ready to be destroyed
            }
        }
    }
}