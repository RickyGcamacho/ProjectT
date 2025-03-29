using System.Collections;
using _Scripts.Enemy.FiniteStateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Enemy
{
    public class VisionEffect : MonoBehaviour // This script it's intended to go attached to the player
    {
        public Image borderImage;
        public Image fadeImage;
        private Color _auxColorBorder;
        private Color _auxColorScreen;
        [Tooltip("Amount of time is seconds it takes to fade in")]
        [Range(0.1f, 5)]
        public float timeToFadeIn;
        [Tooltip("Amount of time is seconds it takes to fade out")]
        [Range(0.1f, 5)]
        public float timeToFadeOut;
    
        public PatientStateMachine enemy;

        [Range(1, 2)]
        public float distanceMultiplier;

        private void Awake()
        {
            _auxColorBorder = borderImage.color;
            _auxColorBorder.a = 0;
            borderImage.color = _auxColorBorder;
            
            _auxColorScreen = fadeImage.color;
            _auxColorScreen.a = 1;
            fadeImage.color = _auxColorScreen;

            if (!enemy) enemy = FindObjectOfType<PatientStateMachine>();
        }

        private void Update()
        {
            BorderEffect();
        }

        private void BorderEffect()
        {
            _auxColorBorder.a = Mathf.InverseLerp(enemy.distanceToChase * distanceMultiplier, enemy.distanceToAttack * distanceMultiplier, (enemy.transform.position - transform.position).magnitude);
            borderImage.color = _auxColorBorder;
        }

        public IEnumerator FadeOutEffect()
        {
            while (fadeImage.color.a < 1)
            {
                _auxColorScreen.a += Time.deltaTime / timeToFadeOut;
                fadeImage.color = _auxColorScreen;

                yield return 0;
            }

            if (!(fadeImage.color.a > 1)) yield break;
            _auxColorScreen.a = 1;
            fadeImage.color = _auxColorScreen;
        }

        public IEnumerator FadeInEffect()
        {
            while (fadeImage.color.a > 0)
            {
                _auxColorScreen.a -= Time.deltaTime / timeToFadeIn;
                fadeImage.color = _auxColorScreen;

                yield return 0;
            }

            if (!(fadeImage.color.a < 0)) yield break;
            _auxColorScreen.a = 0;
            fadeImage.color = _auxColorScreen;
        }
    }
}