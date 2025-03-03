using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.CombatSystem
{
    public abstract class ProjectileBehavior : ScriptableObject
    {
        [SerializeField] private StatusEffectType statusEffectType;
        public StatusEffectType StatusEffectType => statusEffectType;
        public abstract void ApplyEffect(IDamageable target, BaseCharacter sourceCharacter);
    }
}