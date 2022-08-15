using UnityEngine;

namespace ScriptableObjects.DataContainer
{
    [CreateAssetMenu(fileName = "RangeFloatVariable", menuName = "ScriptableObjects/DataContainer/RangeFloatVariable", order = 0)]
    public class RangeFloatVariable : DescriptionBasedSO
    {
        [Range(0, 1)]
        public float value;
    }
}