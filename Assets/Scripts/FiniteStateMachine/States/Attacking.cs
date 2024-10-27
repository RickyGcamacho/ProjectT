using UnityEngine;

namespace FiniteStateMachine.States
{
    public class Attacking : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly float _distanceToAttack;

        public Attacking(StateMachine stateMachine, Transform myTransform, Transform player, float distanceToAttack) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _distanceToAttack = distanceToAttack;
        }

        public override void Enter()
        {
            Debug.Log("Attack!");
        }

        public override void UpdateLogic()
        {
            if ((_player.position - _myTransform.position).magnitude > _distanceToAttack * 1.5f)
            {
                stateMachine.ChangeState(((PatientStateMachine) stateMachine).pursuingState);
            }
        }
    }
}