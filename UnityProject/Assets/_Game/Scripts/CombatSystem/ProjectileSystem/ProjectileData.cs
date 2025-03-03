using System.Collections.Generic;
using _Game.Enums;
using _Game.SkillSystem;
using UnityEngine;

namespace _Game.CombatSystem
{
    [CreateAssetMenu(fileName = "ProjectileData", menuName = "CombatSystem/Projectile/Projectile Data", order = 0)]
    public class ProjectileData : ScriptableObject
    {
        public StatusEffectType Type;
        public Projectile Prefab;
        public float Damage;
        public List<ProjectileBehavior> DefaultBehaviors;
    }
}