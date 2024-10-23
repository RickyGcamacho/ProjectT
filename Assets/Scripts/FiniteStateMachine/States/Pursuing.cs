using UnityEngine;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace FiniteStateMachine.States
{
    public class Pursuing : BaseState
    {
        private Transform _myTransform;
        private Transform _player;
        private Rigidbody _rigidBody;
        private float _speed;
        private float _distanceToAttack;
        
        // ReSharper disable once SuggestBaseTypeForParameter
        public Pursuing(PatientStateMachine stateMachine, Transform myTransform, Transform player, Rigidbody rigidBody, float speed, float distanceToAttack) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

            _myTransform = myTransform;
            _player = player;
            _rigidBody = rigidBody;
            
            _speed = speed;
            _distanceToAttack = distanceToAttack;
        }

        public override void UpdatePhysics()
        {
            var directionalVector = _player.position - _myTransform.position;

            if (directionalVector.magnitude < _distanceToAttack)
            {
                stateMachine.ChangeState(((PatientStateMachine) stateMachine).attackingState);
                return;
            }
            
            _myTransform.LookAt(_player);
            MoveTowardTarget(directionalVector, _speed);
        }

        private void MoveTowardTarget(Vector3 direction, float speed) // creo que ya no necesito este método
        {
            _rigidBody.MovePosition(_myTransform.position + direction.normalized * (speed * Time.fixedDeltaTime));
        }
    }
}