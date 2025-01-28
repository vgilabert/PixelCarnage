using StatSystem;
using UnityEngine;

namespace UpgradeSystem
{
    public class StatCard : BaseCard
    {
        private StatType _statType;
        private StatModType _modType;
        private float _baseValue;
        private int _rarity;
        
        private float Value => (_baseValue * RarityMultipliers[_rarity]);
        
        private static readonly float[] RarityChances = {0.4f, 0.3f, 0.2f, 0.1f};
        private static readonly float[] RarityMultipliers = {1, 1.5f, 2, 2.5f};
        
        public override void Initialize(BaseCardData source)
        {
            StatCardData sourceStatCard = (StatCardData) source;
            if (sourceStatCard == null)
            {
                return;
            }
            UpgradeName = sourceStatCard.name;
            _rarity = GetRandomRarity(); // Set rarity before getting Value
            _statType = sourceStatCard.statType;
            _modType = sourceStatCard.statModType;
            _baseValue = sourceStatCard.baseValue;
            Description = _statType + " + " + Value;
            if (_modType == StatModType.PercentAdd)
                Description += "%";
            
            // Set card visuals
            if (UpgradeName.Length > 0)
                titleText.text = UpgradeName;
            if (Description.Length > 0)
                descriptionText.text = Description;
            if (source.icon != null)
                imageComponent.sprite = source.icon;
            
            // Set card color based on rarity
            switch (_rarity)
            {
                case 0:
                    rarityImage.color = commonColor;
                    break;
                case 1:
                    rarityImage.color = rareColor;
                    break;
                case 2:
                    rarityImage.color = epicColor;
                    break;
                case 3:
                    rarityImage.color = legendaryColor;
                    break;
            }
            
            // Mark card as valid
            IsValid = true;
        }

        private int GetRandomRarity()
        {
            float random = Random.value;
            float chance = 0;
            for (int i = 0; i < RarityChances.Length; i++)
            {
                chance += RarityChances[i];
                if (random <= chance)
                {
                    return i;
                }
            }
            return 0;
        }

        protected override void UpgradePlayer()
        {
            if (!IsValid)
            {
                Debug.LogWarning("Card was not initialized properly");
                return;
            }
            SceneManager.Instance.PlayerReference.Stats[_statType].AddModifier(new StatModifier(Value, _modType, _statType));
        }
    }
}