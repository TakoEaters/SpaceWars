using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.General.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.AI
{
    public class AIStates : MonoBehaviour
    {
        [SerializeField] private float _threshold = 3f;
        private NavMeshAgent _agent;
        private AIConfigs _configs;
        private IWaypointsPath _waypointsPath;
        private Transform _target;

        public void Initialize(AIConfigs configs, NavMeshAgent agent)
        {
            _waypointsPath = ServiceLocator.Current.Get<IWaypointsPath>();
            _agent = agent;
            _configs = configs;
        }

        public void Enable()
        {
            _agent.enabled = true;
            _target = _waypointsPath.GetFutureTarget();
            _agent.destination = _target.position;
        }

        public void UpdateStates()
        {
            if (Vector3.Distance(transform.position, _target.position) < _threshold)
            {
                MoveRandom();
            }
            OnMove();
        }

        public void SetTarget(Vector3 damageablePosition)
        {
            _agent.destination = damageablePosition;
        }

        public void MoveRandom()
        {
            _target = _waypointsPath.GetFutureTarget();
            _agent.SetDestination(_target.position);
        }

        private void OnMove()
        {
            transform.LookAt(_target.position.SetY(transform.position.y), _configs.RotationSpeed);
        }

        public void Disable()
        {
            _agent.enabled = false;
        }
    }
}
