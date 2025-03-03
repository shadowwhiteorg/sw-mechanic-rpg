using _Game.Enums;
using _Game.Interfaces;
using UnityEngine;

namespace _Game.CombatSystem
{
    public class IdleState : ICharacterState
    {
        public void EnterState(BaseCharacter character)
        {
                     character.CharacterState = CharacterState.Idle;
        }

        public void UpdateState(BaseCharacter character)
        {  
            
        }

        public void ExitState(BaseCharacter character)
        {
            
        }
    }
}