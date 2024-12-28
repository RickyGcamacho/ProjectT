using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Enemy.FiniteStateMachine.States
{
    public class Pursuing : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly NavMeshAgent _agent;
        
        private readonly float _speed;
        private readonly float _speedMultiplier;
        private readonly float _distanceToAttack;
        private readonly float _distanceToChase;
        
        private float _timerToChange;

        public Vector3 playerLastKnownPosition;
        private readonly LayerMask _layerMask;
        
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
            
            Debug.Log("Pursuing player");
        }

        public override void Exit()
        {
            _agent.speed = _speed;
            _agent.acceleration = _speed;
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
            
            if (Physics.Raycast(ray, out var hit, _distanceToChase * 2, _layerMask, QueryTriggerInteraction.Collide))
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
            
            _agent.SetDestination(playerLastKnownPosition);
        }
    }
}