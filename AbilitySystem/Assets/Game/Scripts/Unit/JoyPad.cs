// ----- C#
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
        [Range(0f, 50f)]
        [SerializeField] private float         _originMoveValue   = 1f; 
        [Range(0f, 1f)]
        [SerializeField] private float         _originRotateValue = 1f;    

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Unit    _targetUnit     = null;

        private Vector3 _movePos        = new Vector3();

        private float   _joyStickRadius = 0.0f;
        private float   _moveSpeed      = 0.0f;
        private float   _rotateSpeed    = 0.0f;
        private float   _moveFactor     = 1f;

        private bool    _isDragging     = false;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public float         MoveSpeed => _moveSpeed;
        public RectTransform FrameRect => _frameRect;

        // --------------------------------------------------
        // Move Factors Event
        // --------------------------------------------------
        public event Action<float, float> OnChangeMoveFactorsEvent;
        public void ChangeMoveFactors(float moveSpeed, float rotateSpeed)
        {
            if (OnChangeMoveFactorsEvent != null)
                OnChangeMoveFactorsEvent(moveSpeed, rotateSpeed);
        }

        public event Action<bool> OnUsedJoyStickEvent;
        public void UsedJoyStickEvent(bool isUsed)
        {
            if (OnUsedJoyStickEvent != null)
                OnUsedJoyStickEvent(isUsed);
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
            _moveSpeed   = _originMoveValue;
            _rotateSpeed = _originRotateValue;

            OnChangeMoveFactorsEvent += (moveSpeed, rotateSpeed) => { _moveSpeed = moveSpeed; _rotateSpeed             = rotateSpeed;                           };
            OnUsedJoyStickEvent      += (isUsed)                 => { _isActived = isUsed;    _stickRect.localPosition = Vector2.zero; _movePos = Vector3.zero; };
        }

        public void SetSpeed(float speed) => _moveSpeed = speed;
        
        // ----- Private
        private void _OnTouch(Vector2 touchVec)
        {
            Vector2 vec       = new Vector2(touchVec.x - _frameRect.position.x, touchVec.y - _frameRect.position.y);
            Vector2 vecNormal = vec.normalized;
            Vector3 force     = new Vector3(vecNormal.x, 0f, vecNormal.y) * _moveSpeed * _moveFactor * 2.5f;

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