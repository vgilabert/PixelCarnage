using Extensions;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private GameObject overlay;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject upgradeScreen;
        [SerializeField] private GameObject pauseScreen;
        
        public void ShowOverlay()
        {
            overlay.SetActive(true);
        }

        public void HideOverlay()
        {
            overlay.SetActive(false);
        }

        public void ShowGameOverScreen()
        {
            
        }
        
        public void HideGameOverScreen()
        {
            
        }

        public void ShowUpgradeScreen()
        {
            upgradeScreen.SetActive(true);
        }
        
        public void HideUpgradeScreen()
        {
            upgradeScreen.SetActive(false);
        }

        public void ShowPauseScreen()
        {
            pauseScreen.SetActive(true);
        }

        public void HidePauseScreen()
        {
            pauseScreen.SetActive(false);
        }
    }
}