// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.ForDefault;

namespace Utility.JsonUtil
{
    public static class JsonLoader
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string JSONFILE_NAME = "AbilityDefaultDatas";

        // ----- Private
        private static DefaultDataSet _dataSet = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public static void LoadJson()
        {
            var loadedJsonFile = Resources.Load<TextAsset>($"JsonSet/{JSONFILE_NAME}");

            if (loadedJsonFile == null)
            {
                Debug.LogError("<color=red>[JsonParser.LoadJson] Json File이 존재하지 않습니다.</color>");
                return;
            }

            _dataSet = JsonUtility.FromJson<DefaultDataSet>(loadedJsonFile.text);

            if (_dataSet == null)
            {
                Debug.LogError("<color=red>[JsonParser.LoadJson] JSON 파싱에 실패하였습니다.</color>");
                return;
            }
        }

        public static DefaultDataSet GetLocaleDataSet()
        {
            if (_dataSet == null)
                return null;

            return _dataSet;
        }
    }
}