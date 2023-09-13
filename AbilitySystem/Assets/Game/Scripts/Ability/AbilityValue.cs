// ----- C#
using System.Collections.Generic;

namespace InGame.ForAbility
{
    public class AbilityValue
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private float _originValue = 0f;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAbilityType Type
        { 
            get; 
            private set; 
        } = EAbilityType.Unknown;

        public float Value
        {
            get;
            private set;
        } = 0.0f;

        // --------------------------------------------------
        // Constuctor
        // --------------------------------------------------
        public AbilityValue ( EAbilityType type, float value )
        {
            Type         = type;
            Value        = value;
            _originValue = value;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void Change(float value) { Value = value; }
    }
}