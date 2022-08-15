using DG.Tweening;
using ScriptableObjects.DataContainer;
using ScriptableObjects.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units.UnitComponents
{
    public class UnitCombat : MonoBehaviour
    {
        private GameObject _target;
        private UnitDamageable _targetDamageable;
        private bool _combatEnabled;
        private int _ATK;
        private int _ATKSPD;
        private bool _isAttacking;
        private float _attackCooldown = 0;
        
        [BoxGroup("Component Reference")][SerializeField] 
        private Transform meshTransform;
        
        [BoxGroup("Settings")][SerializeField] 
        private FloatVariable attackRange;
        
        [BoxGroup("Settings")][SerializeField] 
        private RangeFloatVariable rotateSpeed;

        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel enableCombatEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel disableCombatEventChannel;
        
        private void Awake()
        {
            enableCombatEventChannel.onEventRaised += EnableCombat;
            disableCombatEventChannel.onEventRaised += DisableCombat;
        }

        private void OnDestroy()
        {
            enableCombatEventChannel.onEventRaised -= EnableCombat;
            disableCombatEventChannel.onEventRaised -= DisableCombat;
        }

        private void OnEnable()
        {
            _attackCooldown = 0;
        }
        
        private void EnableCombat()
        {
            _combatEnabled = true;
        }

        private void DisableCombat()
        {
            _combatEnabled = false;
        }
        
        public void OnATKChanged(int ATK)
        {
            _ATK = ATK;
        }

        public void OnATKSPDChanged(int ATKSPD)
        {
            _ATKSPD = ATKSPD;
        }
        
        public void OnTargetChanged(GameObject target)
        {
            _target = target;
            _targetDamageable = _target.GetComponent<UnitDamageable>();
        }
        
        private void Update()
        {
            if(_target == null || _combatEnabled == false) return;

            var targetPos = _target.transform.position;
            Vector3 vectorToTarget = targetPos - transform.position;
            var sqrDistanceToTarget = vectorToTarget.sqrMagnitude;
            
            if (_attackCooldown > 0)
            {
                _attackCooldown -= Time.deltaTime;
                return;
            }

            if (sqrDistanceToTarget < attackRange.value * attackRange.value)
            {
                FaceTarget(targetPos);
                
                var directionToTarget = vectorToTarget.normalized;
                var angleToTarget = Vector3.Angle(directionToTarget, transform.forward);
                if (angleToTarget < 30)
                {
                    AttackTarget();
                    _attackCooldown = _ATKSPD;
                }
            }
        }
        
        private void FaceTarget(Vector3 destination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed.value);  
        }
        
        private void AttackTarget()
        {
            if(_target == null) return;
            meshTransform.DOShakeScale(.2f, .5f);
            _targetDamageable.TakeDamage(_ATK);
        }
    }
}