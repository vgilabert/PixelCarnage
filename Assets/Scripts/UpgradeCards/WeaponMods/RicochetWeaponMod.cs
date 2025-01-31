using Projectiles;
using Shared;

namespace UpgradeCards.WeaponMods
{
    public class RicochetWeaponMod : WeaponModBase
    {
        private int MaxRicochetCount => Level;
        private int _ricochetCount;

        public override void ApplyMod(Projectile projectile)
        {
            IsBulletActive = true; // Ensure the mod keeps the bullet alive
        }

        public override void OnHit(Damageable target, Projectile projectile)
        {
            if (_ricochetCount < MaxRicochetCount)
            {
                _ricochetCount++;
                projectile.FindNewTarget();
            }
            else
            {
                IsBulletActive = false; // Mark bullet as ready to be destroyed
            }
        }
    }
}