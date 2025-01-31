using Weapons;

namespace UpgradeCards.WeaponMods
{
    public class MultiShotWeaponMod : WeaponModBase
    {
        public override void ApplyMod(Weapon weapon)
        {
            weapon.SetProjectileCount(Level+1);
            IsBulletActive = false;
        }

        protected override void OnAbilityUpgrade()
        {
            base.OnAbilityUpgrade();
        }
    }
}