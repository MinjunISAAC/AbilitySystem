// ----- C#
using System.Collections.Generic;

// ----- User Defined
using InGame.ForAbility;

namespace Utility.ForDefault
{
    [System.Serializable]
    public class DefaultAbilityData
    {
        public string Type;
        public float  DefaultValue;
    }

    [System.Serializable]
    public class DefaultDataSet
    {
        public List<DefaultAbilityData> DataSet = new List<DefaultAbilityData>();
    }
}