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

            yield return null;
        }
    }
}