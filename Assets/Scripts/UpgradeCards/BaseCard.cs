using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradeSystem
{
    public abstract class BaseCard : MonoBehaviour
    {
        [Header("Card Components")]
        [SerializeField] protected TextMeshProUGUI titleText;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        [SerializeField] protected Image imageComponent;
        [SerializeField] protected Image rarityImage;
        
        [Header("Rarity Colors")]
        [SerializeField] protected Color commonColor;
        [SerializeField] protected Color rareColor;
        [SerializeField] protected Color epicColor;
        [SerializeField] protected Color legendaryColor;
        
        [Header("Debug - Only set if you want to test the card")]
        [SerializeField] protected bool testCard;
        [SerializeField] protected StatCardData cardDataTest;
        
        protected string UpgradeName;
        protected string Description;
        protected bool IsValid;

        protected virtual void Start()
        {
            if (testCard)
            {
                Initialize(cardDataTest);
            }
        }

        public abstract void Initialize(BaseCardData cardData);

        protected abstract void UpgradePlayer();

        public void OnPointerDown()
        {
            UpgradePlayer();
            UpgradePhaseManager.Instance.CheckNewDraw();
        }

        public void OnPointerEnter()
        {
            transform.DOScale(Vector3.one * 1.1f, 0.2f).SetUpdate(true);
        }
        
        public void OnPointerExit()
        {
            transform.DOScale(Vector3.one, 0.2f).SetUpdate(true);
        }
    }
}