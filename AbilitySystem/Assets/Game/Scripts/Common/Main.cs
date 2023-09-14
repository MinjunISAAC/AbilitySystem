// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.JsonUtil;
using InGame.ForUnit;
using InGame.ForUnit.Manage;
using InGame.ForCam;
using InGame.ForAbility;

namespace InGame
{
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Unit Group")]
        [SerializeField] private Unit           _unitOrigin     = null;
        [SerializeField] private UnitController _unitController = null;

        [Header("Camera Group")]
        [SerializeField] private CamController  _camController  = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Main NullableInstance
        {
            get;
            private set;
        } = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake() { NullableInstance = this; }

        private IEnumerator Start()
        {
            // Json Data Load
            JsonLoader.LoadJson();

            // Unit Setting
            var unit = _unitController.CreateTargetUnit(_unitOrigin);

            // Camera Setting
            _camController.OnInit(unit);
            _camController.ChangeToCamState(CamController.ECamState.Follow_Unit);

            // [TEST]
            var speedValue = AbilityManager.GetValue(EAbilityType.Speed);
            Debug.Log($"Speed Value 1 : {speedValue}");
            AbilityManager.SetValue(EAbilityType.Speed, 10f);
            Debug.Log($"Speed Value 2 : {AbilityManager.GetValue(EAbilityType.Speed)}");

            yield return null;
        }
    }
}