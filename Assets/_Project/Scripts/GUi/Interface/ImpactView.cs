using System;
using System.Collections;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.Signals;
using _Project.Scripts.General.Utils;
using DG.Tweening;
using Template.Scripts.General;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.GUi.Interface
{
    public class ImpactView : MonoBehaviour
    {
        [SerializeField] private View _impactView;
        [SerializeField] private TextMeshProUGUI _enemyDamageText;
        [SerializeField] private TextMeshProUGUI _killTargetText;
        [SerializeField, Range(1.0f, 5.0f)] private float _viewDelay = 3.0f;
        [SerializeField, Range(1.0f, 15.0f)] private float _lerpSpeed = 3.0f;

        private Coroutine _viewRoutine;
        private Tween _tween;
        private int _previousDamage;
        private int _targetDamage;
        private int _currentValue;

        private void Awake()
        {
            _killTargetText.transform.localScale = Vector3.zero;
        }


        [Sub]
        private void OnTakeDamageAtEnemy(OnTakeDamageAtEnemy reference)
        {
            if (_viewRoutine != null) StopCoroutine(_viewRoutine);
            if (_impactView.IsActive)
            {
                _targetDamage += reference.Damage;
            }

            else
            {
                _impactView.Enable();
                _targetDamage = reference.Damage;
            }


            _viewRoutine = StartCoroutine(ImpactViewRoutine());
        }

        [Sub]
        private void OnKillTarget(OnKillTarget reference)
        {
            _tween.Kill();
            string color = reference.Team == Team.Blue ? "blue" : "red";
            _killTargetText.text = "KILLED <color=" + color + ">" + reference.Name + "</color>";
            _tween = _killTargetText.transform.DOScale(1f, 0.35f);
            StartCoroutine(WaitUtils.WaitWithDelay(() =>
            {
                _tween = _killTargetText.transform.DOScale(0f, 0.35f);
            }, 1f));
        }


        private void LateUpdate()
        {
            _currentValue = (int)Mathf.Lerp(_currentValue, _targetDamage, _lerpSpeed * Time.deltaTime);
            _enemyDamageText.text = _enemyDamageText.text = _currentValue + " | ENEMY DAMAGED";
        }

        private IEnumerator ImpactViewRoutine()
        {
            yield return new WaitForSeconds(_viewDelay);
            _impactView.Disable();
            _targetDamage = 0;
        }
    }

    public struct OnTakeDamageAtEnemy
    {
        public int Damage;
    }

    public struct OnKillTarget
    {
        public string Name;
        public Team Team;
    }
}