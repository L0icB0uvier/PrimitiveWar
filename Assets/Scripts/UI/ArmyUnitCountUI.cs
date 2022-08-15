using System;
using ScriptableObjects.DataContainer;
using ScriptableObjects.SettingsVariable;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ArmyUnitCountUI : MonoBehaviour
    {
        [SerializeField] private Army _army;
        [SerializeField] private FightingUnits _currentFighting;

        [SerializeField] private TextMeshProUGUI _armyNameText;
        [SerializeField] private TextMeshProUGUI _unitCountText;
        
        private void OnEnable()
        {
            _currentFighting.onArmyUpdated += UpdateArmyUnitCount;
        }

        private void OnDisable()
        {
            _currentFighting.onArmyUpdated -= UpdateArmyUnitCount;
        }

        public void SetArmy(Army army)
        {
            _army = army;
        }

        public void Initialize()
        {
            _armyNameText.text = _army.armyName;
            UpdateArmyUnitCount(_army);
        }

        private void UpdateArmyUnitCount(Army army)
        {
            if (army == _army)
            {
                _unitCountText.text = _currentFighting.armies.ContainsKey(army)?  _currentFighting.armies[army].Count
                .ToString() : "0";
            }
        }
    }
}