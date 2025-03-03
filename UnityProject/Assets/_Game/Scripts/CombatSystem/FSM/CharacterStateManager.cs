using System;
using _Game.Interfaces;
using UnityEngine;
using _Game.Managers;

namespace _Game.CombatSystem
{
    public class CharacterStateManager : MonoBehaviour
    {
        private ICharacterState _currentState;
        private BaseCharacter _character;

        private void Awake()
        {
            _character = GetComponent<BaseCharacter>();
            SetState(new IdleState());
        }

        private void OnEnable()
        {
            if(_character.MovingActor)
                EventManager.OnMoveStart += _ => SetState(new MovingState());
            if(_character.AttackingActor)
                EventManager.OnMoveEnd += _ => SetState(new AttackingState());
        }
        
        private void OnDisable()
        {
            if(_character.MovingActor)
                EventManager.OnMoveStart -= _ => SetState(new MovingState());
            if(_character.AttackingActor)
                EventManager.OnMoveEnd -= _ => SetState(new AttackingState());
        }
        

        private void SetState(ICharacterState newState)
        {
            if (_currentState != null)
                _currentState.ExitState(_character);
            _currentState = newState;
            _currentState.EnterState(_character);
        }
        
        
        
        
        // Use it if only it's necessary
        // private void Update()
        // {
        //     _currentState?.UpdateState(_character);
        // }
        
        
    }
}