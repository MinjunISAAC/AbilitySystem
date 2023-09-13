// ----- C#
using System;
using System.Collections;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit;

namespace InGame.ForCam
{
    public class CamController : MonoBehaviour
    {
        // --------------------------------------------------
        // Camera State Enum
        // --------------------------------------------------
        public enum ECamState
        {
            Unknown       = 0,
            Follow_Unit   = 1,
            UnFollow_Unit = 2,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Target Camera")]
        [SerializeField] private Camera  _targetCam      = null;

        [Header("Transform Offset Group")]
        [SerializeField] private Vector3 _positionOffset = Vector3.zero;
        [SerializeField] private Vector3 _rotationOffset = Vector3.zero;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private ECamState _camState        = ECamState.Unknown;
        private Unit      _targetUnit      = null;
        private Coroutine _co_CurrentState = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit(Unit targetUnit)
        {
            if (_targetUnit != null)
            {
                Debug.LogError($"[CamContoller.OnInit] 이미 Taret Unit이 존재합니다.");
                return;
            }

            _targetUnit = targetUnit;
            ChangeToCamState(ECamState.Follow_Unit);
        }

        public void ChangeToCamState(ECamState camState, Action doneCallBack = null)                 => _ChangeToCamState(camState, 0.0f,     doneCallBack);
        public void ChangeToCamState(ECamState camState, float duration, Action doneCallBack = null) => _ChangeToCamState(camState, duration, doneCallBack);

        // ----- Private
        private void _ChangeToCamState(ECamState camState, float duration = 0.0f, Action doneCallBack = null)
        {
            if (!Enum.IsDefined(typeof(ECamState), camState))
            {
                Debug.LogError($"[CamController._ChangeToCamState] {Enum.GetName(typeof(ECamState), camState)}은 정의되어있지 않은 Enum 값입니다.");
                return;
            }

            _camState = camState;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_camState)
            {
                case ECamState.Follow_Unit  : _State_FollowUnit();   break;
                case ECamState.UnFollow_Unit: _State_UnFollowUnit(); break;
            }
        }

        private void _State_FollowUnit()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"[CamController._State_FollowUnit] Target Unit이 존재하지 않습니다.");
                return;
            }

            _co_CurrentState = StartCoroutine(_Co_FollowUnit());
        }

        private void _State_UnFollowUnit()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"[CamController._State_UnFollowUnit] Target Unit이 존재하지 않습니다.");
                return;
            }

            _co_CurrentState = StartCoroutine(_Co_UnFollowUnit());
        }

        // --------------------------------------------------
        // Functions - State Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_FollowUnit()
        {
            while (_camState == ECamState.Follow_Unit)
            {
                if (_targetUnit != null)
                {
                    _targetCam.transform.position = _targetUnit.transform.position + _positionOffset;
                    _targetCam.transform.rotation = Quaternion.Euler(_rotationOffset);
                }
                yield return null;
            }
        }

        private IEnumerator _Co_UnFollowUnit()
        {
            yield return null;
        }
    }
}