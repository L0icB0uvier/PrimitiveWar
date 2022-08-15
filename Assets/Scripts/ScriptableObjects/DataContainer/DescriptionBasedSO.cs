using UnityEngine;

namespace ScriptableObjects.DataContainer
{
    public abstract class DescriptionBasedSO : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string developerDescription = "";
#endif
    }
}