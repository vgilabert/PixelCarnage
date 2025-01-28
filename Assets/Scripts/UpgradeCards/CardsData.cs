using System.Collections.Generic;
using StatSystem;
using UnityEngine;
using UnityEngine.Serialization;
using WeaponMods;

namespace UpgradeSystem
{
    [CreateAssetMenu(fileName = "CardsData", menuName = "Upgrade System/Cards Data")]
    public class CardsData : ScriptableObject
    {
        public StatCardData[] statCards;
        public WeaponModCardData[] abilityCards;
        
        public List<BaseCardData> GetAll()
        {
            List<BaseCardData> allCards = new List<BaseCardData>();
            allCards.AddRange(statCards);
            allCards.AddRange(abilityCards);
            return allCards;
        }
    }
    
    [System.Serializable]
    public class BaseCardData
    {
        public string name;
        public Sprite icon;
    }
    
    [System.Serializable]
    public class StatCardData : BaseCardData
    {
        public StatType statType;
        public StatModType statModType;
        public int baseValue;
    }
    
    [System.Serializable]
    public class WeaponModCardData : BaseCardData
    {
        public WeaponModType weaponModType;
        public WeaponModType[] incompatibleMods;
        public string description;

    }
}