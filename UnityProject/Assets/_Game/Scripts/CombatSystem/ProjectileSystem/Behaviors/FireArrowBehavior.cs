using _Game.CombatSystem;
using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Game.SkillSystem
{
    [CreateAssetMenu(fileName = "FireArrowBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/FireArrowBehavior", order = 0)]
    public class FireArrowBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(IDamageable target, BaseCharacter sourceCharacter)
        {
            Debug.Log("Fire Arrow Behavior");
            target.ApplyStatusEffect(StatusEffectType.Fire,sourceCharacter.StatController.GetStatValue(StatType.BurnDamageDuration),sourceCharacter.StatController.GetStatValue(StatType.BurnDamage));
        }
    }
}