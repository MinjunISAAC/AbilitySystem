// ----- C#
using System;
using System.Collections;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForAbility;
using InGame.ForBuff.ForUI;

namespace InGame.ForBuff
{
    public class UnitBuffSystem : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Buff Option")]
        [SerializeField] private float _buffDuration = 10f;
        [Space]
        [SerializeField] private float _speedUpValue = 3f;
        [SerializeField] private float _sizeUpValue  = 2f;
        
        [Header("2. View")]
        [SerializeField] private UnitBuffView _unitBuffView = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Coroutine _co_SpeedBuff = null;
        private Coroutine _co_SizeBuff  = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit() { _unitBuffView.OnInit(); }
        
        public void BuffToSpeed(Action tryAction = null)
        {
            if(_co_SpeedBuff == null)
            {
                tryAction?.Invoke();
                _co_SpeedBuff = StartCoroutine(_Co_Buff(EAbilityType.Speed, _speedUpValue, () => { _co_SpeedBuff = null; }));
                _unitBuffView.ShowToBuffItemView(EAbilityType.Speed, _buffDuration, null);
            }
        }

        public void BuffToSize(Action doneCallBack) 
        {
            if (_co_SizeBuff == null)
            {
                _co_SizeBuff = StartCoroutine(_Co_Buff(EAbilityType.Size, _sizeUpValue, () => { doneCallBack();  _co_SizeBuff = null; }));
                _unitBuffView.ShowToBuffItemView(EAbilityType.Size, _buffDuration, null);
            }
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------

        private IEnumerator _Co_Buff(EAbilityType type, float buffValue, Action doneCallBack)
        {
            AbilityManager.SetValue(type, buffValue);

            yield return new WaitForSeconds(_buffDuration);

            AbilityManager.RevertValue(type);

            doneCallBack?.Invoke();
        }
    }
}