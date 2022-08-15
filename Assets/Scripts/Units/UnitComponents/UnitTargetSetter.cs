using System;
using System.Collections;
using CustomUnityEvents;
using Enums;
using ScriptableObjects.DataContainer;
using ScriptableObjects.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.UnitComponents
{
    public class UnitTargetSetter : MonoBehaviour
    { 
        private Unit _unit;
        private GameObject _target;
        private ETargetSelectionBehavior _targetSelectionBehavior;
       
        [BoxGroup("Data Containers")][SerializeField]
        private FightingUnits _fightingUnits;

        [BoxGroup("Data Containers")][SerializeField] 
        private FloatVariable targetUpdateRate;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel startUpdateTargetEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel stopUpdateTargetEventChannel;
        
        [FoldoutGroup("Unity Event")][SerializeField]
        private GameObjectEvent onTargetChanged;
        
        private void Awake()
        {
            _unit = GetComponent<Unit>();
            startUpdateTargetEventChannel.onEventRaised += StartUpdateTarget;
            stopUpdateTargetEventChannel.onEventRaised += StopUpdateTarget;
        }

        private void OnDestroy()
        {
            startUpdateTargetEventChannel.onEventRaised -= StartUpdateTarget;
            stopUpdateTargetEventChannel.onEventRaised -= StopUpdateTarget;
        }

        private void StartUpdateTarget()
        {
            StartCoroutine(UpdateTargetWithDelay(targetUpdateRate.value));
        }

        private void StopUpdateTarget()
        {
            StopAllCoroutines();
        }

        private void OnDisable()
        {
            StopUpdateTarget();
        }

        public void ChangeTargetSelectionBehavior(ETargetSelectionBehavior targetSelectionBehavior)
        {
            _targetSelectionBehavior = targetSelectionBehavior;
        }

        private GameObject FindNearestTarget()
        {
            GameObject closestUnit = null;
            var sqrDistanceToClosest = float.MaxValue;
            
            foreach (var army in _fightingUnits.armies)
            {
                if (army.Key == _unit.UnitArmy) continue;

                foreach (var unit in army.Value)
                {
                    var sqrDistanceToUnit = (unit.transform.position - transform.position).sqrMagnitude;
                    if (!(sqrDistanceToUnit < sqrDistanceToClosest)) continue;
                
                    closestUnit = unit.gameObject;
                    sqrDistanceToClosest = sqrDistanceToUnit;
                }
            }

            return closestUnit;
        }

        private GameObject FindLowHealthTarget()
        {
            GameObject lowestHpUnit = null;
            var lowestHpValue = float.MaxValue;

            foreach (var army in _fightingUnits.armies)
            {
                if (army.Key == _unit.UnitArmy) continue;
                
                foreach (var unit in army.Value)
                {
                    //Improve to avoid getting the component everytime
                    var unitHp = unit.UnitDamageable.CurrentHP;
                    if (unitHp > lowestHpValue) continue;
                
                    lowestHpUnit = unit.gameObject;
                    lowestHpValue = unitHp;
                }
            }
            
            return lowestHpUnit;
        }

        private void UpdateTarget()
        {
            GameObject newTarget;
            switch (_targetSelectionBehavior)
            {
                case ETargetSelectionBehavior.Closest:
                    newTarget = FindNearestTarget();
                    break;
                case ETargetSelectionBehavior.LowestHp: 
                    newTarget = FindLowHealthTarget();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (_target == newTarget) return;
            _target = newTarget;
            onTargetChanged?.Invoke(_target);
        }

        private IEnumerator UpdateTargetWithDelay(float updateRate)
        {
            while (true)
            {
                UpdateTarget();
                yield return new WaitForSeconds(updateRate);
            }
        }
    }
}