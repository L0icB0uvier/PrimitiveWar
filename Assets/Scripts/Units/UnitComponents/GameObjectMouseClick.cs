using UnityEngine;
using UnityEngine.Events;

namespace Units.UnitComponents
{
    public class GameObjectMouseClick : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onGameObjectClicked;

        private void OnMouseDown()
        {
            onGameObjectClicked?.Invoke();
        }
    }
}