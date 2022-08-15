using ScriptableObjects.DataContainer;
using ScriptableObjects.SettingsVariable;
using Units;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "ArmyEventChannel", menuName = "ScriptableObjects/EventChannels/ArmyEventChannel", order = 0)]
    public class ArmyEventChannel : DescriptionBasedSO
    {
        public UnityAction<Army> onEventRaised;

        public void RaiseEvent(Army army)
        {
            onEventRaised?.Invoke(army);
        }
    }
}