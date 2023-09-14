// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForAbility;

namespace InGame.ForBuff
{
    public class BuffItem : MonoBehaviour
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