using Enums;
using Units.UnitComponents;
using UnityEngine;

namespace Units.Modifiers
{
    [CreateAssetMenu(fileName = "ShapeModifier", menuName = "ScriptableObjects/Units/Modifiers/ShapeModifier", order = 0)]
    public class ShapeModifier : Modifier
    {
        public Mesh mesh;
        public ETargetSelectionBehavior targetSelectionBehavior;
        
        public override void ApplyModifier(Unit target)
        {
            base.ApplyModifier(target);
            target.UpdateUnitMesh(mesh);
            target.GetComponent<UnitTargetSetter>().ChangeTargetSelectionBehavior(targetSelectionBehavior);
        }
    }
}