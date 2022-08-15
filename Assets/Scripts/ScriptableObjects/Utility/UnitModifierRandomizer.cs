using Enums;
using ScriptableObjects.DataContainer;
using Units;
using Units.Modifiers;
using UnityEngine;

namespace ScriptableObjects.Utility
{
    [CreateAssetMenu(fileName = "UnitModifierRandomizer", menuName = "ScriptableObjects/Utility/UnitModifierRandomizer",
        order = 0)]
    public class UnitModifierRandomizer : DescriptionBasedSO
    {
        [SerializeField]
        private ShapeModifier[] _shapeModifiers;
        
        [SerializeField] 
        private ColorModifier[] _colorModifiers;
        
        [SerializeField] 
        private SizeModifier[] _sizeModifiers;

        public void RandomizeUnit(Unit unit)
        {
            unit.InitializeCharacteristics();
            ShapeModifier randomShapeMod = _shapeModifiers[Random.Range(0, _shapeModifiers.Length)];
            randomShapeMod.ApplyModifier(unit);
            ColorModifier randomColorMod = _colorModifiers[Random.Range(0, _colorModifiers.Length)];
            randomColorMod.ApplyModifier(unit);
            SizeModifier randomSizeMod = _sizeModifiers[Random.Range(0, _sizeModifiers.Length)];
            randomSizeMod.ApplyModifier(unit);
        }
    }
}