using _Game.CombatSystem;
using _Game.Enums;
using UnityEngine;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "ProjectileSkillEffect", menuName = "SkillSystem/ Projectile Skill Effect", order = 0)]
    public class ProjectileSkillEffect : SkillEffectData
    {
        [SerializeField] private ProjectileBehavior projectileBehavior;
        [SerializeField] private float duration;
        public override void ApplyEffect(BaseCharacter character)
        {
            character.AttackingActor.Weapon.SetActiveProjectile(projectileBehavior.StatusEffectType);
        }

        public override void RemoveEffect(BaseCharacter character)
        {
            character.AttackingActor.Weapon.SetActiveProjectile(StatusEffectType.Default);
        }
    }
}