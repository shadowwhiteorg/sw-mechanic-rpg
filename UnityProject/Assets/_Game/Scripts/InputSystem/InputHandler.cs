using System;
using _Game.Managers;
using _Game.CombatSystem;
using _Game.Utils;
using UnityEngine;

namespace _Game.InputSystem
{
    public class InputHandler : Singleton<InputHandler>
    {
        private Camera _mainCamera;
        private PlayerCharacter _player;
        private bool _hasControl;
        private Vector3 _firstMousePosition;
        

        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main;
            _player = FindFirstObjectByType<PlayerCharacter>();
        }

        private void OnEnable()
        {
            EventManager.OnMoveStart += StartControl;
            EventManager.OnMoveEnd += StopControl;
        }

        private void OnDisable()
        {
            EventManager.OnMoveStart -= StartControl;
            EventManager.OnMoveEnd -= StopControl;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // if input is on UI, return
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
                    EventManager.FireOnMoveStart(_player);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
                    EventManager.FireOnMoveEnd(_player);
            }
                
            if(Input.GetMouseButton(0))
                HandleInput();
        }
        
        private void HandleInput()
        {
            if(!_hasControl) return;
            JoystickHandler.Instance.UpdateJoystickPosition(Input.mousePosition);
            _player.MovingActor.Move(InputDirection(Input.mousePosition));
        }

        private Vector2 InputDirection(Vector3 currentMousePosition)
        {
            return (currentMousePosition - _firstMousePosition).normalized;
        }
        
        private void StartControl(BaseCharacter character)
        {
            if(character is not PlayerCharacter) return;
            JoystickHandler.Instance.ShowJoystick(Input.mousePosition);
            _firstMousePosition = Input.mousePosition;
            _hasControl = true;
        }
        private void StopControl(BaseCharacter character)
        {
            if(character is not PlayerCharacter) return;
            JoystickHandler.Instance.HideJoystick();
            _hasControl = false;
        }


    }
}