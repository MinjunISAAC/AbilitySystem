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
        [SerializeField] private EAbilityType   _buffType = EAbilityType.Unknown;
        [SerializeField] private ParticleSystem _buffFx   = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAbilityType   BuffType => _buffType;
        public ParticleSystem BuffFx   => _buffFx;
    }
}