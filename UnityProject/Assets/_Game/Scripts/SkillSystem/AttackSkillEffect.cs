using _Game.CombatSystem;
using UnityEngine;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "AttackSkillEffect", menuName = "SkillSystem/Attack Skill Effect", order = 0)]
    public class AttackSkillEffect : SkillEffectData
    {
        public override void ApplyEffect(BaseCharacter character)
        {
            // throw new System.NotImplementedException();
        }

        public override void RemoveEffect(BaseCharacter character)
        {
            // throw new System.NotImplementedException();
        }
    }
}