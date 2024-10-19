using UnityEngine;

namespace FiniteStateMachine.States
{
    public class Idle : BaseState
    {
        private readonly Transform[] _waypoints;
        private Transform _currentTarget;
        private int _targetIndex;
        private readonly Transform _myTransform;
        private readonly Rigidbody _rigidBody;
        private readonly float _speed;
        private readonly float _distanceToChangeWaypoint;

        // ReSharper disable once SuggestBaseTypeForParameter
        public Idle(PatientStateMachine stateMachine, Transform myTransform, Rigidbody rigidBody, float speed, float distanceToChangeWaypoint, Transform[] waypoints) : base(stateMachine)
        {
            _waypoints = waypoints;
            _myTransform = myTransform;
            _rigidBody = rigidBody;
            _speed = speed;
            _distanceToChangeWaypoint = distanceToChangeWaypoint;
        }

        public override void Enter()
        {
            _targetIndex = Random.Range(0, _waypoints.Length);
            _currentTarget = _waypoints[_targetIndex];
        }

        public override void UpdatePhysics()
        {
            var directionalVector = _currentTarget.transform.position - _myTransform.position;
            
            if (directionalVector.magnitude < _distanceToChangeWaypoint)
            {
                var chooseDifferent = _targetIndex;
                while (chooseDifferent == _targetIndex)
                {
                    _targetIndex = Random.Range(0, _waypoints.Length);
                }

                _currentTarget = _waypoints[_targetIndex];
            }
            else
            {
                MoveTowardTarget(directionalVector, _speed);
            }
        }

        private void MoveTowardTarget(Vector3 direction, float speed)
        {
            _rigidBody.MovePosition(_myTransform.position + direction.normalized * (speed * Time.fixedDeltaTime));
        }
    }
}