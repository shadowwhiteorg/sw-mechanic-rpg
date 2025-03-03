using UnityEngine;

namespace _Game.CombatSystem
{
    [RequireComponent(typeof(BaseCharacter))]
    public class ActorComponent : MonoBehaviour
    {

        protected BaseCharacter _character;
        public BaseCharacter Character => _character;
        
        [HideInInspector]
        public bool IsInitialized;
        public virtual void Initialize(BaseCharacter character = null)
        {
            if (IsInitialized)
                return;
            _character = GetComponent<BaseCharacter>();
            IsInitialized = true;
        }
    }
}