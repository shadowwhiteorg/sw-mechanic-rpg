using UnityEngine;

namespace _Game.CombatSystem
{
    public abstract class AttackBehavior : ScriptableObject
    {
        public abstract void ExecuteAttack(Weapon weapon, Vector3 targetPosition);
    }
}