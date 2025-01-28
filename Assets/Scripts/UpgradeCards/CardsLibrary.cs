using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using WeaponMods;

namespace UpgradeSystem
{
    public class CardsLibrary : MonoSingleton<CardsLibrary>
    {
        [SerializeField] private CardsData cardsData;
        private readonly HashSet<BaseCardData> _availableCards = new ();
        
        protected override void Awake()
        {
            base.Awake();
            foreach (var cardData in cardsData.GetAll())
            {
                _availableCards.Add(cardData);
            }
        }
        
        public BaseCardData GetRandomCard()
        {
            int randomIndex = Random.Range(0, _availableCards.Count);
            return _availableCards.ElementAt(randomIndex);
        }
        
        public BaseCardData[] GetRandomCards(int amount, bool unique = true)
        {
            if (amount > _availableCards.Count)
            {
                amount = _availableCards.Count;
            }
            BaseCardData[] randomCards = new BaseCardData[amount];
            if (unique)
            {
                HashSet<BaseCardData> availableCardsCopy = new (_availableCards);
                
                for (int i = 0; i < amount; i++)
                {
                    int randomIndex = Random.Range(0, availableCardsCopy.Count);
                    randomCards[i] = availableCardsCopy.ElementAt(randomIndex);
                    availableCardsCopy.Remove(randomCards[i]);
                }
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    randomCards[i] = GetRandomCard();
                }
            }
            return randomCards;
        }
        
        public void RemoveCard(BaseCardData data) => _availableCards.Remove(data);

        public void RemoveCard(WeaponModType data)
        {
            WeaponModCardData weaponModCardData = AbilityBaseToAbilityCardData(data);
            if (weaponModCardData != null)
            {
                _availableCards.Remove(weaponModCardData);
            }
        }
        
        private WeaponModCardData AbilityBaseToAbilityCardData(WeaponModType weaponModType)
        {
            foreach (var cardData in cardsData.abilityCards)
            {
                if (cardData.weaponModType == weaponModType)
                {
                    return cardData;
                }
            }

            return null;
        }
    }
}