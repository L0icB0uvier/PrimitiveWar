using ScriptableObjects.DataContainer;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "VoidEventChannel", menuName = "ScriptableObjects/EventChannels/VoidEventChannel", order = 0)]
    public class VoidEventChannel : DescriptionBasedSO
    {
        public UnityAction onEventRaised;

        public void RaiseEvent()
        {
            onEventRaised?.Invoke();
        }
    }
}