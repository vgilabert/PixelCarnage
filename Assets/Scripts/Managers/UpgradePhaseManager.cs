using Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using UpgradeSystem;

namespace Managers
{
    public class UpgradePhaseManager : MonoSingleton<UpgradePhaseManager>
    {
        [SerializeField] private StatCard statCardPrefab;
        [FormerlySerializedAs("abilityCardPrefab")] [SerializeField] private WeaponModCard weaponModCardPrefab;
        
        [SerializeField] private Transform cardsContainer;
        [SerializeField] private int cardsAmount = 3;
        
        private int _drawCount = 0;

        public void AddDrawCount()
        {
            _drawCount++;
        }
        
        public void CheckNewDraw()
        {
            if (_drawCount > 0)
            {
                DrawCardsSet();
                _drawCount--;
            } else
            {
                GameManager.Instance?.EndUpgradePhase();
            }
        }

        private void DrawCardsSet()
        {
            // Destroy cards in cardsContainer
            foreach (Transform child in cardsContainer)
            {
                Destroy(child.gameObject);
            }
            // Draw new cards
            BaseCardData[] cardsData = CardsLibrary.Instance.GetRandomCards(cardsAmount);
            foreach (BaseCardData cardData in cardsData)
            {
                switch (cardData)
                {
                    case StatCardData statCardData:
                    {
                        StatCard statCard = Instantiate(statCardPrefab, cardsContainer);
                        statCard.Initialize(statCardData);
                        break;
                    }
                    case WeaponModCardData abilityCardData:
                    {
                        WeaponModCard weaponModCard = Instantiate(weaponModCardPrefab, cardsContainer);
                        weaponModCard.Initialize(abilityCardData);
                        break;
                    }
                }
            }
        }
    }
}