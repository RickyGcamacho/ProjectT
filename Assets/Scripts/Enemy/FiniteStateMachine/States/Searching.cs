using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.FiniteStateMachine.States
{
    public class Searching : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly NavMeshAgent _agent;
        
        private readonly float _speed;
        private readonly float _distanceToAttack;
        private readonly float _distanceToChase;
        
        private readonly Transform[] _waypoints;
        private readonly LayerMask _layerMask;
        private Vector3 _playerLastKnownPosition;

        private readonly List<Transform> _transformsToSearch = new List<Transform>();
        
        private Transform _currentTarget;

        private bool _firstSearch;
        private float _timerBeforePatrol;
        private float _timerBeforeChangingWaypoint;
        
        public Searching(StateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float distanceToAttack, float distanceToChase, Transform[] waypoints, LayerMask layerMask) : base(stateMachine)
        {
            _myTransform = myTransform;
            _player = player;
            _agent = agent;

            _speed = speed;
            _distanceToAttack = distanceToAttack;
            _distanceToChase = distanceToChase;

            _waypoints = waypoints;
            _layerMask = layerMask;
        }

        public override void Enter()
        {
            _playerLastKnownPosition = ((PatientStateMachine) stateMachine).pursuingState.playerLastKnownPosition;

            _agent.SetDestination(_playerLastKnownPosition);
            _agent.speed = _speed;
            _agent.acceleration = _speed;
        }

        public override void Exit()
        {
            _transformsToSearch.Clear();
            _currentTarget = null;
            _firstSearch = false;
            _timerBeforePatrol = 0;
        }

        public override void UpdatePhysics()
        {
            var myPosition = _myTransform.position;
            var vectorToPlayer = _player.position - myPosition;
            
            var ray = new Ray(myPosition, vectorToPlayer.normalized);
            
            var distanceToPlayer = vectorToPlayer.magnitude;

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _layerMask, QueryTriggerInteraction.Ignore)) return;
            
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                if (distanceToPlayer < _distanceToAttack)
                {
                    stateMachine.ChangeState(((PatientStateMachine) stateMachine).attackingState); Debug.Log($"I change state to {((PatientStateMachine) stateMachine).attackingState}");
                }
                else if (distanceToPlayer < _distanceToChase)
                {
                    stateMachine.ChangeState(((PatientStateMachine) stateMachine).pursuingState); Debug.Log($"I change state to {((PatientStateMachine) stateMachine).pursuingState}");
                }
            }
            else if (!_agent.hasPath && !_firstSearch)
            {
                _firstSearch = true;
                AddWaypointsToSearch();
            }
            else if (!_agent.hasPath && _firstSearch)
            {
                SearchNearbyWaypoints();
            }
        }

        private void AddWaypointsToSearch()
        {
            foreach (var waypoint in _waypoints)
            {
                var myPosition = _myTransform.position;

                if (!Physics.Raycast(myPosition, waypoint.position - myPosition, out var hitWaypoint, 30, _layerMask)) continue;
                
                if (waypoint.gameObject.GetInstanceID() == hitWaypoint.transform.gameObject.GetInstanceID())
                {
                    _transformsToSearch.Add(waypoint);
                }
            } Debug.Log($"I have {_transformsToSearch.Count} waypoint to search");
        }

        private void SearchNearbyWaypoints()
        {
            if (!_currentTarget)
            {
                if (_transformsToSearch.Count > 0)
                {
                    var index = Random.Range(0, _transformsToSearch.Count);  Debug.Log($"I have {_transformsToSearch.Count} waypoints left");
                    _currentTarget = _transformsToSearch[index];
                    _transformsToSearch.RemoveAt(index);

                    _agent.SetDestination(_currentTarget.position);
                }
                else
                {
                    _timerBeforePatrol += Time.fixedDeltaTime;

                    if (_timerBeforePatrol < 3) return;
                    
                    _timerBeforePatrol = 0;
                    
                    stateMachine.ChangeState(((PatientStateMachine) stateMachine).patrollingState); Debug.Log("I have no more waypoints, now I patrol");
                }
            }
            else
            {
                if (_agent.hasPath) return;
                
                _timerBeforeChangingWaypoint += Time.fixedDeltaTime;
                if (_timerBeforeChangingWaypoint < 3) return;
                _timerBeforeChangingWaypoint = 0;
                    
                _currentTarget = null;
            }
        }
    }
}