// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.JsonUtil;
using InGame.ForUnit;
using InGame.ForUnit.Manage;

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
            _unitController.CreateTargetUnit(_unitOrigin);

            yield return null;
        }
    }
}