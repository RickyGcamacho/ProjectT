using Enemy.FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy.FiniteStateMachine
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PatientStateMachine : StateMachine
    {
        [HideInInspector] public Patrolling patrollingState;
        [HideInInspector] public Pursuing pursuingState;
        [HideInInspector] public Attacking attackingState;
        [HideInInspector] public Searching searchingState;
        
        public Transform[] waypoints;
        public NavMeshAgent agent;

        public float speed = 5;
        [Tooltip("The speed multiplier when chasing")]
        public float speedMultiplier = 1.5f;
        [Tooltip("Tolerance to change waypoint")]
        [Range(0f, 1.5f)]
        public float distanceToChangeWaypoint;
        public float distanceToChase;
        public float distanceToAttack;
        [Tooltip("Time needed for the attack to occur")]
        [Range(0f, 10f)]
        public float timeToAttack;
        [Tooltip("Times of successful attacks needed to defeat the player")]
        [Range(2, 10)]
        public int timesBeforeDefeat;

        public Transform player;

        [Tooltip("Layers I can see")]
        public LayerMask layerMask;

        private void Awake()
        {
            var myTransform = transform;
            patrollingState = new Patrolling(this, myTransform, player, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, layerMask);
            pursuingState = new Pursuing(this, myTransform, player, agent, speed, speedMultiplier, distanceToChase, distanceToAttack, layerMask);
            attackingState = new Attacking(this, myTransform, player, distanceToAttack, timeToAttack, timesBeforeDefeat);
            searchingState = new Searching(this, myTransform, player, agent, speed, distanceToAttack, distanceToChase, waypoints, layerMask);
        }

        protected override BaseState GetInitialState()
        {
            return patrollingState;
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, distanceToChase);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, distanceToAttack);
            
            var vectorToPlayer = player.position - position;
            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, vectorToPlayer);
            
            var destination = vectorToPlayer.normalized * (vectorToPlayer.magnitude - 3);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, destination + position);
        }
    }
}