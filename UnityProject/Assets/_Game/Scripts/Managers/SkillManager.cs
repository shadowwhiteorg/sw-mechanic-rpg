using System.Collections.Generic;
using _Game.Scripts.UISystem;
using _Game.SkillSystem;
using _Game.Utils;
using UnityEngine;

namespace _Game.Managers
{
    public class SkillManager : Singleton<SkillManager>
    {
        [SerializeField] private List<BaseSkill> skillsToDisplay;
        [SerializeField] private SkillButton skillButtonPrefab;
        [SerializeField] private SkillsTab skillsTab;
        
        public void InitializeSkills()
        {
            foreach (var skill in skillsToDisplay)
            {
                SkillButton skillButton = Instantiate(skillButtonPrefab);
                skillsTab.AddButtonToParent(skillButton.gameObject);
                skillButton.Initialize(skill);
            }
        }
    }
}