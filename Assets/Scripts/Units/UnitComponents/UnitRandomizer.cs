using ScriptableObjects.Events;
using ScriptableObjects.SettingsVariable;
using ScriptableObjects.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.UnitComponents
{
    [RequireComponent(typeof(Unit))]
    public class UnitRandomizer : MonoBehaviour
    {
        private Unit _unit;
        private bool _canRandomize = true;
        
        [SerializeField] 
        private bool randomizeOnStart;
        
        [SerializeField] 
        private UnitModifierRandomizer _unitModifierRandomizer;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel forbidRandomizationEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel allowRandomizationEventChannel;
        
        [BoxGroup("Listening to")][SerializeField]
        private ArmyEventChannel randomizeArmyEventChannel;
        
        private void Awake()
        {
            _unit = GetComponent<Unit>();
            forbidRandomizationEventChannel.onEventRaised += ForbidUnitRandomization;
            allowRandomizationEventChannel.onEventRaised += AuthorizeUnitRandomization;
            randomizeArmyEventChannel.onEventRaised += RandomizeArmy;
        }

        private void RandomizeArmy(Army army)
        {
            if (_unit.UnitArmy != army) return;
            RandomizeUnit();
        }

        private void OnDestroy()
        {
            forbidRandomizationEventChannel.onEventRaised -= ForbidUnitRandomization;
            allowRandomizationEventChannel.onEventRaised -= AuthorizeUnitRandomization;
            randomizeArmyEventChannel.onEventRaised -= RandomizeArmy;
        }

        private void Start()
        {
            if (randomizeOnStart == true)
            {
                RandomizeUnit();
            }
        }

        private void ForbidUnitRandomization()
        {
            _canRandomize = false;
        }

        private void AuthorizeUnitRandomization()
        {
            _canRandomize = true;
        }
        
        public void RandomizeUnit()
        {
            if (_canRandomize == false) return;
            _unitModifierRandomizer.RandomizeUnit(_unit);
        }
    }
}
