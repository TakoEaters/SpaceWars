using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Core.LocatorServices;
using _Project.Scripts.Core.SignalBus;
using _Project.Scripts.General.LevelHandlers;
using _Project.Scripts.General.Signals;
using _Project.Scripts.GUi.Interface;
using UnityEngine;

namespace _Project.Scripts.Common
{
    public class MultiplayerHandler : MonoBehaviour, IGameHandler
    {
        [SerializeField, Range(100, 1000)] private int _necessaryWinScore = 500;
        [SerializeField, Range(1, 10)] private int _totalSessionMinutes = 5;
    
        
        private readonly List<TeamScore> _teams = new List<TeamScore>();
        private Coroutine _countRoutine;
        private IScoreSystem _score;
    
        private readonly int _secondsInMinute = 60;
        private int _remainingTime;
    
        public void Register()
        {
            ServiceLocator.Current.Register<IGameHandler>(this);
        }


        [Sub]
        private void OnStartLevel(StartLevel reference)
        {
            StartCounter();
        }
        
        [Sub]
        private void OnReceiveScore(ScoreChanger reference)
        {
            TeamScore currentTeam = _teams.Find(x => x.Team == reference.Team);
            if (currentTeam == null)
            {
                currentTeam = new TeamScore(reference.Team, reference.Amount);
                _teams.Add(currentTeam);
                return;
            }

            currentTeam.TotalAmount += reference.Amount;
            _score.SetTeamScore(currentTeam);
        }
    
        private void StartCounter()
        {
            _score = ServiceLocator.Current.Get<IScoreSystem>();
            _score.SetMaxScore(_necessaryWinScore);
            _countRoutine = StartCoroutine(Count());
        }
        
        private bool IsAnyWinner()
        {
            TeamScore winner = _teams.Find(x => x.TotalAmount >= _necessaryWinScore);
            return winner != null;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator Count()
        {
            _remainingTime = _totalSessionMinutes * _secondsInMinute;
            WaitForSeconds waitTime = new WaitForSeconds(1f);

            while (_remainingTime > 0)
            {
                if (IsAnyWinner()) yield break;
                _score.SetCounterTime(_remainingTime);
                _remainingTime -= 1;
                yield return waitTime;
            }
        }
    }

    public interface IGameHandler : IGameService
    {
    }
}