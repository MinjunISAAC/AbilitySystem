// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForBuff;
using InGame.ForAbility;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Unit State Enum
        // --------------------------------------------------
        public enum EState
        {
            Unknown = 0,
            Idle    = 1,
            Run     = 2,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Physic Group")]
        [SerializeField] private Rigidbody      _rigidBody = null;
        
        [Header("Animate Group")]
        [SerializeField] private Animator       _animator  = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const float SIZE_DURATION = 0.25f;
        
        // ----- Private
        private EState    _state           = EState.Unknown;

        private Coroutine _co_CurrentState = null;
        private Coroutine _co_SizeChange   = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody UnitRigidBody => _rigidBody;
        public EState    UnitState     => _state;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<UnitBuffItem>(out UnitBuffItem buffItem))
            {
                switch (buffItem.BuffType)
                {
                    case EAbilityType.Speed: UnitBuffEvent.OnBuffSpeed(); break;
                    case EAbilityType.Size : UnitBuffEvent.OnBuffSize();  break;
                }
            }
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- State (Public)
        public void ChangeToUnitState(EState unitState, Action doneCallBack = null)
            => _ChangeToUnitState(unitState, doneCallBack);

        // ----- State (Private)
        private void _ChangeToUnitState(EState unitState, Action doneCallBack = null)
        {
            if (!Enum.IsDefined(typeof(EState), unitState))
            {
                Debug.LogError($"[Unit._ChangeToUnitState] {Enum.GetName(typeof(EState), unitState)}은 정의되어있지 않은 Enum 값입니다.");
                return;
            }

            if (_state == unitState)
                return;

            _state = unitState;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_state)
            {
                case EState.Idle: _State_Idle(doneCallBack); break;
                case EState.Run:  _State_Run(doneCallBack);  break;
            }
        }

        private void _State_Idle(Action doneCallBack = null)
        {
            _co_CurrentState = StartCoroutine(_Co_Idle(doneCallBack));
        }

        private void _State_Run(Action doneCallBack = null)
        {
            _co_CurrentState = StartCoroutine(_Co_Run(doneCallBack));
        }

        // ----- Public
        public void SizeChange(float value, Action doneCallBack)
        {
            if (transform.localScale.x == value)
                return;

            if (_co_SizeChange == null)
                _co_SizeChange = StartCoroutine(_Co_SizeChange(value, doneCallBack));
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        // ----- State
        private IEnumerator _Co_Idle(Action doneCallBack = null)
        {
            _animator.SetTrigger("Idle");

            while (_state == EState.Idle)
            {
                yield return null;
            }

            doneCallBack?.Invoke();
        }

        private IEnumerator _Co_Run(Action doneCallBack = null)
        {
            _animator.SetTrigger("Run");

            while (_state == EState.Idle)
            {
                yield return null;
            }

            doneCallBack?.Invoke();
        }

        // ----- Option
        private IEnumerator _Co_SizeChange(float value, Action doneCallBack)
        {
            var sec       = 0.0f;
            var startSize = transform.localScale;
            var endSize   = new Vector3(value, value, value);

            while (sec < SIZE_DURATION)
            {
                sec += Time.deltaTime;

                transform.localScale = Vector3.Lerp(startSize, endSize, sec / SIZE_DURATION);
                yield return null;
            }

            transform.localScale = endSize;

            doneCallBack?.Invoke();

            _co_SizeChange = null;
        }
    }
}