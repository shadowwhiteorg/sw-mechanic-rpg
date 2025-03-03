using System.Collections.Generic;
using _Game.SkillSystem;
using UnityEngine;

namespace _Game.CombatSystem
{
    [CreateAssetMenu(fileName = "RangedAttack", menuName = "CombatSystem/AttackBehaviors/Ranged", order = 0)]
    public class RangedAttack : AttackBehavior
    {
        public GameObject ProjectilePrefab;
        public List<ProjectileBehavior> ProjectileBehaviors;
        
        public override void ExecuteAttack(Weapon weapon, Vector3 target)
        {
            // TODO: Apply after stat system is implemented
            // CharacterStats stats = weapon.Owner.GetComponent<CharacterStats>(); // Get the character's stats
            //
            // float dynamicDamage = stats.Damage.GetValue();
            // float dynamicProjectileSpeed = stats.ProjectileSpeed.GetValue();

            // GameObject projectileObj = ObjectPooler.Instance.GetObject(projectilePrefab, weapon.transform.position);
            // if (projectileObj.TryGetComponent(out Projectile projectile))
            // {
            //     projectile.Initialize(target, dynamicProjectileSpeed, dynamicDamage, projectileEffects);
            // }
        }
    }
}