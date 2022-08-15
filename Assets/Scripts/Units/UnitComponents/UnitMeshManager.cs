using UnityEngine;

namespace Units.UnitComponents
{
    public class UnitMeshManager : MonoBehaviour
    {
        public void UpdateScale(float newScale)
        {
            var meshTransform = transform;
            meshTransform.localScale = new Vector3(newScale, newScale, newScale);
            meshTransform.localPosition = new Vector3(0, newScale / 2, 0);
        }
    }
}