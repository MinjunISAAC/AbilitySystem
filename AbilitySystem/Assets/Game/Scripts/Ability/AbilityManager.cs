// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

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

        // ----- Private
        private static void _LoadToDefaultValue()
        {
            if (_isLoadedDefaultValue)
                return;

            _isLoadedDefaultValue = true;

            //var defalutValue = 
        }
    }
}