using System.Collections.Generic;
using _Game.SkillSystem;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace _Game.Scripts.SkillSystem
{
    [CreateAssetMenu(fileName = "RageSkill", menuName = "SkillSystem/Rage Skill", order = 0)]
    public class RageSkill : BaseSkill
    {
        [SerializeField] private List<BaseSkill> baseSkills = new List<BaseSkill>();
        public List<BaseSkill> BaseSkills => baseSkills;
    }
}