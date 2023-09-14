using InGame.ForAbility;
using InGame.ForUnit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public void BuffToSpeed()
        {
            if(_co_SpeedBuff == null)
                _co_SpeedBuff = StartCoroutine(_Co_Buff(EAbilityType.Speed, _speedUpValue, () => { _co_SpeedBuff = null; }));
        }

        public void BuffToSize(Action doneCallBack) 
        {
            if (_co_SizeBuff == null)
                _co_SizeBuff = StartCoroutine(_Co_Buff(EAbilityType.Size, _sizeUpValue, () => { doneCallBack();  _co_SizeBuff = null; }));
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------

        private IEnumerator _Co_Buff(EAbilityType type, float buffValue, Action doneCallBack)
        {
            var startTime = Time.time;
            var endTime   = Time.time + _buffDuration;

            Debug.Log($"In Game");
            AbilityManager.SetValue(type, buffValue);

            while (Time.time < endTime)
            {
                // UI Ç¥Çö

                yield return null;
            }

            AbilityManager.RevertValue(type);

            doneCallBack?.Invoke();
            yield return null;
        }
    }
}