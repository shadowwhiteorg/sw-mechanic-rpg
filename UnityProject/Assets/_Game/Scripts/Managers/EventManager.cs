using System;
using _Game.CombatSystem;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.Managers
{
    public static class EventManager
    {
        public static event Action<BaseCharacter> OnMoveStart;
        public static event Action<BaseCharacter> OnMoveEnd;
        public static event Action OnSearchEnemies;
        public static event Action OnEnemyDeath;
        public static event Action<IDamageable> OnTargetDeath;
        
        public static void FireOnMoveStart(BaseCharacter character)
        {
            OnMoveStart?.Invoke(character);
        }
        public static void FireOnMoveEnd(BaseCharacter character)
        {
            OnMoveEnd?.Invoke(character);
        }

        public static void FireOnEnemySearch()
        {
            OnSearchEnemies?.Invoke();
        }
        
        public static void FireOnEnemyDeath()
        {
            OnEnemyDeath?.Invoke();
        }
        
        public static void FireOnTargetDeath(IDamageable target)
        {
            OnTargetDeath?.Invoke(target);
        }
        
    }
}