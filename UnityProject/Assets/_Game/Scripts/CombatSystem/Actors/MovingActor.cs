using _Game.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.CombatSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovingActor : ActorComponent
    {
        public bool IsMoving => _navMeshAgent.velocity.sqrMagnitude>  0.1f;
        private NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public float movementSpeed => _character.StatController.GetStatValue(StatType.MovementSpeed);

        public override void Initialize(BaseCharacter character)
        {
            base.Initialize(character);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = character.StatController.GetStatValue(StatType.MovementSpeed);
        }
        
        public void Move(Vector2 direction)
        {
            _navMeshAgent.SetDestination(_navMeshAgent.transform.position + new Vector3(direction.x, 0, direction.y));
        }

        public void Stop()
        {
            _navMeshAgent.ResetPath();
        }
    }
}