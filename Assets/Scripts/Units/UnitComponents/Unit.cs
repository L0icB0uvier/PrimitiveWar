using System;
using System.Collections.Generic;
using CustomUnityEvents;
using Enums;
using ScriptableObjects.Events;
using ScriptableObjects.SettingsVariable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(UnitDamageable))]
    public class Unit : MonoBehaviour
    {
        [SerializeField] 
        private CharacteristicBasicValue[] _characteristicBasicValues;

        private Dictionary<ECharacteristics, int> _characteristics = new Dictionary<ECharacteristics, int>();

        public Army UnitArmy { get; private set; }

        private BoxCollider _collider;
        
        public IDamageable UnitDamageable { get; private set; }
        
        [BoxGroup("Component Reference")]
        [SerializeField] private SpriteRenderer _armyCircle;
        
        [BoxGroup("Broadcast on")]
        [SerializeField] private UnitDiedEventChannel onUnitDiedEventChannel;

        [FoldoutGroup("Unity Events")][FoldoutGroup("Unity Events/VisualPropertyEvents")][SerializeField] 
        private MeshUnityEvent _onMeshUnityChanged;
        
        [FoldoutGroup("Unity Events/VisualPropertyEvents")][SerializeField] 
        private MaterialUnityEvent onColorChanged;
        
        [FoldoutGroup("Unity Events/VisualPropertyEvents")][SerializeField] 
        private FloatUnityEvent onUnitSizeChanged;

        [FoldoutGroup("Unity Events/CharacteristicEvents")][SerializeField] 
        private IntUnityEvent onHPChanged;
        
        [FoldoutGroup("Unity Events/CharacteristicEvents")][SerializeField] 
        private IntUnityEvent onSPDChanged;
        
        [FoldoutGroup("Unity Events/CharacteristicEvents")][SerializeField] 
        private IntUnityEvent onATKChanged;
        
        [FoldoutGroup("Unity Events/CharacteristicEvents")][SerializeField] 
        private IntUnityEvent onATKSPDChanged;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            UnitDamageable = GetComponent<UnitDamageable>();
        }
        
        public void InitializeCharacteristics()
        {
            foreach (var c in _characteristicBasicValues)
            {
                SetCharacteristic(c.characteristic, c.value);
            }
        }
        
        public void InitializeUnit(Army army)
        {
            UnitArmy = army;
            _armyCircle.color = army.armyColor;
        }

        private void SetCharacteristic(ECharacteristics c, int value)
        {
            _characteristics[c] = value;
            
            switch (c)
            {
                case ECharacteristics.HP:
                    onHPChanged?.Invoke(_characteristics[c]);
                    break;
                case ECharacteristics.ATK:
                    onATKChanged?.Invoke(_characteristics[c]);
                    break;
                case ECharacteristics.SPD:
                    onSPDChanged?.Invoke(_characteristics[c]);
                    break;
                case ECharacteristics.ATKSPD:
                    onATKSPDChanged?.Invoke(_characteristics[c]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(c), c, null);
            }
        }
        
        public void UpdateCharacteristic(ECharacteristics c, int value)
        {
            SetCharacteristic(c, _characteristics[c] += value);
        }
        
        public void UpdateUnitMesh(Mesh mesh)
        {
            _onMeshUnityChanged.Invoke(mesh);
        }

        public void UpdateUnitColor(Material color)
        {
            onColorChanged.Invoke(color);
        }

        public void UpdateUnitSize(float size)
        {
            _collider.center = new Vector3(0, size / 2, 0);
            _collider.size = new Vector3(size,size, size);
            _armyCircle.transform.localScale = new Vector3(size, size, size);
            onUnitSizeChanged?.Invoke(size);
        }

        public void OnDie()
        {
            onUnitDiedEventChannel.RaiseEvent(UnitArmy,this);
            gameObject.SetActive(false);
        }
    }
}