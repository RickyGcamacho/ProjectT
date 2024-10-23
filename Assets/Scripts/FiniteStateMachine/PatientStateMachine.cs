using FiniteStateMachine.States;
using UnityEngine;
using UnityEngine.AI;

namespace FiniteStateMachine
{
    [RequireComponent(typeof(Rigidbody))]
    public class PatientStateMachine : StateMachine
    {
        [HideInInspector] public Patrolling patrollingState;
        [HideInInspector] public Pursuing pursuingState;
        [HideInInspector] public Attacking attackingState; // probably needs a StateMachine, my transform, the player transform, and the distance to chase the player again
        
        public Transform[] waypoints;
        public Rigidbody rigidBody;
        public NavMeshAgent agent;
        
        public float speed;
        public float distanceToChangeWaypoint;
        public float distanceToChase;
        public float distanceToAttack;

        public Transform player;

        private void Awake()
        {
            if (!rigidBody) rigidBody = GetComponent<Rigidbody>();
            
            var myTransform = transform;
            patrollingState = new Patrolling(this, myTransform, rigidBody, agent, speed, distanceToChangeWaypoint, distanceToChase, waypoints, player);
            pursuingState = new Pursuing(this, myTransform, player, rigidBody, speed, distanceToAttack);
            attackingState = new Attacking(this, myTransform, player, distanceToAttack);
        }

        protected override BaseState GetInitialState()
        {
            return patrollingState;
        }
    }
}