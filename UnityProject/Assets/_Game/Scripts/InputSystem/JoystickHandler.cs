using _Game.Enums;
using _Game.Utils;
using UnityEngine;

namespace _Game.InputSystem
{
    public class JoystickHandler : Singleton<JoystickHandler>
    {
        
        [SerializeField] private float handleRange = 100f;
        [SerializeField] private float handleSensitivity = 1f;
        [SerializeField] private float joystickMoveSpeed;
        [SerializeField] private float joystickFollowDistance;
        [SerializeField] private JoystickType joystickType;
        [SerializeField] private RectTransform joystick;
        [SerializeField] private RectTransform joystickHandle;
        
        private Vector3 _targetHandlePosition;
        private bool _isActive;

        protected override void Awake()
        {
            base.Awake();
            if (!joystick || !joystickHandle)
            joystick.gameObject.SetActive(false);
        }

        public void ShowJoystick(Vector3 targetPosition)
        {
            if (!_isActive)
            {
                joystick.gameObject.SetActive(true);
                _isActive = true;
            }
            
            if(joystickType != JoystickType.Fixed)
                joystick.position = targetPosition;
        }

        public void HideJoystick()
        {
            if (_isActive)
            {
                joystick.gameObject.SetActive(false);
                joystickHandle.localPosition = Vector3.zero;
                _isActive = false;
            }
        }
        
        public void UpdateJoystickPosition(Vector3 inputPosition)
        {
            if (!_isActive) return;
            MoveHandle(inputPosition);
            if(joystickType == JoystickType.Floating)
                MoveJoystick(inputPosition);
        }
        private void MoveHandle(Vector3 inputPosition)
        {
            _targetHandlePosition = -joystick.position + inputPosition * handleSensitivity;
            joystickHandle.localPosition = Vector3.ClampMagnitude(_targetHandlePosition, handleRange);
        }
        
        private void MoveJoystick(Vector3 inputPosition)
        {
            if(Vector3.Distance(joystick.position, inputPosition) >= joystickFollowDistance) 
                joystick.position = Vector3.Slerp(joystick.position, inputPosition, Time.deltaTime * joystickMoveSpeed);
        }
    }
    
    

}