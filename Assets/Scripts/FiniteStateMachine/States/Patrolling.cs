using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine.States
{
    public class Patrolling : BaseState
    {
        private readonly Transform[] _waypoints;
        private Transform _currentTarget;
        private int _targetIndex;
        
        private readonly Transform _myTransform;
        private readonly Rigidbody _rigidBody;
        private readonly NavMeshAgent _agent;
        
        //private readonly float _speed;
        private readonly float _distanceToChangeWaypoint;
        private readonly float _distanceToDetect;

        private readonly Transform _player;

        private float _timerToChange;

        // ReSharper disable once SuggestBaseTypeForParameter
        public Patrolling(PatientStateMachine stateMachine, Transform myTransform, Rigidbody rigidBody, NavMeshAgent agent, float speed, float distanceToChangeWaypoint, float distanceToDetect, Transform[] waypoints, Transform player) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
            
            _myTransform = myTransform;
            _rigidBody = rigidBody;
            
            _agent = agent;
            agent.speed = speed;
            agent.acceleration = speed;
            //_speed = speed;
            
            _distanceToChangeWaypoint = distanceToChangeWaypoint;
            _distanceToDetect = distanceToDetect;

            _waypoints = waypoints;
            _player = player;
        }

        public override void Enter()
        {
            _targetIndex = Random.Range(0, _waypoints.Length);
            _currentTarget = _waypoints[_targetIndex];
            _agent.SetDestination(_currentTarget.position);
        }

        public override void UpdatePhysics()
        {
            if ((_player.position - _myTransform.position).magnitude < _distanceToDetect)
            {
                stateMachine.ChangeState(((PatientStateMachine) stateMachine).pursuingState);
                return;
            }
            
            var directionalVector = _currentTarget.position - _myTransform.position;

            if (directionalVector.magnitude < _distanceToChangeWaypoint)
            {
                ChangeWaypoint();
            }
        }

        private void ChangeWaypoint()
        {
            _timerToChange += Time.deltaTime;

            if (_timerToChange < 3) return; // After this time has past we change the waypoint
            
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