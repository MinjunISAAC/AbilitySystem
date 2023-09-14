// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForAbility;

namespace InGame.ForBuff
{
    public class UnitBuffItem : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private EAbilityType _buffType = EAbilityType.Unknown;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAbilityType BuffType => _buffType;
    }
}