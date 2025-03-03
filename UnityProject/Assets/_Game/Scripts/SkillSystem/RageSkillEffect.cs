using _Game.CombatSystem;
using _Game.SkillSystem;
using _Game.SkillSystem.StatSkills;
using UnityEngine;

namespace _Game.Scripts.SkillSystem
{
    [CreateAssetMenu(fileName = "RageSkillEffect", menuName = "SkillSystem/Rage Skill Effect", order = 0)]
    public class RageSkillEffect : SkillEffectData
    {
        public override void ApplyEffect(BaseCharacter character)
        {
            character.AttackingActor.InRage = true;
            character.DuplicateActiveSkills();
        }

        public override void RemoveEffect(BaseCharacter character)
        {
            character.AttackingActor.InRage = false;
            character.RemoveAllDuplicatedSkills();
        }
    }
}