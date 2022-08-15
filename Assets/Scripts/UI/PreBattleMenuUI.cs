using System;
using ScriptableObjects.DataContainer;
using UnityEngine;

namespace UI
{
    public class PreBattleMenuUI : MonoBehaviour
    {
        [SerializeField] private ArmiesVariable armies;
        [SerializeField] private GameObject armySettingsPrefab;
        
        private void Start()
        {
            foreach (var army in armies.value)
            {
                var instantiatedGO = Instantiate(armySettingsPrefab, transform);
                var settingPos = Camera.main.WorldToScreenPoint(army.ArmySpawnPosition) + (Vector3)army.armySettingOffset;
                RectTransform rect = instantiatedGO.GetComponent<RectTransform>();
                Vector2 localPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, settingPos, Camera.main,out localPos);
                rect.anchoredPosition = localPos;
                
                var armySettings = instantiatedGO.GetComponent<ArmySettingsUI>();
                armySettings.Initialize(army);
            }
        }
    }
}