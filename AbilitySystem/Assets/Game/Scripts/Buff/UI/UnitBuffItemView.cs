// ----- C#
using System;
using System.Collections;

// ----- Unity
using UnityEngine;
using TMPro;

// ----- User Defined
using InGame.ForAbility;

namespace InGame.ForBuff.ForUI
{
    public class UnitBuffItemView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Buff Type")]
        [SerializeField] private EAbilityType    _buffType  = EAbilityType.Unknown;

        [Header("2. UI Group")]
        [SerializeField] private TextMeshProUGUI _TMP_Time  = null;

        [Header("3. Animate Group")]
        [SerializeField] private Animation       _animation = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string TRIGGER_SHOW = "BuffItemView_Show";
        private const string TRIGGER_HIDE = "BuffItemView_Hide";

        // ----- Private
        private Coroutine _co_Show = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAbilityType BuffType => _buffType;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void Show(float duration, Action doneCallBack)
        {
            if (_co_Show == null)
                _co_Show = StartCoroutine(_Co_Show(duration, doneCallBack));
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Show(float duration, Action doneCallBack)
        {
            string FormatTime(float time)
            {
                int min = Mathf.FloorToInt(time / 60);
                int sec = Mathf.FloorToInt(time % 60);

                return string.Format("{0:D2}:{1:D2}", min, sec);
            }

            var startTime = Time.time;
            var endTime   = startTime + duration;

            _animation.clip = _animation.GetClip(TRIGGER_SHOW);
            _animation.Play();

            while (Time.time < endTime)
            {
                _TMP_Time.text = FormatTime(endTime - Time.time);
                
                yield return null;
            }

            _TMP_Time .text = "00:00";
            _animation.clip = _animation.GetClip(TRIGGER_HIDE);
            _animation.Play();

            doneCallBack?.Invoke();
            _co_Show = null;
        }
    }
}