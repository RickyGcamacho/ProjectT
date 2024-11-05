using UnityEngine;

namespace Enemy.FiniteStateMachine.States
{
    public class Attacking : BaseState
    {
        private readonly Transform _myTransform;
        private readonly Transform _player;
        private readonly float _distanceToAttack;
        private float _attackTimer; // Timer that goes up
        private readonly float _timeToAttack;
        private int _timesOfSuccessfulAttacks; // Times the attack was successful
        private readonly int _timesBeforeDefeat;

        public Attacking(StateMachine stateMachine, Transform myTransform, Transform player, float distanceToAttack, float timeToAttack, int timesBeforeDefeat) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            
            _distanceToAttack = distanceToAttack;
            _timeToAttack = timeToAttack;
            _timesBeforeDefeat = timesBeforeDefeat;
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
            else
            {
                _attackTimer += Time.deltaTime;
                
                if (_attackTimer < _timeToAttack) return;

                _attackTimer = 0;
                _timesOfSuccessfulAttacks++;
                
                if (_timesOfSuccessfulAttacks < _timesBeforeDefeat)
                {
                    EnemyManager.instance.CaughtPlayer();
                }
                else
                {
                    EnemyManager.instance.PlayerLost();
                }
            }
        }

        public override void Exit()
        {
            _attackTimer = 0;
        }
    }
}