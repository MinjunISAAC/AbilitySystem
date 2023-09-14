// ----- C#
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.JsonUtil;

namespace InGame.ForAbility
{
    public static class AbilityManager 
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private static Dictionary<EAbilityType, AbilityValue> _abilitySet           = new Dictionary<EAbilityType, AbilityValue>();
        private static bool                                   _isLoadedDefaultValue = false;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public static void SetValue(EAbilityType type, float value)
        {
            _LoadToDefaultValue();
            _SetAbilityValue(type, value);
        }

        public static void RevertValue()
        {
            _LoadToDefaultValue();
            _SetAbilityValue(EAbilityType.Unknown, -1, true);
        }

        public static float GetValue(EAbilityType type)
        {
            _LoadToDefaultValue();

            return _GetAbilityValue(type).Value;
        }

        // ----- Private
        private static void _LoadToDefaultValue()
        {
            if (_isLoadedDefaultValue)
                return;

            _isLoadedDefaultValue = true;

            var defaultValueDataSet = JsonLoader.GetAbilityDefaultDataSet().DataSet;
            for (int i = 0; i < defaultValueDataSet.Count; i++)
            {
                var data         = defaultValueDataSet[i];
                var valueType    = data.Type;
                var defaultValue = data.DefaultValue;
                
                if (Enum.TryParse(valueType, out EAbilityType abilityType))
                {
                    if (!_abilitySet.TryGetValue(abilityType, out AbilityValue value))
                    {
                        var abilityValue = new AbilityValue(abilityType, defaultValue);
                        _abilitySet.Add(abilityType, abilityValue);
                    }
                    else continue;
                }
                else
                    Debug.LogError($"<color=red>[AbilityManager._LoadToDefaultValue] {valueType}에 해당하는 Enum 값이 존재하지 않습니다.</color>");
            }
        }

        private static AbilityValue _GetAbilityValue(EAbilityType type)
        {
            if (_abilitySet.TryGetValue(type, out AbilityValue value))
                return value;
            else
            {
                Debug.LogError($"<color=red>[AbilityManager._GetAbilityValue] {type}에 해당하는 Ability Value가 존재하지 않습니다.</color>");
                return null;
            }
        }

        private static void _SetAbilityValue(EAbilityType type, float value, bool isRevert = false)
        {
            if (_abilitySet.TryGetValue(type, out AbilityValue val)) 
            {
                if (!isRevert) val.Set(value);
                else           val.Revert();
            } 
            else Debug.LogError($"<color=red>[AbilityManager._SetAbilityValue] Data Set이 초기화 되지 않았습니다.</color>");
        }
    }
}