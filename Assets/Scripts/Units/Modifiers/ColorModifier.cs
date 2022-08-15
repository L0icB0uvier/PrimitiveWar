using UnityEngine;

namespace Units.Modifiers
{
    [CreateAssetMenu(fileName = "ColorModifier", menuName = "ScriptableObjects/Units/Modifiers/ColorModifier", order = 0)]
    public class ColorModifier : Modifier
    {
        public Material material;
        public override void ApplyModifier(Unit target)
        {
            base.ApplyModifier(target);
            target.UpdateUnitColor(material);
        }
    }
}