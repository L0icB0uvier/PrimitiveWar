using ScriptableObjects.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Units
{
    public class UnitDamageable : MonoBehaviour, IDamageable
    {
        private int _HP;
        public int CurrentHP { get; private set; }

        [SerializeField] 
        private VoidEventChannel[] resetCurrentHPEventChannel;
        
        [FoldoutGroup("UnityEvent")][SerializeField]
        private UnityEvent onHPEmpty;

        private void Awake()
        {
            foreach (var eventChannel in resetCurrentHPEventChannel)
            {
                eventChannel.onEventRaised += ResetCurrentHP;
            }
        }

        private void OnDestroy()
        {
            foreach (var eventChannel in resetCurrentHPEventChannel)
            {
                eventChannel.onEventRaised -= ResetCurrentHP;
            }
        }

        public void OnHPChanged(int HP)
        {
            _HP = HP;
            ResetCurrentHP();
        }

        public void ResetCurrentHP()
        {
            CurrentHP = _HP;
        }

        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP <= 0)
            {
                onHPEmpty?.Invoke();
            }
        }
    }
}