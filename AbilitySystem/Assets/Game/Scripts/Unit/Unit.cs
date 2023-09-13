// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Physic Group")]
        [SerializeField] private Rigidbody _rigidBody = null;
        
        [Header("Animate Group")]
        [SerializeField] private Animator  _animator  = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody UnitRigidBody => _rigidBody;
    }
}