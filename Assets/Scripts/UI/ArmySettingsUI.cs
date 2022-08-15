using ScriptableObjects.Events;
using ScriptableObjects.SettingsVariable;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ArmySettingsUI : MonoBehaviour
    {
        [SerializeField] private Army _army;
        public Army Army => _army;
        [SerializeField] private ArmySettingSetter[] _armySettingSetters;
        [SerializeField] private TextMeshProUGUI buttonText;

        [SerializeField] private ArmyEventChannel onRandomizeArmyEventChannel;

        public void Initialize(Army army)
        {
            _army = army;
            buttonText.text = $"Randomize {army.armyName}";
            foreach (var armySettingSetter in _armySettingSetters)
            {
                armySettingSetter.Initialize();
            }
        }

        public void RandomizeArmy()
        {
            onRandomizeArmyEventChannel.RaiseEvent(_army);
        }
    }
}
