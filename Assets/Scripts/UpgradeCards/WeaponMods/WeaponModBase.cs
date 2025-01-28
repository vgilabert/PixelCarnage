using Projectiles;
using Shared;
using UpgradeSystem;
using WeaponMods;

namespace UpgradeCards.WeaponMods
{
    public abstract class WeaponModBase
    {
        private WeaponModType weaponModType;
        private const int MaxAbilityLevel = 5;
        public bool IsBulletActive { get; protected set; } = true;

        public int Level { get; protected set; } = 1;
        
        public void Upgrade()
        {
            if (Level >= MaxAbilityLevel)
            {
                return;
            }
            Level++;
            OnAbilityUpgrade();
        }
        
        protected virtual void OnAbilityUpgrade()
        {
            if (Level == MaxAbilityLevel)
            {
                CardsLibrary.Instance.RemoveCard(weaponModType);
            }
        }

        public abstract void ApplyMod(PlayerBullet bullet);
        public virtual void OnHit(Damageable target, PlayerBullet bullet) { }
        public virtual void OnInitialize(PlayerBullet bullet) { }
        
        public virtual WeaponModBase Clone()
        {
            return (WeaponModBase)MemberwiseClone();
        }
    }
}