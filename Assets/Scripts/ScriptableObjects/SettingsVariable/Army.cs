using System;
using ScriptableObjects.DataContainer;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.SettingsVariable
{
    [CreateAssetMenu(fileName = "Army", menuName = "ScriptableObjects/SettingsVariable/Army", order = 0)]
    public class Army : ScriptableObject
    {
        public string armyName;
        public Color armyColor;
        
        public Vector3 ArmySpawnPosition;
        public Quaternion ArmyLookingDirection;
        public Vector2 armySettingOffset;

        [PropertyRange("armyMinSize","armyMaxSize")]
        public int armySize = 20;
        
        [PropertyRange("minUnitPerRaw","maxUnitPerRaw")]
        public int unitPerRaw = 5;
        
        [PropertyRange("minUnitDistance","maxUnitDistance")]
        public float unitDistance = 2.5f;

        public UnityAction<Army> onArmySizeChanged;
        public UnityAction<Army> onUnitPerRawChanged;
        public UnityAction<Army> onUnitDistanceChanged;
        
        [FoldoutGroup("ArmyGeneralSettings")][ShowInInspector]
        public static int armyMinSize = 1;
        
        [FoldoutGroup("ArmyGeneralSettings")][ShowInInspector]
        public static int armyMaxSize = 40;
        
        [FoldoutGroup("ArmyGeneralSettings")][ShowInInspector]
        public static int minUnitPerRaw = 4;
        
        [FoldoutGroup("ArmyGeneralSettings")][ShowInInspector]
        public static int maxUnitPerRaw = 10;
        
        [FoldoutGroup("ArmyGeneralSettings")][ShowInInspector]
        public static float minUnitDistance = 2.5f;
        
        [FoldoutGroup("ArmyGeneralSettings")][ShowInInspector]
        public static float maxUnitDistance = 5f;
        
        public void UpdateArmySizeSetting(Single value)
        {
            armySize = (int)value;
            onArmySizeChanged.Invoke(this);
        }
        public void UpdateUnitPerRawSetting(Single value)
        {
            unitPerRaw = (int)value;
            onUnitPerRawChanged.Invoke(this);
        }
        public void UpdateUnitDistanceSetting(Single value)
        {
            unitDistance = Mathf.Round(value * 10f) / 10f;
            onUnitDistanceChanged.Invoke(this);
        }
    }
}