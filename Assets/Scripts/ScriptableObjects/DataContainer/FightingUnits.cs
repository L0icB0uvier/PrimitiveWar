using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.SettingsVariable;
using Sirenix.OdinInspector;
using Units;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.DataContainer
{
    [CreateAssetMenu(fileName = "ArmiesUnitsVariable", menuName = "ScriptableObjects/DataContainer/ArmiesUnitsVariable", order = 0)]
    public class FightingUnits : DescriptionBasedSO
    {
        public Dictionary<Army, HashSet<Unit>> armies = new Dictionary<Army, HashSet<Unit>>();
        public UnityAction<Army> onArmyUpdated;

        public void AddUnitToArmy(Army army, Unit unit)
        {
            if (armies.ContainsKey(army) == false)
            {
                armies[army] = new HashSet<Unit>() {unit};
            }
            
            if (armies[army].Contains(unit)) return;
            armies[army].Add(unit);
            onArmyUpdated?.Invoke(army);
        }

        public void RemoveUnitFromArmy(Army army, Unit unit)
        {
            if (armies.ContainsKey(army) == false || !armies[army].Contains(unit)) return;
            armies[army].Remove(unit);
            onArmyUpdated?.Invoke(army);
        }

        public bool IsTeamEmpty(Army army)
        {
            if (armies.ContainsKey(army) == false) return true;
            return armies[army].Count == 0;
        }

        public void ClearArmy(Army army)
        {
            if (armies.ContainsKey(army) == false) return;
            armies[army].Clear();
        }

        public void RemoveArmy(Army army)
        {
            if (armies.ContainsKey(army) == false) return;
            armies.Remove(army);
        }

        public void Reset()
        {
            armies.Clear();
        }
    }
}