using UnityEngine;
using UnityEngine.AI;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace FiniteStateMachine.States
{
    public class Pursuing : BaseState
    {
        private Transform _myTransform;
        private Transform _player;
        private NavMeshAgent _agent;
        
        private float _speed;
        private float _speedMultiplier;
        private float _distanceToAttack;
        private float _distanceToChase;
        
        private float _timerToChange;

        public Vector3 playerLastKnownPosition;
        private LayerMask _layerMask;
        
        // ReSharper disable once SuggestBaseTypeForParameter
        public Pursuing(PatientStateMachine stateMachine, Transform myTransform, Transform player, NavMeshAgent agent, float speed, float speedMultiplier, float distanceToChase, float distanceToAttack, LayerMask layerMask) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _agent = agent;
            
            _speed = speed;
            _speedMultiplier = speedMultiplier;
            
            _distanceToChase = distanceToChase;
            _distanceToAttack = distanceToAttack;

            _layerMask = layerMask;
        }

        public override void Enter()
        {
            var auxSpeed = _speed * _speedMultiplier;
            _agent.speed = auxSpeed;
            _agent.acceleration = auxSpeed;
        }

        public override void Exit()
        {
            var auxSpeed = _speed / _speedMultiplier;
            _agent.speed = auxSpeed;
            _agent.acceleration = auxSpeed;
        }

        public override void UpdateLogic()
        {
            UpdatePlayerPosition();
        }

        public override void UpdatePhysics()
        {
            var myPosition = _myTransform.position;
            var vectorToPlayer = _player.position - myPosition;
            
            var ray = new Ray(myPosition, vectorToPlayer.normalized);
            
            var distanceToPlayer = vectorToPlayer.magnitude;
            
            if (Physics.Raycast(ray, out var hit, _distanceToChase * 2, _layerMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    if (distanceToPlayer < _distanceToAttack)
                    {
                        stateMachine.ChangeState(((PatientStateMachine) stateMachine).attackingState);
                    }
                    else if (distanceToPlayer > _distanceToChase * 1.5f)
                    {
                        stateMachine.ChangeState(((PatientStateMachine) stateMachine).patrollingState);
                    }
                }
                else
                {
                    stateMachine.ChangeState(((PatientStateMachine) stateMachine).searchingState);
                }
            }

            _myTransform.LookAt(_player);
        }

        private void UpdatePlayerPosition()
        {
            var position = _myTransform.position;
            var vectorToPlayer = _player.position - position;

            playerLastKnownPosition = vectorToPlayer.normalized * (vectorToPlayer.magnitude - 1) + position;
            
            Debug.Log("Update player position");
            _agent.SetDestination(playerLastKnownPosition);
        }
    }
}