using CustomUnityEvents;
using ScriptableObjects.Events;
using ScriptableObjects.SettingsVariable;
using Units;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private ArmyEventChannel battleEndedEventChannel;

        [SerializeField] private ArmyEvent onBattleEnded;

        private void Awake()
        {
            battleEndedEventChannel.onEventRaised += OnBattleEnded;
        }

        private void OnBattleEnded(Army winningArmy)
        {
            onBattleEnded?.Invoke(winningArmy);
        }
    }
}
