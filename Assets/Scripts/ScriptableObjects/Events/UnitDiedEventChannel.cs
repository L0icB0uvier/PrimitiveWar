using Enums;
using ScriptableObjects.DataContainer;
using ScriptableObjects.SettingsVariable;
using Units;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "UnitDiedEventChannel", menuName = "ScriptableObjects/EventChannels/UnitDiedEventChannel", order = 0)]
    public class UnitDiedEventChannel : DescriptionBasedSO
    {
        public UnityAction<Army, Unit> onEventRaised;

        public void RaiseEvent(Army army, Unit unit)
        {
            onEventRaised?.Invoke(army, unit);
        }
    }
}