using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using MyBox;
using UnityEngine;

namespace _Project.Scripts.General.GameEvents
{
    public class GameEvents : MonoBehaviour
    {
        [SerializeField] private List<GameEvent> _events = new List<GameEvent>();

        private Coroutine _eventsRoutine;
        private GameEvent _currentEvent;
        private bool _eventWasFinished;
        
        [Sub]
        private void OnStartGame(StartLevel reference)
        {
            _eventsRoutine = StartCoroutine(GameEventRoutine());
        }

        private IEnumerator GameEventRoutine()
        {
            while (true)
            {
                _eventWasFinished = false;
                _currentEvent = _events.GetRandom();
                _currentEvent.EnableEvent(() => _eventWasFinished = true);
                yield return new WaitUntil(() => _eventWasFinished);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        [Sub]
        private void OnFinishGame(FinishLevel reference)
        {
            if (_eventsRoutine != null) StopCoroutine(_eventsRoutine);
        }
    }
}
