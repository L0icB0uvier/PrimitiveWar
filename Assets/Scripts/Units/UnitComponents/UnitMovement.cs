using ScriptableObjects.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Units.UnitComponents
{
    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Transform _target;
        private bool _canMove;

        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel startMovingEventChannel;
        
        [BoxGroup("Listening to")][SerializeField] 
        private VoidEventChannel stopMovingEventChannel;
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            startMovingEventChannel.onEventRaised += EnableMovement;
            stopMovingEventChannel.onEventRaised += DisableMovement;
        }

        private void OnDestroy()
        {
            startMovingEventChannel.onEventRaised -= EnableMovement;
            stopMovingEventChannel.onEventRaised -= DisableMovement;
        }

        public void OnUnitSpeedChanged(int s)
        {
            _navMeshAgent.speed = s;
        }

        public void OnTargetChanged(GameObject target)
        {
            _target = target.transform;
        }

        private void Update()
        {
            if (_canMove == false || _target == null) return;
            _navMeshAgent.SetDestination(_target.position);
        }
        
        private void EnableMovement()
        {
            _canMove = true;
            _navMeshAgent.enabled = true;
        }

        private void DisableMovement()
        {
            _canMove = false;
            _navMeshAgent.enabled = false;
        }
    }
}