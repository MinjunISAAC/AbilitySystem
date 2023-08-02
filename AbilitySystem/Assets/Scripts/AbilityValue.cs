// ----- C#
using System.Collections.Generic;

namespace AbilitySystem.Mutable
{
    public class AbilityValue
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<float>  _valueSet = new List<float>();

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EAbilityType Type         { get; private set; } = EAbilityType.Unknown;
        public float        CurrentValue
        {
            get
            {
                var lastIndex = _valueSet.Count;
                return _valueSet[lastIndex];
            }
        }

        // --------------------------------------------------
        // Constuctor
        // --------------------------------------------------
        public AbilityValue ( EAbilityType type, float value )
        {
            Type = type;
            _valueSet.Add( value );
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void Set(float value) 
        {
            _valueSet.Add(value);
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