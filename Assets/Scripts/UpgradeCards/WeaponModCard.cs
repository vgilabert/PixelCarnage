using UpgradeCards.WeaponMods;
using WeaponMods;

namespace UpgradeSystem
{
    public class WeaponModCard : BaseCard
    {
        public WeaponModType WeaponModType { get; private set; }

        private WeaponModType[] _incompatibleMods;

        public override void Initialize(BaseCardData cardData)
        {
            WeaponModCardData sourceWeaponModCard = (WeaponModCardData) cardData;
            if (sourceWeaponModCard == null)
            {
                return;
            }
            UpgradeName = sourceWeaponModCard.name;
            WeaponModType = sourceWeaponModCard.weaponModType;
            Description = sourceWeaponModCard.description;
            
            // Set card visuals
            if (UpgradeName.Length > 0)
                titleText.text = UpgradeName;
            if (sourceWeaponModCard.icon != null)
                imageComponent.sprite = sourceWeaponModCard.icon;
            if (Description.Length > 0)
                descriptionText.text = Description;
            
            _incompatibleMods = sourceWeaponModCard.incompatibleMods;
            IsValid = true;
        }

        protected override void UpgradePlayer()
        {
            // Remove incompatible mods from the library
            foreach (WeaponModType modType in _incompatibleMods)
            {
                CardsLibrary.Instance.RemoveCard(modType);
            }
            WeaponModsManager.Instance.ApplyMod(WeaponModType);
        }
    }
}