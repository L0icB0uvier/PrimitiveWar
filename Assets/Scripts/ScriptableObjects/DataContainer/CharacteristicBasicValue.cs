using Enums;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "CharacteristicBasicValue", menuName = "ScriptableObjects/Units/CharacteristicBasicValue", order = 0)]
    public class CharacteristicBasicValue : ScriptableObject
    {
        public ECharacteristics characteristic;
        public int value;
    }
}