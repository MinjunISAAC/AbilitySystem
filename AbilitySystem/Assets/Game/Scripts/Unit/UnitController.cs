// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

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
        // ----- Const
        private const float ROTATE_VALUE = 0.5f;

        // ----- Private
        private Unit  _targetUnit    = null;
        private float _unitMoveValue = 0.0f;

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
                Debug.LogError($"<color = red>[UnitController.CreateTargetUnit] �̹� PlayableUnit�� �����մϴ�.</color>");
                return null;
            }

            _targetUnit = Instantiate(targetUnit, _unitCreateParents);

            if (spawnTrans != null)
                _targetUnit.transform.position = spawnTrans.position;

            // Joy Pad �ʱ�ȭ
            _SetJoyPad();

            // Unit Animate �ʱ�ȭ
            _joyPad.onMouseDownEvent += () => { ChangeToUnitState(Unit.EState.Run);  };
            _joyPad.onMouseUpEvent   += () => { ChangeToUnitState(Unit.EState.Idle); };

            // Unit Ability �ʱ�ȭ
            var defaultSize  = AbilityManager.GetValue(EAbilityType.Size);
            _ChangeUnitScale(defaultSize, null);

            // Unit Buff �ʱ�ȭ
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
                Debug.LogError($"<color = red>[UnitController.SetJoyPad] Target Unit�� �Ҵ���� �ʾҽ��ϴ�.</color>");
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