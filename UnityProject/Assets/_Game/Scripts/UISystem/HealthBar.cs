using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UISystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image baseBar;
        [SerializeField] private Image fillBar;
        
        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            fillBar.fillAmount = currentHealth / maxHealth;
        }
    }
}