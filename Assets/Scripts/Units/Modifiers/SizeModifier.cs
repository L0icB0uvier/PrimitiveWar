using UnityEngine;

namespace Units.Modifiers
{
    [CreateAssetMenu(fileName = "SizeModifier", menuName = "ScriptableObjects/Units/Modifiers/SizeModifier", order = 0)]
    public class SizeModifier : Modifier
    {
        [Range(1,5)]
        public float size = 1;

        public override void ApplyModifier(Unit target)
        {
            base.ApplyModifier(target);
            target.UpdateUnitSize(size);
        }
    }
}