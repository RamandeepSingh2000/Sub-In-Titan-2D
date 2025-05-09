using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Submarine.UI
{
    public class SubmarineStatsView : MonoBehaviour
    {
        [SerializeField] private GameObject _statsContainer;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _dashBar;        
        [SerializeField] private TMP_Text _ammoText;
        [SerializeField] private Image _aoeCooldownImage;

        public void ShowStats() => _statsContainer.SetActive(true);

        public void HideStats() => _statsContainer.SetActive(false);

        public void UpdateAmmo(int amount) => _ammoText.text = $"x{amount}";

        public void UpdateHealth(float percentage) => _healthBar.fillAmount = percentage;

        public void UpdateDash(float percentage) => _dashBar.fillAmount = percentage;

        public void UpdateAOECooldown(float percentage) => _aoeCooldownImage.fillAmount = percentage;
    }
}