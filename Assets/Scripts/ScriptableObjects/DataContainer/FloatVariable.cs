using UnityEngine;

namespace ScriptableObjects.DataContainer
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "ScriptableObjects/DataContainer/FloatVariable", order = 0)]
    public class FloatVariable : DescriptionBasedSO
    {
        public float value;
    }
}