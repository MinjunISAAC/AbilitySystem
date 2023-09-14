// ----- C#
using System;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage.ForUI;
using InGame.ForAbility;
using InGame.ForBuff;

namespace InGame.ForUnit.Manage
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Joy Pad")]
        [SerializeField] private JoyPad         _joyPad            = null;

        [Header("2. Tranfrom Group")]
        [SerializeField] private Transform      _unitCreateParents = null;

        [Header("3. Unit Buff System")]
        [SerializeField] private UnitBuffSystem _buffSystem        = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private Unit  _targetUnit    = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public Unit   TargetUnit => _targetUnit;
        public JoyPad JoyPad     => _joyPad;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Unit CreateTargetUnit(Unit targetUnit, Transform spawnTrans = null)
        {
            if (_targetUnit != null)
            {
                Debug.LogError($"<color = red>[UnitController.CreateTargetUnit] 이미 PlayableUnit이 존재합니다.</color>");
                return null;
            }

            _targetUnit = Instantiate(targetUnit, _unitCreateParents);

            if (spawnTrans != null)
                _targetUnit.transform.position = spawnTrans.position;

            // Joy Pad 초기화
            _SetJoyPad();

            // Unit Animate 초기화
            _joyPad.onMouseDownEvent += () => { ChangeToUnitState(Unit.EState.Run);  };
            _joyPad.onMouseUpEvent   += () => { ChangeToUnitState(Unit.EState.Idle); };

            // Unit Ability 초기화
            var defaultSize  = AbilityManager.GetValue(EAbilityType.Size);
            _ChangeUnitScale(defaultSize, null);

            // Unit Buff 초기화
            _buffSystem.OnInit();
            UnitBuffEvent.onBuffSpeed += () => _BuffToSpeed();
            UnitBuffEvent.onBuffSize  += () => _BuffToSize ();

            return _targetUnit;
        }

        public void ChangeToUnitState(Unit.EState unitState, Action doneCallBack = null)
        {
            if (_targetUnit == null)
                return;

            _targetUnit.ChangeToUnitState(unitState, doneCallBack);
        }


        // ----- Private
        private void _SetJoyPad()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"<color = red>[UnitController.SetJoyPad] Target Unit이 할당되지 않았습니다.</color>");
                return;
            }

            _joyPad.OnInit           (_targetUnit);
            _joyPad.UsedJoyStickEvent(true);

            _UsedJoyPad(true);
        }

        private void _ChangeUnitScale(float value, Action doneCallBack)
        {
            _targetUnit.SizeChange(value, doneCallBack);
        }

        private void _UsedJoyPad(bool isOn)
        {
            _joyPad.UsedJoyStickEvent(isOn);

            if (!isOn) _joyPad.FrameRect.gameObject.SetActive(isOn);
        }

        private void _BuffToSpeed() => _buffSystem.BuffToSpeed();
        private void _BuffToSize() 
        {
            _buffSystem.BuffToSize
            (
                () =>
                {
                    var buffValue = AbilityManager.GetValue(EAbilityType.Size);
                    _targetUnit.SizeChange(buffValue, null);
                }
            );

            var buffValue = AbilityManager.GetValue(EAbilityType.Size);
            _targetUnit.SizeChange(buffValue, null);
        } 
    }
}