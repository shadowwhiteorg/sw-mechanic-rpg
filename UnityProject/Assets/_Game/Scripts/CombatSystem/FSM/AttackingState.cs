using _Game.Enums;
using _Game.Interfaces;
using Unity.Cinemachine;

namespace _Game.CombatSystem
{
    public class AttackingState : ICharacterState
    {
        public void EnterState(BaseCharacter character)
        {
            character.AttackingActor?.Initialize(character);
            character.CharacterState = CharacterState.Attacking;
            character.AttackingActor?.StartAttack();
            
        }

        public void UpdateState(BaseCharacter character)
        {
            
        }

        public void ExitState(BaseCharacter character)
        {
            character?.AttackingActor?.Stop();
        }
    }
}