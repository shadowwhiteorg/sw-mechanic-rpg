using _Game.CombatSystem;
using _Game.DataStructures;
using _Game.Enums;
using _Game.StatSystem;
using UnityEngine;

namespace _Game.SkillSystem.StatSkills
{
    [CreateAssetMenu(fileName = "StatSkillEffect", menuName = "SkillSystem/Stat Skill Effect", order = 0)]
    public class StatSkillEffect : SkillEffectData
    {
        
        [SerializeField] private StatType statType;
        [SerializeField] private float value;
        [SerializeField] private ModifierType modifierType;
        [SerializeField] private float duration =0 ;
        private StatModifier _modifier;
        public override void ApplyEffect(BaseCharacter character)
        {
            _modifier = new StatModifier(value, modifierType,duration);
            character.StatController.AddStatModifier(statType, _modifier);
        }

        public override void RemoveEffect(BaseCharacter character)
        {
            _modifier = new StatModifier(value, modifierType,duration);
            character.StatController.RemoveStatModifier(statType, _modifier);
        }
    }
}