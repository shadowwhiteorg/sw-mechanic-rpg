using _Game.CombatSystem;
using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.Scripts.CombatSystem.ProjectileSystem.Behaviors
{
    [CreateAssetMenu(fileName = "ArrowBehavior", menuName = "CombatSystem/Projectile/ProjectileBehaviors/ArrowBehavior", order = 0)]
    public class ArrowBehavior : ProjectileBehavior
    {
        public override void ApplyEffect(IDamageable target , BaseCharacter sourceCharacter)
        {
            // target.TakeDamage(sourceCharacter.StatController.GetStatValue(StatType.AttackDamage));
        }
    }
}