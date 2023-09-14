// ----- C#
using InGame.ForAbility;
using System;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit.Manage.ForUI
{
    public class JoyPad : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Activate")]
        [SerializeField] private bool          _isActived         = true;    

        [Header("JoyStick RectTransform")]
        [SerializeField] private RectTransform _frameRect         = null;    
        [SerializeField] private RectTransform _stickRect         = null;    

        [Header("Origin Move Speed")]
        [Range(0f, 1f)]
        [SerializeField] private float         _originRotateValue = 1f;    

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Unit    _targetUnit     = null;

        private Vector3 _movePos        = new Vector3();

        private float   _joyStickRadius = 0.0f;
        private float   _rotateSpeed    = 0.0f;

        private bool    _isDragging     = false;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public RectTransform FrameRect => _frameRect;

        // --------------------------------------------------
        // Move Factors Event
        // --------------------------------------------------
        public event Action<bool> OnUsedJoyStickEvent;
        public void UsedJoyStickEvent(bool isUsed)
        {
            if (OnUsedJoyStickEvent != null)
                OnUsedJoyStickEvent(isUsed);
        }

        public event Action onMouseDownEvent;
        public void OnMouseDown_Event()
        {
            if (onMouseDownEvent != null)
                onMouseDownEvent();
        }

        public event Action onMouseUpEvent;
        public void OnMouseUp_Event()
        {
            if (onMouseUpEvent != null)
                onMouseUpEvent();
        }

        // --------------------------------------------------
        // Function - Event
        // --------------------------------------------------
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (null == _targetUnit) return;
                if (!_isActived)         return;

                _isDragging         = true;
                _frameRect.position = Input.mousePosition;

                OnMouseDown_Event();

                _OnTouch(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                if (null == _targetUnit) return;
                if (!_isActived)         return;

                _frameRect.gameObject.SetActive(true);

                _OnTouch(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (null == _targetUnit) return;
                if (!_isActived)         return;

                _isDragging = false;

                _targetUnit.UnitRigidBody.velocity = Vector3.zero;
                
                OnMouseUp_Event();
                
                _frameRect.gameObject.SetActive(false);
                
                _stickRect.localPosition = Vector2.zero;
            }
        }

        // --------------------------------------------------
        // Function - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit(Unit targetUnit)
        {
            _joyStickRadius = _frameRect.rect.width * 0.5f;

            if (null != _targetUnit)
                return;

            _targetUnit  = targetUnit;
            _rotateSpeed = _originRotateValue;

            OnUsedJoyStickEvent += (isUsed) => { _isActived = isUsed;    _stickRect.localPosition = Vector2.zero; _movePos = Vector3.zero; };
        }

        // ----- Private
        private void _OnTouch(Vector2 touchVec)
        {
            Vector2 vec       = new Vector2(touchVec.x - _frameRect.position.x, touchVec.y - _frameRect.position.y);
            Vector2 vecNormal = vec.normalized;
            Vector3 force     = new Vector3(vecNormal.x, 0f, vecNormal.y) * AbilityManager.GetValue(EAbilityType.Speed) * 3f;

            Debug.Log($"Speed Value : {AbilityManager.GetValue(EAbilityType.Speed)}");
            _stickRect.localPosition = Vector2.ClampMagnitude(vec, _joyStickRadius);

            if (_isDragging) _targetUnit.UnitRigidBody.velocity = force;
            else             _targetUnit.UnitRigidBody.velocity = Vector3.zero;

            if (null == _targetUnit)
                return;

            var rotateVec = Quaternion.Euler(new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f));

            _targetUnit.transform.rotation = Quaternion.Lerp(_targetUnit.transform.rotation, rotateVec, _rotateSpeed);
        }

    }
}