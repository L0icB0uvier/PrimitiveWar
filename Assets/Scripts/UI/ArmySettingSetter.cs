using System;
using System.Globalization;
using Enums;
using ScriptableObjects.SettingsVariable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ArmySettingSetter : MonoBehaviour
    {
        [SerializeField] private EArmySettings armySettings;
        [SerializeField] private ArmySettingsUI _armySettingsUI;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _settingValueText;
        
        public void Initialize()
        {
            switch (armySettings)
            {
                case EArmySettings.ArmySize:
                    _slider.onValueChanged.AddListener(_armySettingsUI.Army.UpdateArmySizeSetting);
                    _armySettingsUI.Army.onArmySizeChanged += UpdateText;
                    _slider.value = _armySettingsUI.Army.armySize;
                    _slider.minValue = Army.armyMinSize;
                    _slider.maxValue = Army.armyMaxSize;
                    break;
                case EArmySettings.UnitPerRaw:
                    _slider.onValueChanged.AddListener(_armySettingsUI.Army.UpdateUnitPerRawSetting);
                    _armySettingsUI.Army.onUnitPerRawChanged += UpdateText;
                    _slider.value = _armySettingsUI.Army.unitPerRaw;
                    _slider.minValue = Army.minUnitPerRaw;
                    _slider.maxValue = Army.maxUnitPerRaw;
                    break;
                case EArmySettings.UnitDistance:
                    _slider.onValueChanged.AddListener(_armySettingsUI.Army.UpdateUnitDistanceSetting);
                    _armySettingsUI.Army.onUnitDistanceChanged += UpdateText;
                    _slider.value = _armySettingsUI.Army.unitDistance;
                    _slider.minValue = Army.minUnitDistance;
                    _slider.maxValue = Army.maxUnitDistance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            UpdateText(_armySettingsUI.Army);
        }
        
        private void UpdateText(Army army)
        {
            switch (armySettings)
            {
                case EArmySettings.ArmySize:
                    _settingValueText.text = army.armySize.ToString();
                    break;
                case EArmySettings.UnitPerRaw:
                    _settingValueText.text = army.unitPerRaw.ToString();
                    break;
                case EArmySettings.UnitDistance:
                    _settingValueText.text = army.unitDistance.ToString(CultureInfo.InvariantCulture);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
