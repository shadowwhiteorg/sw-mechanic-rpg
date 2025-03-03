using _Game.Managers;
using _Game.SkillSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UISystem
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Image skillIcon;
        [SerializeField] private GameObject activeIndicator;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI description;
        
        private bool _skillActive;
        
        public void SetSkillIcon(Sprite icon)
        {
            skillIcon.sprite = icon;
        }
        
        public void SetActiveIndicator(bool active)
        {
            activeIndicator.SetActive(active);
        }
        
        public void ActivateSkillIcon(bool active)
        {
            activeIndicator.SetActive(active);
        }

        public void Initialize(BaseSkill skill)
        {
            skillIcon.sprite = skill.Icon;
            skillName.text = skill.SkillName;
            description.text = skill.Description;
            ActivateSkillIcon(false);
            button.onClick.AddListener(() =>
            {
                if (_skillActive)
                {
                    GameManager.Instance.PlayerCharacter.RemoveSkill(skill);
                    ActivateSkillIcon(false);
                    _skillActive = false;
                }
                else
                {
                    GameManager.Instance.PlayerCharacter.LearnSkill(skill);
                    ActivateSkillIcon(true);
                    _skillActive = true;
                }
            });
        }
    }
}