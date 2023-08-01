// ----- C#
using System.Collections.Generic;

namespace AbilitySystem.Mutable
{
    public class AbilityValue
    {
        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAbilityType Type  { get; private set; } = EAbilityType.Unknown;
        public float        Value { get; private set; } = 0.0f;

        // --------------------------------------------------
        // Constuctor
        // --------------------------------------------------
        public AbilityValue ( EAbilityType type, float value )
        {
            Type  = type;
            Value = value;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void Set(float value) 
        { 
            
        }

        public void Revert() 
        { 
        
        }

        // ----- Private
        private void _Remove() 
        { 

        }
    }
}