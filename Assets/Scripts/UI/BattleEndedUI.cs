using ScriptableObjects.SettingsVariable;
using TMPro;
using Units;
using UnityEngine;

namespace UI
{
    public class BattleEndedUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winnerDisplayText;
    
        public void DisplayUI(Army winningArmy)
        {
            _winnerDisplayText.text = $"{winningArmy.armyName} Won!";
            gameObject.SetActive(true);
        }
    }
}
