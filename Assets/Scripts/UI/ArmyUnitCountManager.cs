using ScriptableObjects.DataContainer;
using UnityEngine;

namespace UI
{
    public class ArmyUnitCountManager : MonoBehaviour
    {
        [SerializeField] private GameObject armyUnitCountPrefab;
        [SerializeField] private GameObject separatorPrefab;
        
        [SerializeField] private ArmiesVariable _armies;
        private ArmyUnitCountUI[] _armyUnitCountUIs;

        private void Awake()
        {
            SpawnUIElements();
        }

        private void SpawnUIElements()
        {
            _armyUnitCountUIs = new ArmyUnitCountUI[_armies.value.Length];
            for (int i = 0; i < _armies.value.Length; i++)
            {
                var go = Instantiate(armyUnitCountPrefab, transform);
                
                _armyUnitCountUIs[i] = go.GetComponent<ArmyUnitCountUI>();
                _armyUnitCountUIs[i].SetArmy(_armies.value[i]);

                if (i < _armies.value.Length - 1)
                {
                    Instantiate(separatorPrefab, transform);
                }
            }
        }

        private void OnEnable()
        {
            foreach (var armyUnitCountUI in _armyUnitCountUIs)
            {
                armyUnitCountUI.Initialize();
            }
        }
    }
}