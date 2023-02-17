using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.General.GameEvents
{
    public class StarshipsEvent : GameEvent
    {
        [SerializeField] private Transform _firstStarship;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _flyDuration = 20f;

        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = _firstStarship.position;
        }

        public override void EnableEvent(Action callback)
        {
            OnStopEvent = callback;
            StartCoroutine(ShipFly());
            _firstStarship.DOMove(_endPoint.position, _flyDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                _firstStarship.position = _initialPosition;
            });
        }

        private IEnumerator ShipFly()
        {
            float elapsedTime = 0f;
            while (elapsedTime < _duration)
            {
                _firstStarship.position = _initialPosition;
                _firstStarship.DOMove(_endPoint.position, _flyDuration).SetEase(Ease.Linear);
                elapsedTime += _duration;
                yield return new WaitForSeconds(_flyDuration);
            }
            
            OnStopEvent?.Invoke();
        }
    }
}
