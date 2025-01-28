using WeaponMods;

namespace UpgradeSystem
{
    public class WeaponModCard : BaseCard
    {
        public WeaponModType AbilityType { get; private set; }

        public override void Initialize(BaseCardData cardData)
        {
            WeaponModCardData sourceWeaponModCard = (WeaponModCardData) cardData;
            if (sourceWeaponModCard == null)
            {
                return;
            }
            UpgradeName = sourceWeaponModCard.name;
            AbilityType = sourceWeaponModCard.weaponModType;
            Description = sourceWeaponModCard.description;
            if (UpgradeName.Length > 0)
                titleText.text = UpgradeName;
            if (sourceWeaponModCard.icon != null)
                imageComponent.sprite = sourceWeaponModCard.icon;
            IsValid = true;
        }

        protected override void UpgradePlayer()
        {
            //AbilitiesManager.Instance.ApplyAbility(AbilityType);
            WeaponModsManager.Instance.ApplyMod(AbilityType);
        }
    }
}