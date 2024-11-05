using System.Collections;
using Enemy.FiniteStateMachine;
using UnityEngine;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager instance;
        
        [SerializeField] private PatientStateMachine _patientStateMachine;
        [SerializeField] private VisionEffect _visionEffect;

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            StartCoroutine(_visionEffect.FadeInEffect());
        }

        public void CaughtPlayer()
        {
            StartCoroutine(Caught());
        }

        private IEnumerator Caught()
        {
            StartCoroutine(_visionEffect.FadeOutEffect());
            yield return new WaitUntil(() => _visionEffect.fadeImage.color.a >= 1);
            
            _patientStateMachine.player.position = _patientStateMachine.waypoints[Random.Range(0, _patientStateMachine.waypoints.Length)].position;
            StartCoroutine(_visionEffect.FadeInEffect());
        }

        public void PlayerLost()
        {
            StartCoroutine(_visionEffect.FadeOutEffect());
        }
    }
}