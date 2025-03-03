using System.Collections;
using _Game.Enums;
using _Game.Utils;
using UnityEngine;

namespace _Game.CombatSystem
{
    
    public class AttackingActor : ActorComponent
    {
        [SerializeField] private Weapon weapon;
        private BaseCharacter _character;
        public bool InRage;
        public Weapon Weapon => weapon;
        public override void Initialize(BaseCharacter character)
        {
            base.Initialize();
            _character = character;
            weapon.Initialize(character);
        }
        
        public void Attack()
        {
            weapon.Attack();
        }

        public void StartAttack()
        {
            weapon.SetCurrentTarget();
            _character.MovingActor.NavMeshAgent.updateRotation = false;
            _character.transform.LookAt(weapon.CurrentTarget.GetPosition());
            _character.MovingActor.NavMeshAgent.updateRotation = true;
            StartCoroutine(RepeatedAttack());
        }
        
        public void Stop()
        {
            StopAllCoroutines();
        }
        
        IEnumerator RepeatedAttack()
        {
            while (true)
            {
                _character.CharacterModel.PlayAttackAnimation();
                yield return new WaitForSeconds(0.35f);
                for (int i = 0; i < _character.StatController.GetStatValue(StatType.AttackCount); i++)
                {
                    Attack();
                    yield return new WaitForSeconds(0.2f);
                }
                float delay = 1 / (_character.StatController.GetStatValue(StatType.AttackSpeed).Map(15, 60, 2, 1));
                yield return new WaitForSeconds(delay);
            }
        }
        
    }
}