using System.Collections.Generic;
using _Game.CombatSystem;
using _Game.Enums;
using UnityEngine;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "BaseSkill", menuName = "SkillSystem/BaseSkill", order = 0)]
    public class BaseSkill : ScriptableObject
    {
        [SerializeField] private string skillName;
        [SerializeField] private string description;
        [SerializeField] private SkillType skillType;
        [SerializeField] private float cooldown;
        [SerializeField] private float cost;
        [SerializeField] private Sprite icon;
        [SerializeField] private List<SkillEffectData> skillEffects;
        [SerializeField] private bool isFromRage;
        private BaseSkill _coupleSkill;
        public BaseSkill CoupleSkill => _coupleSkill;
        public bool IsFromRage => isFromRage;
        public string SkillName => skillName;
        public string Description => description;
        public float Cooldown => cooldown;
        public float Cost => cost;
        public SkillType SkillType => skillType;
        public Sprite Icon => icon;

        private int _applicationCount = 1;
        public int ApplicationCount => _applicationCount;
        
        public void ApplySkill(BaseCharacter character)
        {
            foreach (var effect in skillEffects)
            {
                effect.ApplyEffect(character);
            }
        }

        public void RemoveSkill(BaseCharacter character)
        {
            foreach (var effect in skillEffects)
            {
                effect.RemoveEffect(character);
            }
        }

        public void SetCoupleSkill(BaseSkill coupleSkill)
        {
            _coupleSkill = coupleSkill;
        }
        
        public BaseSkill CreateDuplicate()
        {
            BaseSkill duplicate = Instantiate(this);
            duplicate.name = this.name + " (Duplicated)";
            return duplicate;
        }

        public void RemoveDuplicate()
        {
            _applicationCount = 1;
        }
    }
}