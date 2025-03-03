using _Game.Managers;
using UnityEngine;

namespace _Game.CombatSystem
{
    public class EnemyCharacter : BaseCharacter
    {
        protected override void Die()
        {
            base.Die();
            CombatManager.Instance.RemoveEnemy(this);
            EventManager.FireOnEnemyDeath();
            // Destroy(gameObject);
        }
        
    }
}