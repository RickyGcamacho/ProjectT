using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemy.FiniteStateMachine.States
{
    public class Patrolling : BaseState
    {
        private readonly Transform[] _waypoints;
        private Transform _currentTarget;
        private int _targetIndex;
        
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly NavMeshAgent _agent;
        
        private readonly float _speed;
        private readonly float _distanceToChangeWaypoint;
        private readonly float _distanceToChase;
        
        private float _timerToChange;
        
        private readonly LayerMask _layerMask;

        // ReSharper disable once SuggestBaseTypeForParameter
        public Patrolling(PatientStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToChangeWaypoint, float distanceToChase, Transform[] waypoints, LayerMask layerMask) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
            
            _myTransform = myTransform;
            _player = player;
            
            _agent = agent;
            _speed = speed;
            _agent.speed = speed;
            _agent.acceleration = speed * 4;
            
            _distanceToChangeWaypoint = distanceToChangeWaypoint;
            _distanceToChase = distanceToChase;

            _waypoints = waypoints;

            _layerMask = layerMask;
        }

        public override void Enter()
        {
            _targetIndex = Random.Range(0, _waypoints.Length);
            _currentTarget = _waypoints[_targetIndex];
            _agent.SetDestination(_currentTarget.position);

            _agent.speed = _speed;
            _agent.acceleration = _speed;
        }

        public override void UpdatePhysics()
        {
            var myPosition = _myTransform.position;
            var ray = new Ray(myPosition, (_player.position - myPosition).normalized);
            
            if (Physics.Raycast(ray, out var hit, _distanceToChase, _layerMask, QueryTriggerInteraction.Collide))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    stateMachine.ChangeState(((PatientStateMachine) stateMachine).pursuingState);
                    return;
                }
            }

            var directionalVector = _currentTarget.position - myPosition;

            if (directionalVector.magnitude < _distanceToChangeWaypoint)
            {
                ChangeWaypoint();
            }
        }

        private void ChangeWaypoint()
        {
            _timerToChange += Time.fixedDeltaTime;

            if (_timerToChange < 4) return;
            
            _timerToChange = 0;
                
            Debug.Log("Change waypoint");
            var chooseDifferent = _targetIndex;
                
            while (chooseDifferent == _targetIndex)
            {
                _targetIndex = Random.Range(0, _waypoints.Length);
            }

            _currentTarget = _waypoints[_targetIndex];
            Debug.Log(_targetIndex);
            _agent.SetDestination(_currentTarget.position);
        }
    }
}