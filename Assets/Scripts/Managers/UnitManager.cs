using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.DataContainer;
using ScriptableObjects.Events;
using ScriptableObjects.SettingsVariable;
using Sirenix.OdinInspector;
using Units;
using UnityEngine;

namespace Managers
{
    public class UnitManager : SerializedMonoBehaviour
    {
        private class UnitInfo
        {
            public Unit Unit { get; private set; }
            public Vector3 PreBattlePosition { get; private set; }
            public Quaternion PreBattleRotation { get; private set; }
            

            public UnitInfo(Unit unit)
            {
                Unit = unit;
            }

            public void SetUnitInfo(Vector3 pos, Quaternion rot)
            {
                PreBattlePosition = pos;
                PreBattleRotation = rot;
            }
        }
        
        [SerializeField] 
        private FightingUnits _fightingUnitsContainer;

        [Tooltip("Scriptable Object that contains reference to armies involved in the battle")][SerializeField] 
        private ArmiesVariable armies;
        
        private Dictionary<Army, List<UnitInfo>> _armiesUnits = new Dictionary<Army, List<UnitInfo>>();
        
        [BoxGroup("Broadcasting on")][SerializeField] 
        private ArmyEventChannel battleEndedEventChannel;        
        
        [BoxGroup("Broadcasting on")][SerializeField] 
        private ArmyEventChannel randomizeArmyEventChannel;
        
        [BoxGroup("Broadcasting on")][SerializeField] 
        private VoidEventChannel startFightingEventChannel;
        
        [BoxGroup("Broadcasting on")][SerializeField] 
        private VoidEventChannel stopFightingEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private UnitDiedEventChannel onUnitDiedEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel initializeArmiesEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private ArmyEventChannel randomizeArmyButtonClickedEventChannel;
        
        [FoldoutGroup("Prefab reference")][SerializeField] 
        private GameObject unitPrefab;
        
        private void Awake()
        {
            onUnitDiedEventChannel.onEventRaised += RemoveUnitFromArmy;
            initializeArmiesEventChannel.onEventRaised += ResetArmies;
            randomizeArmyButtonClickedEventChannel.onEventRaised += RandomizeArmy;

            foreach (var army in armies.value)
            {
                army.onArmySizeChanged += ChangeArmySize;
                army.onUnitPerRawChanged += SetArmyUnitsPosition;
                army.onUnitDistanceChanged += SetArmyUnitsPosition;
            }
        }

        private void OnDestroy()
        {
            onUnitDiedEventChannel.onEventRaised -= RemoveUnitFromArmy;
            initializeArmiesEventChannel.onEventRaised -= ResetArmies;
            randomizeArmyButtonClickedEventChannel.onEventRaised -= RandomizeArmy;
            
            foreach (var army in armies.value)
            {
                army.onArmySizeChanged -= ChangeArmySize;
                army.onUnitPerRawChanged -= SetArmyUnitsPosition;
                army.onUnitDistanceChanged -= SetArmyUnitsPosition;
            }
        }
        
        private void Start()
        {
            CreateArmies();
        }
        
        private void DestroyArmy(Army army)
        {
            foreach (var unitInfo in _armiesUnits[army])
            {
                Destroy(unitInfo.Unit.gameObject);
            }

            _armiesUnits.Remove(army);
        }
        
        private void DestroyUnit(Army army, UnitInfo unitInfo)
        {
            if (_armiesUnits[army].Any(x => x.Unit) == false) return;
            Destroy(unitInfo.Unit.gameObject);
            _armiesUnits[army].Remove(unitInfo);
        }
        
        private void ChangeArmySize(Army army)
        {
            //Remove units
            if (army.armySize < _armiesUnits[army].Count)
            {
                var unitToDestroy = _armiesUnits[army].Count - army.armySize;
                for (int i = 0; i < unitToDestroy; i++)
                {
                    DestroyUnit(army, _armiesUnits[army][_armiesUnits[army].Count - 1 - i]);
                }
            }

            //Add units
            else
            {
                var unitToCreate = army.armySize - _armiesUnits[army].Count;
                for (int i = 0; i < unitToCreate; i++)
                {
                    CreateUnit(army);
                }
            }
            
            SetArmyUnitsPosition(army);
        }

        private void CreateArmies()
        {
            foreach (var army in armies.value)
            {
                CreateArmy(army);
                SetArmyUnitsPosition(army);
            }
        }

        private void CreateArmy(Army army)
        {
            if (_armiesUnits.ContainsKey(army))
            {
                DestroyArmy(army);
            }
            
            _armiesUnits.Add(army, new List<UnitInfo>());
            for (int u = 0; u < army.armySize; u++)
            {
                CreateUnit(army);
            }
        }

        private void CreateUnit(Army army)
        {
            var spawnedGO = Instantiate(unitPrefab);
            var unit = spawnedGO.GetComponent<Unit>();
            _armiesUnits[army].Add(new UnitInfo(unit));
            SetupUnit(unit, army);
        }

        //Reset Army to pre battle state
        private void ResetArmies()
        {
            foreach (var army in _armiesUnits)
            {
                foreach (var unitInfo in army.Value)
                {
                    var unitTransform = unitInfo.Unit.transform;
                    unitTransform.position = unitInfo.PreBattlePosition;
                    unitTransform.rotation = unitInfo.PreBattleRotation;
                    if (unitTransform.gameObject.activeInHierarchy == false)
                    {
                        unitTransform.gameObject.SetActive(true);
                    }
                }
            }
        }

        //Organize Army Units following the Army settings
        private void SetArmyUnitsPosition(Army army)
        {
            Vector3 leaderPos = army.ArmySpawnPosition;
            Quaternion armyRotation = army.ArmyLookingDirection;
            int colIndex = 0;

            int unitIndex = 0;

            foreach (var unitInfo in _armiesUnits[army])
            {
                Unit unit = unitInfo.Unit;
                Transform unitTransform = unit.transform;
                unitTransform.rotation = armyRotation;
                
                Vector3 unitForwardVector = unitTransform.forward;
                Vector3 unitRightVector = unitTransform.right;
                
                //Set raw position
                int raw = unitIndex / army.unitPerRaw;
                
                //Set column position
                float col;
                int i = unitIndex % army.unitPerRaw;
                
                if (i == 0)
                {
                    colIndex = 0;
                    col = 0;
                }
                else if (i % 2 == 0)
                {
                    col = -colIndex;
                    
                }
                else
                {
                    colIndex++;
                    col = colIndex;
                }
                
                Vector3 unitPos = leaderPos + (-unitForwardVector * raw * army.unitDistance) +
                                  unitRightVector * col * army.unitDistance;

                
                unit.transform.position = unitPos;
                
                //Save unit position and rotation
                unitInfo.SetUnitInfo(unitPos, armyRotation);
                unitIndex++;
            }
        }
        
        private void SetupUnit(Unit unit, Army unitArmy)
        {
            unit.InitializeUnit(unitArmy);
        }

        //Initialize currentArmiesUnits SO variable with all units in each armies and send Start Fighting Event
        public void PrepareBattle()
        {
            _fightingUnitsContainer.Reset();
            
            foreach (var army in armies.value)
            {
                foreach (var unitInfo in _armiesUnits[army])
                {
                    _fightingUnitsContainer.AddUnitToArmy(army, unitInfo.Unit);
                }
            }
            
            startFightingEventChannel.RaiseEvent();
        }
        
        private void RandomizeArmy(Army army)
        {
            randomizeArmyEventChannel.RaiseEvent(army);
        }

        //Remove the unit from the fighting units in the specified army
        private void RemoveUnitFromArmy(Army army, Unit unit)
        {
            _fightingUnitsContainer.RemoveUnitFromArmy(army, unit);
            
            if (!_fightingUnitsContainer.IsTeamEmpty(army)) return;
            
            _fightingUnitsContainer.RemoveArmy(army);
            ArmyDefeated();
        }

        private void ArmyDefeated()
        {
            if (_fightingUnitsContainer.armies.Count == 1)
            {
                foreach (var army in armies.value)
                {
                    if (_fightingUnitsContainer.armies.ContainsKey(army) == false) continue;
                    battleEndedEventChannel.RaiseEvent(army);
                    stopFightingEventChannel.RaiseEvent();
                }
            }
        }
    }
}