using FiniteStateMachine.States;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PatientStateMachine : StateMachine
    {
        [HideInInspector] public Idle idleState;
        //[HideInInspector] public Moving movingState;
        public Transform[] waypoints;
        public Rigidbody rigidBody;
        public float speed;
        public float distanceToChangeWaypoint;

        private void Awake()
        {
            idleState = new Idle(this, transform, rigidBody, speed, distanceToChangeWaypoint, waypoints);
            //movingState = new Moving(this);
        }

        protected override BaseState GetInitialState()
        {
            return idleState;
        }
    }
}