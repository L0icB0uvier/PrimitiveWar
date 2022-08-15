using ScriptableObjects.SettingsVariable;
using Units;
using UnityEngine;

namespace ScriptableObjects.DataContainer
{
    [CreateAssetMenu(fileName = "ArmiesVariable", menuName = "ScriptableObjects/DataContainer/ArmiesVariable", order = 0)]
    public class ArmiesVariable : DescriptionBasedSO
    {
        public Army[] value;
    }
}